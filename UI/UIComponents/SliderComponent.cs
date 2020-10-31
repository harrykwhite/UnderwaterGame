namespace UnderwaterGame.Ui.UiComponents
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Options;
    using UnderwaterGame.Sound;
    using UnderwaterGame.Ui.UiElements;

    public class SliderComponent : UiComponent
    {
        private Vector2 ballPosition;

        private Shape ballShape;

        public Option option;

        public static SliderComponent locked;

        public override void Draw()
        {
            Main.spriteBatch.Draw(Main.textureLibrary.UI_SLIDER_BAR.asset, getPosition(), null, Color.White * 0.5f * getAlpha(), 0f, new Vector2(Main.textureLibrary.UI_SLIDER_BAR.asset.Width, Main.textureLibrary.UI_SLIDER_BAR.asset.Height) / 2f, scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(Main.textureLibrary.UI_SLIDER_BALL.asset, ballPosition, null, Color.White * getAlpha(), 0f, new Vector2(Main.textureLibrary.UI_SLIDER_BALL.asset.Width, Main.textureLibrary.UI_SLIDER_BALL.asset.Height) / 2f, scale, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            ballShape = new Shape(Shape.Fill.Circle, Main.textureLibrary.UI_SLIDER_BALL.asset.Width, Main.textureLibrary.UI_SLIDER_BALL.asset.Height);
        }

        public override void Update()
        {
            Vector2 mousePosition = Control.GetMousePositionUi();
            bool buttonLeft = Control.MouseLeftHeld();
            bool buttonLeftPressed = Control.MouseLeftPressed();
            UpdateScale();
            if(getAlpha() > 0f)
            {
                if(buttonLeftPressed)
                {
                    Shape ballShape = this.ballShape;
                    ballShape.position = ballPosition - new Vector2((int)Math.Ceiling(Main.textureLibrary.UI_SLIDER_BALL.asset.Width / 2f), (int)Math.Ceiling(Main.textureLibrary.UI_SLIDER_BALL.asset.Height / 2f));
                    if(ballShape.Intersects(((GameCursorElement)UiManager.GetElement<GameCursorElement>()).GetShape()))
                    {
                        locked = this;
                    }
                }
            }
            if(locked == this)
            {
                if(getAlpha() <= 0f || !buttonLeft)
                {
                    locked = null;
                }
            }
            UpdateBall();
        }

        private void UpdateBall()
        {
            int width = Main.textureLibrary.UI_SLIDER_BAR.asset.Width - Main.textureLibrary.UI_SLIDER_BALL.asset.Width;
            float rawValue = option.value - option.valueMin;
            float rawSpan = option.valueMax - option.valueMin;
            if(locked == this)
            {
                float mouse = MathHelper.Clamp(Control.GetMousePositionUi().X, getPosition().X - (width / 2f), getPosition().X + (width / 2f));
                float valuePrevious = option.value;
                option.value = ((mouse - (getPosition().X - (width / 2f))) / width) * rawSpan;
                option.value += option.valueMin;
                if(option.valueRounded)
                {
                    option.value = (float)Math.Round(option.value);
                }
                int valueDisplay = (int)Math.Floor(option.value * 100f);
                int valuePreviousDisplay = (int)Math.Floor(valuePrevious * 100f);
                if(valueDisplay != valuePreviousDisplay)
                {
                    SoundManager.PlaySound(Main.soundLibrary.UI_SCROLL.asset, SoundManager.Category.Sound);
                    if(option.valueRounded)
                    {
                        locked = null;
                    }
                }
            }
            ballPosition = getPosition() + new Vector2(((rawValue / rawSpan) - 0.5f) * width, 0f);
        }
    }
}