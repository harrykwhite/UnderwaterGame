namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Tiles;

    public class SurfaceTerrainGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int waterLevel = 32;
            int wallLevel = waterLevel + 96;
            int sandLevel = wallLevel + 2;
            int stoneLevel = sandLevel + 8;
            int levelOffset = 0;
            int levelOffsetMax = 4;
            int levelInterval = 0;
            int levelIntervalMax = 8;
            int levelIntervalMaxOffset = 2;
            int levelIntervalCount = 0;
            for(int x = 0; x < World.width; x++)
            {
                if(levelInterval > 0)
                {
                    levelInterval--;
                }
                else
                {
                    int offset = levelOffset;
                    do
                    {
                        offset += Main.random.Next(2) == 0 ? 1 : -1;
                    } while(offset == levelOffset || offset < -levelOffsetMax || offset > levelOffsetMax);
                    levelOffset = offset;
                    levelIntervalCount++;
                    levelInterval = levelIntervalMax + Main.random.Next(-levelIntervalMaxOffset, levelIntervalMaxOffset);
                }
                for(int y = 0; y < World.height; y++)
                {
                    if(y >= stoneLevel + levelOffset)
                    {
                        World.AddTileAt(x, y, World.Tilemap.Solids, Tile.stone);
                        World.AddTileAt(x, y, World.Tilemap.Walls, Tile.stoneWall);
                    }
                    else if(y >= sandLevel + levelOffset)
                    {
                        World.AddTileAt(x, y, World.Tilemap.Solids, Tile.sand);
                        World.AddTileAt(x, y, World.Tilemap.Walls, Tile.sandWall);
                    }
                    else if(y >= wallLevel + levelOffset)
                    {
                        World.AddTileAt(x, y, World.Tilemap.Walls, Tile.sandWall);
                    }
                    if(y >= waterLevel)
                    {
                        World.AddTileAt(x, y, World.Tilemap.Liquids, Tile.water);
                    }
                }
            }
        }
    }
}