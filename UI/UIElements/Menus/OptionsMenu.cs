namespace UnderwaterGame.Ui.UiElements.Menus
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using UnderwaterGame.Options;
    using UnderwaterGame.Ui.UiComponents;
    using UnderwaterGame.Utilities;

    public class OptionsMenu : MenuElement
    {
        public class OptionButton
        {
            public MenuElement menuElement;

            public Func<Vector2> getPosition;

            public Func<float> getAlpha;

            public Option option;

            public SliderComponent slider;

            public void Draw()
            {
                slider.Draw();
                string optionText = option.name;
                string optionTextValue = option.value.ToString();
                switch(option.valueFormat)
                {
                    case Option.Format.Percent:
                        optionTextValue = Math.Floor(option.value * 100f).ToString() + "%";
                        break;

                    case Option.Format.Toggle:
                        optionTextValue = option.value == 1f ? "Enabled" : "Disabled";
                        break;
                }
                DrawUtilities.DrawString(Main.fontLibrary.ARIALSMALL.asset, new DrawUtilities.Text(option.name + ": " + optionTextValue), getPosition(), Color.White * getAlpha(), DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
            }

            public void Init()
            {
                slider = new SliderComponent();
                slider.Init();
                slider.option = option;
                slider.getAlpha = getAlpha;
                slider.getPosition = () => getPosition() + new Vector2(0f, DrawUtilities.MeasureString(Main.fontLibrary.ARIALSMALL.asset, option.name).Y);
            }

            public void Update()
            {
                if(getAlpha() <= 0f)
                {
                    if(SliderComponent.locked == slider)
                    {
                        SliderComponent.locked = null;
                    }
                }
                if(menuElement.open)
                {
                    slider.Update();
                }
            }
        }

        public OptionButton[] optionButtons;

        public override void Init()
        {
            optionButtons = new OptionButton[Option.options.Count];
            for(int i = 0; i < optionButtons.Length; i++)
            {
                int tempI = i;
                optionButtons[i] = new OptionButton { menuElement = this, getAlpha = () => alpha, getPosition = () => (UiManager.Size / 2f) + new Vector2(0f, (tempI - (optionButtons.Length / 2f)) * UiComponent.gap), option = Option.GetOptionById((byte)(i + 1)) };
                optionButtons[i].Init();
            }
            InitIconButtons();
            AddBackIconButton(() => true);
        }

        public override void Draw()
        {
            for(int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].Draw();
            }
        }

        public override void Update()
        {
            for(int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].Update();
            }
            if(open)
            {
                if(Control.KeyPressed(Keys.Escape))
                {
                    ToggleOpen(false);
                }
            }
            UpdateAlpha();
            UpdateLoadingWaitComplete();
        }
    }
}