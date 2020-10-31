namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;

    public class SurfaceTerrainGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int waterLevel = 45;
            int wallLevel = waterLevel + 62;
            int sandLevel = wallLevel + 3;
            int stoneLevel = sandLevel + 10;
            int levelOffset = 0;
            int levelOffsetMax = 8;
            int levelInterval = 8;
            float levelVariance = 0.2f;
            for(int x = 0; x < World.width; x++)
            {
                if(x > 0 && x % levelInterval == 0)
                {
                    if(RandomUtilities.Chance(levelVariance))
                    {
                        int offset = levelOffset;
                        do
                        {
                            offset += Main.random.Next(2) == 0 ? 1 : -1;
                        } while(offset == levelOffset || offset < -levelOffsetMax || offset > levelOffsetMax);
                        levelOffset = offset;
                    }
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