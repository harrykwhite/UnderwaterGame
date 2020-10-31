namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public static class Lighting
    {
        public class Source
        {
            public Vector2 position;

            public float radius;

            public Source(Vector2 position, float radius)
            {
                this.position = position;
                this.radius = radius;
            }
        }

        public static byte[] data;

        public static List<Source> sources;

        public static int offset = 2;

        public static int jump = 51;

        public static void Init()
        {
            data = new byte[GetWidth() * GetHeight()];
            sources = new List<Source>();
        }

        public static void Draw()
        {
            Vector2 offset = GetOffset();
            if(data.Length != GetWidth() * GetHeight())
            {
                data = new byte[GetWidth() * GetHeight()];
            }
            Clear();
            for(int i = 0; i < data.Length; i++)
            {
                int x = i % GetWidth();
                int y = i / GetWidth();
                if(data[i] > 0)
                {
                    Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, offset + (new Vector2(x, y) * Tile.size), null, Color.Black * (data[i] / 255f), 0f, Vector2.Zero, Tile.size, SpriteEffects.None, 1f);
                }
            }
        }

        private static void Clear()
        {
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
            AddTiles();
            AddSources();
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)((data[i] / jump) * jump);
            }
        }

        private static void AddTiles()
        {
            Vector2 offset = GetOffset();
            for(int i = 0; i < data.Length; i++)
            {
                int x = i % GetWidth();
                int y = i / GetWidth();
                int tx = (int)(offset.X / Tile.size) + x;
                int ty = (int)(offset.Y / Tile.size) + y;
                WorldTile worldTile = World.GetTileAt(tx, ty, World.Tilemap.Solids);
                if(worldTile != null)
                {
                    data[i] = (byte)MathUtilities.Clamp(data[i] + worldTile.lighting, 0f, 255f);
                }
            }
        }

        private static void AddSources()
        {
            Vector2 offset = GetOffset();
            List<Source> sourcesCheck = sources.FindAll(delegate (Source source)
            {
                int x = (int)((source.position.X - offset.X) / Tile.size);
                int y = (int)((source.position.Y - offset.Y) / Tile.size);
                int r = (int)(source.radius / Tile.size) + 1;
                return x + r > 0 && y + r > 0 && x - r < GetWidth() - 1 && y - r < GetHeight() - 1;
            });
            for(int i = 0; i < data.Length; i++)
            {
                int x = i % GetWidth();
                int y = i / GetWidth();
                foreach(Source sourceCheck in sourcesCheck)
                {
                    float distance = Vector2.Distance(offset + (new Vector2(x, y) * Tile.size), sourceCheck.position);
                    if(distance <= sourceCheck.radius)
                    {
                        data[i] = (byte)MathUtilities.Clamp(data[i] * (distance / sourceCheck.radius), 0f, 255f);
                    }
                }
            }
        }

        private static Vector2 GetOffset()
        {
            Vector2 offset = Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f);
            offset.X = (int)((offset.X / Tile.size) - Lighting.offset) * Tile.size;
            offset.Y = (int)((offset.Y / Tile.size) - Lighting.offset) * Tile.size;
            return offset;
        }

        public static int GetWidth()
        {
            return (Camera.GetWidth() / Tile.size) + (offset * 2);
        }

        public static int GetHeight()
        {
            return (Camera.GetHeight() / Tile.size) + (offset * 2);
        }
    }
}