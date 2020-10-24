using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Input;
using UnderwaterGame.Sound;
using UnderwaterGame.UI.UIElements.Menus;

namespace UnderwaterGame.UI.UIComponents.Buttons
{
    public abstract class ButtonComponent : UIComponent
    {
        public MenuElement menuElement;

        public bool selected;
        public Action selectedAction;
        public Action selectedInteractAction;
        public Action selectedInteractAlternativeAction;

        public Func<bool> getSelected;
        public Func<bool> getSelectedInteract;

        public bool touching;
        public Func<bool> getCanTouch;

        protected bool CanTouch => Alpha > 0f && Main.loading == null && UIManager.MenuCurrent == menuElement && (getCanTouch?.Invoke() ?? true);
        protected abstract bool IsTouching();

        protected void UpdateButton()
        {
            bool selectedPrevious = selected;

            touching = IsTouching();
            selected = getSelected?.Invoke() ?? touching;

            scaleTo = selected ? scaleMax : Vector2.One;
            UpdateScale();

            if (selected)
            {
                selectedAction?.Invoke();

                if (touching && !selectedPrevious)
                {
                    SoundManager.PlaySound(Main.SoundLibrary.UI_HOVER.Asset, SoundManager.Category.Sound);
                }

                if (getSelectedInteract?.Invoke() ?? true)
                {
                    bool interacted = false;

                    if (InputManager.MouseLeftPressed())
                    {
                        if (selectedInteractAction != null)
                        {
                            selectedInteractAction.Invoke();
                            interacted = true;
                        }
                    }

                    if (InputManager.MouseRightPressed())
                    {
                        if (selectedInteractAlternativeAction != null)
                        {
                            selectedInteractAlternativeAction.Invoke();
                            interacted = true;
                        }
                    }

                    if (interacted)
                    {
                        SoundManager.PlaySound(Main.SoundLibrary.UI_CLICK.Asset, SoundManager.Category.Sound);
                    }
                }
            }
        }
    }
}