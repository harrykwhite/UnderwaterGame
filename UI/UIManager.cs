using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UnderwaterGame.UI.UIComponents;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.UI.UIElements.Menus;

namespace UnderwaterGame.UI
{
    public static class UIManager
    {
        public static List<UIElement> UIElements { get; private set; } = new List<UIElement>();

        public static FadeElement[] FadeElements { get; private set; } = new FadeElement[4];

        public static MenuElement MenuCurrent { get; private set; }

        public static Vector2 Size => new Vector2(Main.BufferWidth, Main.BufferHeight) / Scale;
        public static float Scale => 2f;

        public static bool InMenu => MenuCurrent != null;

        public static void Init()
        {
            UIElements.Clear();
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
            AddElement<FloatingTextElement>();
            AddElement<CharacterHealthElement>();

            FadeElements[0] = new FadeElement
            {
                alphaMax = 0.5f,
                getActive = () => InMenu
            };
            AddElement(FadeElements[0], true);

            AddElement<PlayerMenu>();

            FadeElements[1] = new FadeElement
            {
                alphaMax = 0.5f,
                getActive = delegate ()
                {
                    OptionsMenu optionsMenu = (OptionsMenu)GetElement<OptionsMenu>();

                    return optionsMenu.Open;
                }
            };
            AddElement(FadeElements[1], true);

            AddElement<OptionsMenu>();

            FadeElements[2] = new FadeElement
            {
                alphaMax = 1f,
                getActive = () => Main.loading != null
            };
            AddElement(FadeElements[2], true);

            AddElement<GameCursorElement>(true);
            AddElement<ScreenFlashElement>();
        }

        public static void UpdateElements()
        {
            UIElement[] tempElements = UIElements.ToArray();

            foreach (UIElement element in tempElements)
            {
                if (!element.loadingUpdate)
                {
                    if (Main.loading != null && FadeElements[2].Alpha == FadeElements[2].alphaMax)
                    {
                        continue;
                    }
                }

                element.Update();

                foreach (UIComponent component in element.Components)
                {
                    if (component.Active)
                    {
                        component.Update();
                    }
                }
            }
        }

        public static void DrawElements()
        {
            UIElement[] tempElements = UIElements.ToArray();

            foreach (UIElement element in tempElements)
            {
                if (!element.loadingUpdate)
                {
                    if (Main.loading != null && FadeElements[2].Alpha == FadeElements[2].alphaMax)
                    {
                        continue;
                    }
                }

                element.Draw();

                foreach (UIComponent component in element.Components)
                {
                    if (component.Active)
                    {
                        component.Draw();
                    }
                }
            }
        }

        public static UIElement AddElement<T>(bool loadingUpdate = false) where T : UIElement
        {
            UIElement element = Activator.CreateInstance<T>();
            element.loadingUpdate = loadingUpdate;

            element.Init();

            UIElements.Add(element);
            return element;
        }

        public static UIElement AddElement(UIElement element, bool loadingUpdate = false)
        {
            element.loadingUpdate = loadingUpdate;

            element.Init();

            UIElements.Add(element);
            return element;
        }

        public static UIElement GetElement<T>() where T : UIElement
        {
            foreach (UIElement element in UIElements)
            {
                if (element is T)
                {
                    return element;
                }
            }

            return null;
        }

        public static Vector2 WorldToUI(Vector2 world)
        {
            return ((world - (Camera.position - (new Vector2(Camera.Width, Camera.Height) / 2f))) * Camera.Scale) / Scale;
        }

        public static Vector2 UIToWorld(Vector2 ui)
        {
            return ((ui * Scale) / Camera.Scale) + Camera.position - (new Vector2(Camera.Width, Camera.Height) / 2f);
        }

        private static void CheckMenu()
        {
            MenuCurrent = null;

            foreach (UIElement element in UIElements)
            {
                if (element is MenuElement)
                {
                    MenuElement menuElement = (MenuElement)element;

                    if (menuElement.Open)
                    {
                        MenuCurrent = menuElement;
                    }
                }
            }
        }
    }
}