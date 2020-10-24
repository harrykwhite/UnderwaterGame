using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UnderwaterGame.Tiles;
using UnderwaterGame.Utilities;
using UnderwaterGame.Worlds;

namespace UnderwaterGame
{
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

        public static byte[] Data { get; private set; }
        public static List<Source> Sources { get; private set; }

        public static int Width => (Camera.Width / Tile.Size) + (Offset * 2);
        public static int Height => (Camera.Height / Tile.Size) + (Offset * 2);
        public static int Offset => 2;
        public static int Jump => 51;

        public static void Init()
        {
            Data = new byte[Width * Height];
            Sources = new List<Source>();
        }

        public static void Draw()
        {
            Vector2 offset = GetOffset();

            if (Data.Length != Width * Height)
            {
                Data = new byte[Width * Height];
            }

            Clear();

            for (int i = 0; i < Data.Length; i++)
            {
                int x = i % Width;
                int y = i / Width;

                if (Data[i] > 0)
                {
                    Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, offset + (new Vector2(x, y) * Tile.Size), null, Color.Black * ((float)Data[i] / 255f), 0f, Vector2.Zero, Tile.Size, SpriteEffects.None, 1f);
                }
            }
        }

        private static void Clear()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = 0;
            }

            AddTiles();
            AddSources();

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = (byte)((Data[i] / Jump) * Jump);
            }
        }

        private static void AddTiles()
        {
            Vector2 offset = GetOffset();

            for (int i = 0; i < Data.Length; i++)
            {
                int x = i % Width;
                int y = i / Width;

                int tx = (int)(offset.X / Tile.Size) + x;
                int ty = (int)(offset.Y / Tile.Size) + y;

                WorldTile worldTile = Main.World.GetTileAt(tx, ty, World.TilemapType.Solids);

                if (worldTile != null)
                {
                    Data[i] = (byte)MathUtilities.Clamp(Data[i] + worldTile.lighting, 0f, 255f);
                }
            }
        }

        private static void AddSources()
        {
            Vector2 offset = GetOffset();

            List<Source> sourcesCheck = Sources.FindAll(delegate (Source source)
            {
                int x = (int)((source.position.X - offset.X) / Tile.Size);
                int y = (int)((source.position.Y - offset.Y) / Tile.Size);
                int r = (int)(source.radius / Tile.Size) + 1;

                return x + r > 0 && y + r > 0 && x - r < Width - 1 && y - r < Height - 1;
            });

            for (int i = 0; i < Data.Length; i++)
            {
                int x = i % Width;
                int y = i / Width;

                foreach (Source sourceCheck in sourcesCheck)
                {
                    float distance = Vector2.Distance(offset + (new Vector2(x, y) * Tile.Size), sourceCheck.position);

                    if (distance <= sourceCheck.radius)
                    {
                        Data[i] = (byte)MathUtilities.Clamp(Data[i] * (distance / sourceCheck.radius), 0f, 255f);
                    }
                }
            }
        }

        private static Vector2 GetOffset()
        {
            Vector2 offset = Camera.position - (new Vector2(Camera.Width, Camera.Height) / 2f);

            offset.X = (int)((offset.X / Tile.Size) - Offset) * Tile.Size;
            offset.Y = (int)((offset.Y / Tile.Size) - Offset) * Tile.Size;

            return offset;
        }
    }
}