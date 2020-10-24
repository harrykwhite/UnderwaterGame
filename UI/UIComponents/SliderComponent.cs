using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.Input;
using UnderwaterGame.Options;
using UnderwaterGame.Sound;
using UnderwaterGame.UI.UIElements;

namespace UnderwaterGame.UI.UIComponents
{
    public class SliderComponent : UIComponent
    {
        private Vector2 ballPosition;
        private Shape ballShape;

        public Option option;

        public static SliderComponent locked;

        public override void Draw()
        {
            Main.SpriteBatch.Draw(Main.TextureLibrary.UI_SLIDER_BAR.Asset, Position, null, Color.White * 0.5f * Alpha, 0f, new Vector2(Main.TextureLibrary.UI_SLIDER_BAR.Asset.Width, Main.TextureLibrary.UI_SLIDER_BAR.Asset.Height) / 2f, scale, SpriteEffects.None, 1f);
            Main.SpriteBatch.Draw(Main.TextureLibrary.UI_SLIDER_BALL.Asset, ballPosition, null, Color.White * Alpha, 0f, new Vector2(Main.TextureLibrary.UI_SLIDER_BALL.Asset.Width, Main.TextureLibrary.UI_SLIDER_BALL.Asset.Height) / 2f, scale, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            ballShape = new Shape(Shape.Fill.Circle, Main.TextureLibrary.UI_SLIDER_BALL.Asset.Width, Main.TextureLibrary.UI_SLIDER_BALL.Asset.Height);
        }

        public override void Update()
        {
            Vector2 mousePosition = InputManager.GetMousePositionUI();

            bool buttonLeft = InputManager.MouseLeftHeld();
            bool buttonLeftPressed = InputManager.MouseLeftPressed();

            UpdateScale();

            if (Alpha > 0f)
            {
                if (buttonLeftPressed)
                {
                    Shape ballShape = this.ballShape;
                    ballShape.position = ballPosition - new Vector2((int)Math.Ceiling(Main.TextureLibrary.UI_SLIDER_BALL.Asset.Width / 2f), (int)Math.Ceiling(Main.TextureLibrary.UI_SLIDER_BALL.Asset.Height / 2f));

                    if (ballShape.Intersects(((GameCursorElement)UIManager.GetElement<GameCursorElement>()).GetShape()))
                    {
                        locked = this;
                    }
                }
            }

            if (locked == this)
            {
                if (Alpha <= 0f || !buttonLeft)
                {
                    locked = null;
                }
            }

            UpdateBall();
        }

        private void UpdateBall()
        {
            int width = Main.TextureLibrary.UI_SLIDER_BAR.Asset.Width - Main.TextureLibrary.UI_SLIDER_BALL.Asset.Width;

            float rawValue = option.value - option.ValueMin;
            float rawSpan = option.ValueMax - option.ValueMin;

            if (locked == this)
            {
                float mouse = MathHelper.Clamp(InputManager.GetMousePositionUI().X, Position.X - (width / 2f), Position.X + (width / 2f));
                float valuePrevious = option.value;

                option.value = ((mouse - (Position.X - (width / 2f))) / (float)width) * rawSpan;
                option.value += option.ValueMin;

                if (option.ValueRounded)
                {
                    option.value = (float)Math.Round(option.value);
                }

                int valueDisplay = (int)Math.Floor(option.value * 100f);
                int valuePreviousDisplay = (int)Math.Floor(valuePrevious * 100f);

                if (valueDisplay != valuePreviousDisplay)
                {
                    SoundManager.PlaySound(Main.SoundLibrary.UI_SCROLL.Asset, SoundManager.Category.Sound);

                    if (option.ValueRounded)
                    {
                        locked = null;
                    }
                }
            }

            ballPosition = Position + new Vector2(((rawValue / rawSpan) - 0.5f) * width, 0f);
        }
    }
}