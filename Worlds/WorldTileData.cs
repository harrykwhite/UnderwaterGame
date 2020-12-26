namespace UnderwaterGame.Worlds
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Tiles;

    public class WorldTileData
    {
        public WorldTile worldTile;

        public int x;

        public int y;

        public World.Tilemap tilemap;

        public Shape shape;

        public WorldTileData(WorldTile worldTile, int x, int y, World.Tilemap tilemap)
        {
            this.worldTile = worldTile;
            this.x = x;
            this.y = y;
            this.tilemap = tilemap;
            Shape.Fill fill = Shape.Fill.Rectangle;
            switch(this.worldTile.texture)
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
            shape = new Shape(fill, Tile.size, Tile.size) { position = new Vector2(x, y) * Tile.size };
        }
    }
}