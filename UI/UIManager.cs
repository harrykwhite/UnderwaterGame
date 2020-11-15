namespace UnderwaterGame.Ui
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Ui.UiComponents;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Ui.UiElements.Menus;

    public static class UiManager
    {
        public static List<UiElement> uiElements;

        public static FadeElement[] fadeElements;

        public static MenuElement menuCurrent;

        public static int scale = 2;

        public static void Init()
        {
            uiElements = new List<UiElement>();
            fadeElements = new FadeElement[4];
            LoadElements();
        }

        public static void Update()
        {
            CheckMenu();
            UpdateElements();
        }

        public static void Draw()
        {
            DrawElements();
        }

        public static void LoadElements()
        {
            AddElement<HotspotCountElement>();
            AddElement<CharacterHealthElement>();
            AddElement<FloatingTextElement>();
            fadeElements[0] = new FadeElement { alphaMax = 0.5f, getActive = () => menuCurrent != null };
            AddElement(fadeElements[0], true);
            AddElement<PlayerMenu>();
            fadeElements[1] = new FadeElement { alphaMax = 0.5f, getActive = delegate () { OptionsMenu optionsMenu = (OptionsMenu)GetElement<OptionsMenu>(); return optionsMenu.open; } };
            AddElement(fadeElements[1], true);
            AddElement<OptionsMenu>();
            fadeElements[2] = new FadeElement { alphaMax = 1f, getActive = () => Main.loading != null };
            AddElement(fadeElements[2], true);
            AddElement<LoadingElement>(true);
            AddElement<GameCursorElement>(true);
            AddElement<ScreenFlashElement>();
        }

        public static void UpdateElements()
        {
            UiElement[] tempElements = uiElements.ToArray();
            foreach(UiElement element in tempElements)
            {
                if(!element.loadingUpdate)
                {
                    if(Main.loading != null)
                    {
                        continue;
                    }
                }
                element.Update();
                foreach(UiComponent component in element.components)
                {
                    if(component.getActive())
                    {
                        component.Update();
                    }
                }
            }
        }

        public static void DrawElements()
        {
            UiElement[] tempElements = uiElements.ToArray();
            foreach(UiElement element in tempElements)
            {
                if(!element.loadingUpdate)
                {
                    if(Main.loading != null && fadeElements[2].alpha == fadeElements[2].alphaMax)
                    {
                        continue;
                    }
                }
                element.Draw();
                foreach(UiComponent component in element.components)
                {
                    if(component.getActive())
                    {
                        component.Draw();
                    }
                }
            }
        }

        public static UiElement AddElement<T>(bool loadingUpdate = false) where T : UiElement
        {
            UiElement element = Activator.CreateInstance<T>();
            element.loadingUpdate = loadingUpdate;
            element.Init();
            uiElements.Add(element);
            return element;
        }

        public static UiElement AddElement(UiElement element, bool loadingUpdate = false)
        {
            element.loadingUpdate = loadingUpdate;
            element.Init();
            uiElements.Add(element);
            return element;
        }

        public static UiElement GetElement<T>() where T : UiElement
        {
            foreach(UiElement element in uiElements)
            {
                if(element is T)
                {
                    return element;
                }
            }
            return null;
        }

        public static Vector2 WorldToUi(Vector2 world)
        {
            return ((world - (Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f))) * Camera.scale) / scale;
        }

        public static Vector2 UiToWorld(Vector2 ui)
        {
            return ((ui * scale) / Camera.scale) + Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f);
        }

        private static void CheckMenu()
        {
            menuCurrent = null;
            foreach(UiElement element in uiElements)
            {
                if(element is MenuElement menuElement)
                {
                    if(menuElement.open)
                    {
                        menuCurrent = menuElement;
                    }
                }
            }
        }

        public static Vector2 GetSize()
        {
            return new Vector2(Main.GetBufferWidth(), Main.GetBufferHeight()) / scale;
        }
    }
}