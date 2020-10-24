using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UnderwaterGame.UI.UIComponents;
using UnderwaterGame.UI.UIComponents.Buttons;

namespace UnderwaterGame.UI.UIElements.Menus
{
    public abstract class MenuElement : UIElement
    {
        protected List<IconButton>[] iconButtons;

        protected bool waitUntilLoaded;
        protected Action waitUntilLoadedAction;

        protected float Alpha { get; private set; }
        protected virtual float AlphaTo => Open ? 1f : 0f;
        protected virtual float AlphaSpeed => 0.1f;

        public bool Open { get; private set; }

        public void ToggleOpen()
        {
            Open = !Open;
        }

        public void ToggleOpen(bool open)
        {
            Open = open;
        }

        protected void UpdateAlpha()
        {
            if (Alpha < AlphaTo)
            {
                Alpha += Math.Min(AlphaTo - Alpha, AlphaSpeed);
            }
            else if (Alpha > AlphaTo)
            {
                Alpha -= Math.Min(Alpha - AlphaTo, AlphaSpeed);
            }
        }

        protected void UpdateLoadingWaitComplete()
        {
            if (waitUntilLoaded)
            {
                if (Main.loading == null)
                {
                    ToggleOpen(false);
                    Alpha = 0f;

                    waitUntilLoadedAction?.Invoke();

                    waitUntilLoaded = false;
                    waitUntilLoadedAction = null;
                }
            }
        }

        protected void InitIconButtons()
        {
            int buttonCount = 4;
            iconButtons = new List<IconButton>[buttonCount];

            for (int i = 0; i < buttonCount; i++)
            {
                iconButtons[i] = new List<IconButton>();
            }
        }

        protected IconButton AddIconButton(int corner, Texture2D icon, Func<bool> getActive)
        {
            int count = iconButtons[corner].Count;

            IconButton iconButton = (IconButton)AddComponent<IconButton>();
            iconButtons[corner].Add(iconButton);

            iconButton.menuElement = this;
            iconButton.icon = icon;

            iconButton.getAlpha = () => Alpha;
            iconButton.getActive = getActive;

            iconButton.getPosition = delegate ()
            {
                switch (corner)
                {
                    case 0:
                        return new Vector2(UIComponent.Gap * (count + 1), UIComponent.Gap);

                    case 1:
                        return new Vector2(UIManager.Size.X - (UIComponent.Gap * (count + 1)), UIComponent.Gap);

                    case 2:
                        return new Vector2(UIComponent.Gap * (count + 1), UIManager.Size.Y - UIComponent.Gap);

                    case 3:
                        return new Vector2(UIManager.Size.X - (UIComponent.Gap * (count + 1)), UIManager.Size.Y - UIComponent.Gap);
                }

                return Vector2.Zero;
            };

            return iconButton;
        }

        protected IconButton AddBackIconButton(Func<bool> getActive)
        {
            IconButton returnButton = AddIconButton(2, Main.TextureLibrary.UI_BUTTONS_ICONS_OTHER_BACKICON.Asset, getActive);

            returnButton.selectedInteractAction = delegate ()
            {
                ToggleOpen(false);
            };

            return returnButton;
        }
    }
}