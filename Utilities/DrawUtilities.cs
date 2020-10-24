using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UnderwaterGame.Utilities
{
    public static class DrawUtilities
    {
        public class Text
        {
            public string String { get; private set; }
            public string[] StringLines => String.Split('\n');

            public Text(string str)
            {
                String = str;
            }
        }

        public enum HAlign
        {
            Left,
            Middle,
            Right
        }

        public enum VAlign
        {
            Top,
            Middle,
            Bottom
        }

        public static float StringScale => 0.5f;

        public static Vector2 MeasureString(SpriteFont font, string text)
        {
            return font.MeasureString(text) * StringScale;
        }

        public static void DrawString(SpriteFont font, Text text, Vector2 at, Color color, HAlign halign, VAlign valign)
        {
            Vector2 totalSize = font.MeasureString(text.String);
            string[] lines = text.StringLines;

            foreach (string line in lines)
            {
                Vector2 lineSize = font.MeasureString(line);
                Vector2 origin = Vector2.Zero;

                switch (halign)
                {
                    case HAlign.Middle:
                        origin.X += lineSize.X / 2f;
                        break;

                    case HAlign.Right:
                        origin.X += lineSize.X;
                        break;
                }

                switch (valign)
                {
                    case VAlign.Middle:
                        origin.Y += totalSize.Y / 2f;
                        break;

                    case VAlign.Bottom:
                        origin.Y += totalSize.Y;
                        break;
                }

                Main.SpriteBatch.DrawString(font, line, at, color, 0f, origin, StringScale, SpriteEffects.None, 1f);
                at.Y += lineSize.Y * StringScale;
            }
        }

        public static void DrawStringExt(SpriteFont font, Text text, Vector2 at, Color color, float rotation, Vector2 scale, HAlign halign, VAlign valign)
        {
            Vector2 totalSize = font.MeasureString(text.String);
            string[] lines = text.StringLines;

            foreach (string line in lines)
            {
                Vector2 lineSize = font.MeasureString(line);
                Vector2 origin = Vector2.Zero;

                switch (halign)
                {
                    case HAlign.Middle:
                        origin.X += lineSize.X / 2f;
                        break;

                    case HAlign.Right:
                        origin.X += lineSize.X;
                        break;
                }

                switch (valign)
                {
                    case VAlign.Middle:
                        origin.Y += totalSize.Y / 2f;
                        break;

                    case VAlign.Bottom:
                        origin.Y += totalSize.Y;
                        break;
                }

                Main.SpriteBatch.DrawString(font, line, at, color, rotation, origin, scale * StringScale, SpriteEffects.None, 1f);
                at.Y += lineSize.Y * StringScale;
            }
        }

        public static void DrawBar(Shape shape, float value, Color frontCol, Color backColor, int direction)
        {
            Vector2 position = shape.position;
            Vector2 scale = new Vector2(shape.width, shape.height);

            Vector2 modifiedPosition = position;
            Vector2 modifiedScale = scale;

            if (direction == 0 || direction == 2)
            {
                modifiedScale.X = scale.X * value;
            }

            if (direction == 1 || direction == 3)
            {
                modifiedScale.Y = scale.Y * value;
            }

            if (direction == 1)
            {
                modifiedPosition.Y += (float)shape.height * (1f - value);
            }

            if (direction == 2)
            {
                modifiedPosition.X += (float)shape.width * (1f - value);
            }

            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, position, null, backColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, modifiedPosition, null, frontCol, 0f, Vector2.Zero, modifiedScale, SpriteEffects.None, 1f);
        }

        public static void DrawBarChange(Shape shape, float value, float changeValue, Color frontColor, Color backColor, Color changeColor, int direction)
        {
            Vector2 position = shape.position;
            Vector2 scale = new Vector2(shape.width, shape.height);

            Vector2 modifiedPosition = position;
            Vector2 modifiedScale = scale;

            Vector2 modifiedChangePosition = position;
            Vector2 modifiedChangeScale = scale;

            if (direction == 0 || direction == 2)
            {
                modifiedScale.X = scale.X * value;
                modifiedChangeScale.X = scale.X * changeValue;
            }

            if (direction == 1 || direction == 3)
            {
                modifiedScale.Y = scale.Y * value;
                modifiedChangeScale.Y = scale.Y * changeValue;
            }

            if (direction == 1)
            {
                modifiedPosition.Y += (float)shape.height * (1f - value);
                modifiedChangePosition.Y += (float)shape.height * (1f - changeValue);
            }

            if (direction == 2)
            {
                modifiedPosition.X += (float)shape.width * (1f - value);
                modifiedChangePosition.X += (float)shape.width * (1f - changeValue);
            }

            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, position, null, backColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, modifiedChangePosition, null, changeColor, 0f, Vector2.Zero, modifiedChangeScale, SpriteEffects.None, 1f);
            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, modifiedPosition, null, frontColor, 0f, Vector2.Zero, modifiedScale, SpriteEffects.None, 1f);
        }
    }
}