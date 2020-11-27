using UnderwaterGame.Tiles;

namespace UnderwaterGame.Worlds.Generation
{
    public class CloudGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int levelOffset = 8;
            int levelOffsetMin = 6;
            int levelOffsetMax = 10;
            int levelInterval = 0;
            int levelIntervalMax = 4;
            for(int x = 0; x < World.width; x++)
            {
                if(levelInterval < levelIntervalMax)
                {
                    levelInterval++;
                }
                else
                {
                    int offset = levelOffset;
                    do
                    {
                        offset += Main.random.Next(2) == 0 ? 1 : -1;
                    } while(offset == levelOffset || offset < levelOffsetMin || offset > levelOffsetMax);
                    levelOffset = offset;
                    levelInterval = 0;
                }
                for(int y = 0; y < levelOffset; y++)
                {
                    World.AddTileAt(x, y, World.Tilemap.SecondSolids, Tile.cloud);
                }
            }
        }
    }
}