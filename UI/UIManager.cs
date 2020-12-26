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
            AddElement(fadeElements[0] = new FadeElement { color = new Color(204, 81, 81), getActive = () => false }, false);
            AddElement<HealthElement>();
            AddElement<TutorialElement>();
            AddElement(fadeElements[1] = new FadeElement { getActive = () => menuCurrent != null }, true);
            AddElement<PlayerMenu>();
            AddElement(fadeElements[2] = new FadeElement { getActive = delegate () { OptionsMenu optionsMenu = (OptionsMenu)GetElement<OptionsMenu>(); return optionsMenu.open; } }, true);
            AddElement<OptionsMenu>();
            AddElement(fadeElements[3] = new FadeElement { alphaMax = 1f, getActive = () => Main.loading != null }, true);
            AddElement<LoadingElement>(true);
            AddElement<CursorElement>(true);
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
                    if(Main.loading != null && fadeElements[3]?.alpha == fadeElements[3]?.alphaMax)
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
            return (world - (Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f))) * Camera.scale;
        }

        public static Vector2 UiToWorld(Vector2 ui)
        {
            return (ui / Camera.scale) + Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f);
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
    }
}