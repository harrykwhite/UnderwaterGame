using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Tiles;

namespace UnderwaterGame.Worlds.Generation
{
    public class SurfaceMountainGeneration : WorldGeneration
    {
        public override void Generate()
        {
            Point[] mountainPositions = new Point[12];
            int mountainPositionsGap = 32;
            for(int i = 0; i < mountainPositions.Length; i++)
            {
                int width = Main.random.Next(6, 8);
                int height = Main.random.Next(64, 80);
                do
                {
                    mountainPositions[i].X = Main.random.Next(World.width);
                    mountainPositions[i].Y = 0;
                    while(World.GetTileAt(mountainPositions[i].X, mountainPositions[i].Y, World.Tilemap.FirstSolids) == null)
                    {
                        mountainPositions[i].Y++;
                    }
                } while(!ValidMountainPosition(i));
                int xStart = mountainPositions[i].X - (width / 2);
                int yStart = mountainPositions[i].Y - height;
                int xEnd = mountainPositions[i].X + (width / 2);
                int yEnd = mountainPositions[i].Y;
                int xOffset = 0;
                int xOffsetMax = 8;
                int xOffsetInterval = 0;
                int xOffsetIntervalMax = 4;
                for(int y = yStart; y <= yEnd; y++)
                {
                    if(xOffsetInterval < xOffsetIntervalMax)
                    {
                        xOffsetInterval++;
                    }
                    else
                    {
                        int xOffsetPrevious = xOffset;
                        do
                        {
                            xOffset += Main.random.Next(2) == 0 ? 1 : -1;
                        } while(xOffset == xOffsetPrevious || xOffset < -xOffsetMax || xOffset > xOffsetMax);
                        xOffsetInterval = 0;
                    }
                    for(int x = xStart; x <= xEnd; x++)
                    {
                        World.AddTileAt(x + xOffset, y, World.Tilemap.SecondWalls, Tile.stoneWall);
                    }
                }
            }
            bool ValidMountainPosition(int index)
            {
                for(int i = index - 1; i >= 0; i--)
                {
                    if(Math.Abs(mountainPositions[i].X - mountainPositions[index].X) < mountainPositionsGap)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}