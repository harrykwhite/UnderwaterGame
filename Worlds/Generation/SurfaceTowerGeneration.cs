namespace UnderwaterGame.Worlds.Generation
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Tiles;

    public class SurfaceTowerGeneration : WorldGeneration
    {
        public override void Generate()
        {
            Point[] towerPositions = new Point[3];
            int towerPositionsGap = 128;
            for(int i = 0; i < towerPositions.Length; i++)
            {
                int levelWidth = Main.random.Next(8, 10);
                int levelHeight = levelWidth * 2;
                int levelCount = Main.random.Next(6, 8);
                int levelOffset = Main.random.Next(2) - levelCount + 1;
                int topOffset = Main.random.Next(3);
                int topOffsetMax = 2;
                int topInterval = 2;
                int[] tops = new int[levelWidth + 1];
                for(int t = 0; t < tops.Length; t++)
                {
                    if(t > 0 && t % topInterval == 0)
                    {
                        int offset = topOffset;
                        do
                        {
                            offset += Main.random.Next(2) == 0 ? 1 : -1;
                        } while(offset == topOffset || offset < 0 || offset > topOffsetMax);
                        topOffset = offset;
                    }
                    tops[t] = topOffset;
                }
                do
                {
                    towerPositions[i].X = Main.random.Next(World.width);
                    towerPositions[i].Y = 0;
                    while(World.GetTileAt(towerPositions[i].X, towerPositions[i].Y, World.Tilemap.FirstSolids) == null)
                    {
                        towerPositions[i].Y++;
                    }
                } while(!ValidTowerPosition(i));
                for(int l = 0; l < levelCount; l++)
                {
                    int xStart = towerPositions[i].X - (levelWidth / 2);
                    int yStart = towerPositions[i].Y - (levelHeight * (l + levelOffset + 1));
                    int xEnd = towerPositions[i].X + (levelWidth / 2);
                    int yEnd = towerPositions[i].Y - (levelHeight * (l + levelOffset)) - 1;
                    int gapSize = 2;
                    int gapOffset = Main.random.Next(-1, 2);
                    for(int y = yStart; y <= yEnd; y++)
                    {
                        for(int x = xStart; x <= xEnd; x++)
                        {
                            if(y < yStart + tops[x - xStart] && l == levelCount - 1)
                            {
                                continue;
                            }
                            World.RemoveTileAt(x, y, World.Tilemap.FirstSolids);
                            World.RemoveTileAt(x, y, World.Tilemap.FirstWalls);
                            if(x == xStart || y == yStart || x == xEnd || (y == yEnd && l == 0))
                            {
                                bool placeTile = true;
                                if(y == yStart)
                                {
                                    if(l < levelCount - 1)
                                    {
                                        if(x >= ((xStart + xEnd - gapSize) / 2) + gapOffset && x <= ((xStart + xEnd + gapSize) / 2) + gapOffset)
                                        {
                                            placeTile = false;
                                        }
                                    }
                                    else
                                    {
                                        if(x > xStart && x < xEnd)
                                        {
                                            placeTile = false;
                                        }
                                    }
                                }
                                if(placeTile)
                                {
                                    World.AddTileAt(x, y, World.Tilemap.FirstSolids, Tile.brick);
                                }
                            }
                            World.AddTileAt(x, y, World.Tilemap.FirstWalls, Tile.brickWall);
                        }
                    }
                }
            }
            bool ValidTowerPosition(int index)
            {
                for(int i = index - 1; i >= 0; i--)
                {
                    if(Math.Abs(towerPositions[i].X - towerPositions[index].X) < towerPositionsGap)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}