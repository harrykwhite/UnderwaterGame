namespace UnderwaterGame.Utilities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class DrawUtilities
    {
        public class Text
        {
            public string text;

            public Text(string text)
            {
                this.text = text;
            }
        }

        public enum HorizontalAlign
        {
            Left, Middle, Right
        }

        public enum VerticalAlign
        {
            Top, Middle, Bottom
        }

        public static float stringScale = 0.5f;

        public static Vector2 MeasureString(SpriteFont font, string text)
        {
            return font.MeasureString(text) * stringScale;
        }

        public static void DrawString(SpriteFont font, Text text, Vector2 at, Color color, HorizontalAlign horizontalAlign, VerticalAlign verticalAlign)
        {
            Vector2 totalSize = font.MeasureString(text.text);
            string[] lines = text.text.Split('\n');
            foreach(string line in lines)
            {
                Vector2 lineSize = font.MeasureString(line);
                Vector2 origin = Vector2.Zero;
                switch(horizontalAlign)
                {
                    case HorizontalAlign.Middle:
                        origin.X += lineSize.X / 2f;
                        break;

                    case HorizontalAlign.Right:
                        origin.X += lineSize.X;
                        break;
                }
                switch(verticalAlign)
                {
                    case VerticalAlign.Middle:
                        origin.Y += totalSize.Y / 2f;
                        break;

                    case VerticalAlign.Bottom:
                        origin.Y += totalSize.Y;
                        break;
                }
                Main.spriteBatch.DrawString(font, line, at, color, 0f, origin, stringScale, SpriteEffects.None, 1f);
                at.Y += lineSize.Y * stringScale;
            }
        }

        public static void DrawStringExt(SpriteFont font, Text text, Vector2 at, Color color, float rotation, Vector2 scale, HorizontalAlign horizontalAlign, VerticalAlign verticalAlign)
        {
            Vector2 totalSize = font.MeasureString(text.text);
            string[] lines = text.text.Split('\n');
            foreach(string line in lines)
            {
                Vector2 lineSize = font.MeasureString(line);
                Vector2 origin = Vector2.Zero;
                switch(horizontalAlign)
                {
                    case HorizontalAlign.Middle:
                        origin.X += lineSize.X / 2f;
                        break;

                    case HorizontalAlign.Right:
                        origin.X += lineSize.X;
                        break;
                }
                switch(verticalAlign)
                {
                    case VerticalAlign.Middle:
                        origin.Y += totalSize.Y / 2f;
                        break;

                    case VerticalAlign.Bottom:
                        origin.Y += totalSize.Y;
                        break;
                }
                Main.spriteBatch.DrawString(font, line, at, color, rotation, origin, scale * stringScale, SpriteEffects.None, 1f);
                at.Y += lineSize.Y * stringScale;
            }
        }

        public static void DrawBar(Shape shape, float value, Color frontCol, Color backColor, int direction)
        {
            Vector2 position = shape.position;
            Vector2 scale = new Vector2(shape.width, shape.height);
            Vector2 modifiedPosition = position;
            Vector2 modifiedScale = scale;
            if(direction == 0 || direction == 2)
            {
                modifiedScale.X = scale.X * value;
            }
            if(direction == 1 || direction == 3)
            {
                modifiedScale.Y = scale.Y * value;
            }
            if(direction == 1)
            {
                modifiedPosition.Y += shape.height * (1f - value);
            }
            if(direction == 2)
            {
                modifiedPosition.X += shape.width * (1f - value);
            }
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, position, null, backColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, modifiedPosition, null, frontCol, 0f, Vector2.Zero, modifiedScale, SpriteEffects.None, 1f);
        }

        public static void DrawBarChange(Shape shape, float value, float changeValue, Color frontColor, Color backColor, Color changeColor, int direction)
        {
            Vector2 position = shape.position;
            Vector2 scale = new Vector2(shape.width, shape.height);
            Vector2 modifiedPosition = position;
            Vector2 modifiedScale = scale;
            Vector2 modifiedChangePosition = position;
            Vector2 modifiedChangeScale = scale;
            if(direction == 0 || direction == 2)
            {
                modifiedScale.X = scale.X * value;
                modifiedChangeScale.X = scale.X * changeValue;
            }
            if(direction == 1 || direction == 3)
            {
                modifiedScale.Y = scale.Y * value;
                modifiedChangeScale.Y = scale.Y * changeValue;
            }
            if(direction == 1)
            {
                modifiedPosition.Y += shape.height * (1f - value);
                modifiedChangePosition.Y += shape.height * (1f - changeValue);
            }
            if(direction == 2)
            {
                modifiedPosition.X += shape.width * (1f - value);
                modifiedChangePosition.X += shape.width * (1f - changeValue);
            }
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, position, null, backColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, modifiedChangePosition, null, changeColor, 0f, Vector2.Zero, modifiedChangeScale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, modifiedPosition, null, frontColor, 0f, Vector2.Zero, modifiedScale, SpriteEffects.None, 1f);
        }
    }
}