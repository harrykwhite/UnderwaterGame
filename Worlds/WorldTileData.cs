using Microsoft.Xna.Framework;
using UnderwaterGame.Tiles;

namespace UnderwaterGame.Worlds
{
    public class WorldTileData
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public WorldTile WorldTile { get; private set; }
        public World.TilemapType Tilemap { get; private set; }

        public Shape Shape { get; private set; }

        public WorldTileData(int x, int y, WorldTile worldTile, World.TilemapType tilemap)
        {
            X = x;
            Y = y;

            WorldTile = worldTile;
            Tilemap = tilemap;

            Shape.Fill fill = Shape.Fill.Rectangle;

            switch (WorldTile.texture)
            {
                case 16:
                    fill = Shape.Fill.TopLeftSlope;
                    break;

                case 17:
                    fill = Shape.Fill.TopRightSlope;
                    break;

                case 18:
                    fill = Shape.Fill.BottomLeftSlope;
                    break;

                case 19:
                    fill = Shape.Fill.BottomRightSlope;
                    break;
            }

            Shape = new Shape(fill, Tile.Size, Tile.Size)
            {
                position = new Vector2(X, Y) * Tile.Size
            };
        }
    }
}