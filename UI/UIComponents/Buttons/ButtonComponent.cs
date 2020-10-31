namespace UnderwaterGame.Ui.UiComponents.Buttons
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Sound;
    using UnderwaterGame.Ui.UiElements.Menus;

    public abstract class ButtonComponent : UiComponent
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

        protected bool CanTouch => getAlpha() > 0f && Main.loading == null && UiManager.menuCurrent == menuElement && (getCanTouch?.Invoke() ?? true);

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
                    SoundManager.PlaySound(Main.soundLibrary.UI_HOVER.asset, SoundManager.Category.Sound);
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
                        SoundManager.PlaySound(Main.soundLibrary.UI_CLICK.asset, SoundManager.Category.Sound);
                    }
                }
            }
        }
    }
}