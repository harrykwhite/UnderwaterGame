namespace UnderwaterGame.Ui.UiElements.Menus
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using UnderwaterGame.Options;
    using UnderwaterGame.Ui.UiComponents;

    public class OptionsMenu : MenuElement
    {
        public SliderComponent[] sliders;

        public override void Init()
        {
            sliders = new SliderComponent[Option.options.Count];
            for(int i = 0; i < sliders.Length; i++)
            {
                int tempI = i;
                sliders[i] = (SliderComponent)AddComponent<SliderComponent>();
                sliders[i].menuElement = this;
                sliders[i].option = Option.GetOptionById((byte)(i + 1));
                sliders[i].getAlpha = () => alpha;
                sliders[i].getPosition = () => (UiManager.GetSize() / 2f) + new Vector2(0f, (tempI - ((sliders.Length - 1f) / 2f)) * UiComponent.gap);
            }
            InitIconButtons();
            AddBackIconButton(() => true);
        }

        public override void Draw()
        {
            for(int i = 0; i < sliders.Length; i++)
            {
                sliders[i].Draw();
            }
        }

        public override void Update()
        {
            for(int i = 0; i < sliders.Length; i++)
            {
                sliders[i].Update();
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