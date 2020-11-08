namespace UnderwaterGame.Ui.UiComponents.Buttons
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;

    public abstract class ButtonComponent : UiComponent
    {
        public bool selected;

        public Action selectedAction;

        public Action selectedInteractAction;

        public Action selectedInteractAlternativeAction;

        public Func<bool> getSelected;

        public Func<bool> getSelectedInteract;

        public bool touching;

        protected abstract bool IsTouching();

        protected void UpdateButton()
        {
            bool selectedPrevious = selected;
            touching = IsTouching();
            selected = getSelected?.Invoke() ?? touching;
            scaleTo = selected ? scaleMax : Vector2.One;
            UpdateScale();
            if(selected)
            {
                selectedAction?.Invoke();
                if(touching && !selectedPrevious)
                {
                    SoundUtilities.PlaySound(Main.soundLibrary.UI_HOVER.asset);
                }
                if(getSelectedInteract?.Invoke() ?? true)
                {
                    bool interacted = false;
                    if(Control.MouseLeftPressed())
                    {
                        if(selectedInteractAction != null)
                        {
                            selectedInteractAction();
                            interacted = true;
                        }
                    }
                    if(Control.MouseRightPressed())
                    {
                        if(selectedInteractAlternativeAction != null)
                        {
                            selectedInteractAlternativeAction();
                            interacted = true;
                        }
                    }
                    if(interacted)
                    {
                        SoundUtilities.PlaySound(Main.soundLibrary.UI_CLICK.asset);
                    }
                }
            }
        }

        protected bool GetCanTouch()
        {
            return getAlpha() > 0f && UiManager.menuCurrent == menuElement && SliderComponent.locked == null;
        }
    }
}