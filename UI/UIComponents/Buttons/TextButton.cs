namespace UnderwaterGame.Ui.UiComponents.Buttons
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Utilities;

    public class TextButton : ButtonComponent
    {
        public Func<string> getText;

        public SpriteFont font;

        public DrawUtilities.HorizontalAlign horizontalAlign = DrawUtilities.HorizontalAlign.Left;

        public DrawUtilities.VerticalAlign verticalAlign = DrawUtilities.VerticalAlign.Top;

        public override void Draw()
        {
            DrawUtilities.DrawStringExt(font, new DrawUtilities.Text(getText()), getPosition(), Color.White * getAlpha(), 0f, scale, horizontalAlign, verticalAlign);
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
            if(!CanTouch)
            {
                return false;
            }
            Vector2 size = DrawUtilities.MeasureString(font, getText());
            Shape shape = new Shape(Shape.Fill.Rectangle, (int)size.X, (int)size.Y);
            switch(horizontalAlign)
            {
                case DrawUtilities.HorizontalAlign.Left:
                    shape.position.X = (int)getPosition().X;
                    break;

                case DrawUtilities.HorizontalAlign.Middle:
                    shape.position.X = (int)(getPosition().X - (size.X / 2f));
                    break;

                case DrawUtilities.HorizontalAlign.Right:
                    shape.position.X = (int)(getPosition().X - size.X);
                    break;
            }
            switch(verticalAlign)
            {
                case DrawUtilities.VerticalAlign.Top:
                    shape.position.Y = (int)getPosition().Y;
                    break;

                case DrawUtilities.VerticalAlign.Middle:
                    shape.position.Y = (int)(getPosition().Y - (size.Y / 2f));
                    break;

                case DrawUtilities.VerticalAlign.Bottom:
                    shape.position.Y = (int)(getPosition().Y - size.Y);
                    break;
            }
            return shape.Intersects(((GameCursorElement)UiManager.GetElement<GameCursorElement>()).GetShape());
        }
    }
}