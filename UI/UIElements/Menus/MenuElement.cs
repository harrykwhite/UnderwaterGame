namespace UnderwaterGame.Ui.UiElements.Menus
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Ui.UiComponents;
    using UnderwaterGame.Ui.UiComponents.Buttons;

    public abstract class MenuElement : UiElement
    {
        protected List<IconButton>[] iconButtons;

        protected bool waitUntilLoaded;

        protected Action waitUntilLoadedAction;

        protected float alpha;

        protected float alphaTo;

        protected float alphaAcc = 0.1f;

        public bool open;

        public void ToggleOpen()
        {
            open = !open;
        }

        public void ToggleOpen(bool open)
        {
            this.open = open;
        }

        protected void UpdateAlpha()
        {
            if(open)
            {
                if(alpha < 1f)
                {
                    alpha += Math.Min(alphaAcc, 1f - alpha);
                }
            }
            else
            {
                if(alpha > 0f)
                {
                    alpha -= Math.Min(alphaAcc, alpha);
                }
            }
        }

        protected void UpdateLoadingWaitComplete()
        {
            if(waitUntilLoaded)
            {
                if(Main.loading == null)
                {
                    ToggleOpen(false);
                    alpha = 0f;
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
            for(int i = 0; i < buttonCount; i++)
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
            iconButton.getAlpha = () => alpha;
            iconButton.getActive = getActive;
            iconButton.getPosition = delegate ()
            {
                return corner switch
                {
                    0 => new Vector2(UiComponent.gap * (count + 1), UiComponent.gap),
                    1 => new Vector2(Main.GetBufferWidth() - (UiComponent.gap * (count + 1)), UiComponent.gap),
                    2 => new Vector2(UiComponent.gap * (count + 1), Main.GetBufferHeight() - UiComponent.gap),
                    3 => new Vector2(Main.GetBufferWidth() - (UiComponent.gap * (count + 1)), Main.GetBufferHeight() - UiComponent.gap),
                    _ => Vector2.Zero,
                };
            };
            return iconButton;
        }

        protected IconButton AddBackIconButton(Func<bool> getActive)
        {
            IconButton returnButton = AddIconButton(2, Main.textureLibrary.UI_BUTTONS_ICONS_OTHER_BACKICON.asset, getActive);
            returnButton.selectedInteractAction = delegate ()
            {
                ToggleOpen(false);
            };
            return returnButton;
        }
    }
}