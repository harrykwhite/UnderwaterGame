using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIComponents.Buttons
{
    public class TextButton : ButtonComponent
    {
        public Func<string> getText;
        public SpriteFont font;

        public DrawUtilities.HAlign hAlign = DrawUtilities.HAlign.Left;
        public DrawUtilities.VAlign vAlign = DrawUtilities.VAlign.Top;

        public override void Draw()
        {
            DrawUtilities.DrawStringExt(font, new DrawUtilities.Text(getText.Invoke()), Position, Color.White * Alpha, 0f, scale, hAlign, vAlign);
        }

        public override void Init()
        {

        }

        public override void Update()
        {
            UpdateButton();
        }

        protected override bool IsTouching()
        {
            if (!CanTouch)
            {
                return false;
            }

            Vector2 size = DrawUtilities.MeasureString(font, getText.Invoke());
            Shape shape = new Shape(Shape.Fill.Rectangle, (int)size.X, (int)size.Y);

            switch (hAlign)
            {
                case DrawUtilities.HAlign.Left:
                    shape.position.X = (int)Position.X;
                    break;

                case DrawUtilities.HAlign.Middle:
                    shape.position.X = (int)(Position.X - (size.X / 2f));
                    break;

                case DrawUtilities.HAlign.Right:
                    shape.position.X = (int)(Position.X - size.X);
                    break;
            }

            switch (vAlign)
            {
                case DrawUtilities.VAlign.Top:
                    shape.position.Y = (int)Position.Y;
                    break;

                case DrawUtilities.VAlign.Middle:
                    shape.position.Y = (int)(Position.Y - (size.Y / 2f));
                    break;

                case DrawUtilities.VAlign.Bottom:
                    shape.position.Y = (int)(Position.Y - size.Y);
                    break;
            }

            return shape.Intersects(((GameCursorElement)UIManager.GetElement<GameCursorElement>()).GetShape());
        }
    }
}