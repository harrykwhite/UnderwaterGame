namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using UnderwaterGame.Utilities;

    public static class Lighting
    {
        public class Source
        {
            public Vector2 position;

            public int radius;

            public Source(Vector2 position, int radius)
            {
                this.position = position;
                this.radius = radius;
            }
        }

        public static byte[] data;

        public static List<Source> sources;

        public static void Init()
        {
            return;
            data = new byte[Camera.GetWidth() * Camera.GetHeight()];
            sources = new List<Source>();
        }

        public static void Draw()
        {
            return;
            if(data.Length != Camera.GetWidth() * Camera.GetHeight())
            {
                data = new byte[Camera.GetWidth() * Camera.GetHeight()];
            }
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
            for(int i = 0; i < data.Length; i++)
            {
                int x = i % Camera.GetWidth();
                int y = i / Camera.GetWidth();
                foreach(Source source in sources)
                {
                    float distance = Vector2.Distance(Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f) + new Vector2(x, y), source.position);
                    if(distance <= source.radius)
                    {
                        data[i] = (byte)MathUtilities.Clamp(data[i] * (distance / source.radius), 0f, 255f);
                    }
                }
            }
            for(int i = 0; i < data.Length; i++)
            {
                int x = i % Camera.GetWidth();
                int y = i / Camera.GetWidth();
                if(data[i] > 0)
                {
                    Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f) + new Vector2(x, y), null, Color.Black * (data[i] / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                }
            }
        }
    }
}