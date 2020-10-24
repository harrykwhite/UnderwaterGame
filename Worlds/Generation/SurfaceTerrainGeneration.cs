using UnderwaterGame.Tiles;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Worlds.Generation
{
    public class SurfaceTerrainGeneration : WorldGeneration
    {
        public override void Generate(World world)
        {
            int waterLevel = 45;
            int wallLevel = waterLevel + 62;
            int sandLevel = wallLevel + 3;
            int stoneLevel = sandLevel + 10;

            int levelOffset = 0;
            int levelOffsetMax = 8;

            int levelInterval = 8;
            float levelVariance = 0.2f;

            for (int x = 0; x < world.Width; x++)
            {
                if (x > 0 && x % levelInterval == 0)
                {
                    if (RandomUtilities.Chance(levelVariance))
                    {
                        int offset = levelOffset;

                        do
                        {
                            offset += Main.Random.Next(2) == 0 ? 1 : -1;
                        } while (offset == levelOffset || offset < -levelOffsetMax || offset > levelOffsetMax);

                        levelOffset = offset;
                    }
                }

                for (int y = 0; y < world.Height; y++)
                {
                    if (y >= stoneLevel + levelOffset)
                    {
                        world.AddTileAt(x, y, World.TilemapType.Solids, Tile.Stone);
                        world.AddTileAt(x, y, World.TilemapType.Walls, Tile.StoneWall);
                    }
                    else if (y >= sandLevel + levelOffset)
                    {
                        world.AddTileAt(x, y, World.TilemapType.Solids, Tile.Sand);
                        world.AddTileAt(x, y, World.TilemapType.Walls, Tile.SandWall);
                    }
                    else if (y >= wallLevel + levelOffset)
                    {
                        world.AddTileAt(x, y, World.TilemapType.Walls, Tile.SandWall);
                    }

                    if (y >= waterLevel)
                    {
                        world.AddTileAt(x, y, World.TilemapType.Liquids, Tile.Water);
                    }
                }
            }
        }
    }
}