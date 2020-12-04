using System;

namespace UnderwaterGame.Worlds.Generation
{
    public class SurfaceSpawnGeneration : WorldGeneration
    {
        public int width = 24;

        public int height = 4;

        public override void Generate()
        {
            int xStart = (World.width - width) / 2;
            int xEnd = (World.width + width) / 2;
            for(int x = xStart; x <= xEnd; x++)
            {
                int yStart = 0;
                while(World.GetTileAt(x, yStart, World.Tilemap.Solids) == null)
                {
                    yStart++;
                }
                int yEnd = yStart + Math.Min(Math.Min(x - xStart, xEnd - x), height);
                for(int y = yStart; y <= yEnd; y++)
                {
                    World.RemoveTileAt(x, y, World.Tilemap.Solids);
                }
            }
        }
    }
}