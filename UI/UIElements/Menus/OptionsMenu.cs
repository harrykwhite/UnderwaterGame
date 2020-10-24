using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using UnderwaterGame.Input;
using UnderwaterGame.Options;
using UnderwaterGame.UI.UIComponents;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIElements.Menus
{
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

                string optionText = option.Name;
                string optionTextValue = option.value.ToString();

                switch (option.ValueFormat)
                {
                    case Option.Format.Percent:
                        optionTextValue = Math.Floor(option.value * 100f).ToString() + "%";
                        break;

                    case Option.Format.Toggle:
                        optionTextValue = option.Toggle ? "Enabled" : "Disabled";
                        break;
                }

                DrawUtilities.DrawString(Main.FontLibrary.ARIALSMALL.Asset, new DrawUtilities.Text(option.Name + ": " + optionTextValue), getPosition.Invoke(), Color.White * getAlpha.Invoke(), DrawUtilities.HAlign.Middle, DrawUtilities.VAlign.Middle);
            }

            public void Init()
            {
                slider = new SliderComponent();
                slider.Init();

                slider.option = option;

                slider.getAlpha = getAlpha;
                slider.getPosition = () => getPosition.Invoke() + new Vector2(0f, DrawUtilities.MeasureString(Main.FontLibrary.ARIALSMALL.Asset, option.Name).Y);
            }

            public void Update()
            {
                if (getAlpha.Invoke() <= 0f)
                {
                    if (SliderComponent.locked == slider)
                    {
                        SliderComponent.locked = null;
                    }
                }

                if (menuElement.Open)
                {
                    slider.Update();
                }
            }
        }

        public OptionButton[] optionButtons;

        public override void Init()
        {
            optionButtons = new OptionButton[Option.Options.Count];

            for (int i = 0; i < optionButtons.Length; i++)
            {
                int tempI = i;

                optionButtons[i] = new OptionButton
                {
                    menuElement = this,
                    getAlpha = () => Alpha,
                    getPosition = () => (UIManager.Size / 2f) + new Vector2(0f, (tempI - (optionButtons.Length / 2f)) * UIComponent.Gap),
                    option = Option.GetOptionByID((byte)(i + 1))
                };

                optionButtons[i].Init();
            }

            InitIconButtons();
            AddBackIconButton(() => true);
        }

        public override void Draw()
        {
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].Draw();
            }
        }

        public override void Update()
        {
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].Update();
            }

            if (Open)
            {
                if (InputManager.KeyPressed(Keys.Escape))
                {
                    ToggleOpen(false);
                }
            }

            UpdateAlpha();
            UpdateLoadingWaitComplete();
        }
    }
}