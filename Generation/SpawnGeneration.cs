using System;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Generation
{
    public class SpawnGeneration : Generation
    {
        public int width = 32;

        public int height = 8;

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