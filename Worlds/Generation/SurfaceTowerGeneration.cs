using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Tiles;

namespace UnderwaterGame.Worlds.Generation
{
    public class SurfaceTowerGeneration : WorldGeneration
    {
        private Point[] towerPositions = new Point[3];
        private int towerPositionsGap = 128;

        public override void Generate(World world)
        {
            for (int i = 0; i < towerPositions.Length; i++)
            {
                int levelWidth = Main.Random.Next(8, 10);
                int levelHeight = levelWidth * 2;

                int levelCount = Main.Random.Next(6, 8);
                int levelOffset = Main.Random.Next(2) - levelCount + 1;

                int topOffset = Main.Random.Next(3);
                int topOffsetMax = 2;

                int topInterval = 2;

                int[] tops = new int[levelWidth + 1];

                for (int t = 0; t < tops.Length; t++)
                {
                    if (t > 0 && t % topInterval == 0)
                    {
                        int offset = topOffset;

                        do
                        {
                            offset += Main.Random.Next(2) == 0 ? 1 : -1;
                        } while (offset == topOffset || offset < 0 || offset > topOffsetMax);

                        topOffset = offset;
                    }

                    tops[t] = topOffset;
                }

                do
                {
                    towerPositions[i].X = Main.Random.Next(levelWidth / 2, world.Width - (levelWidth / 2));
                    towerPositions[i].Y = 0;

                    while (world.GetTileAt(towerPositions[i].X, towerPositions[i].Y, World.TilemapType.Solids) == null)
                    {
                        towerPositions[i].Y++;
                    }
                } while (!ValidTowerPosition(i));

                for (int l = 0; l < levelCount; l++)
                {
                    int xStart = towerPositions[i].X - (levelWidth / 2);
                    int yStart = towerPositions[i].Y - (levelHeight * (l + levelOffset + 1));

                    int xEnd = towerPositions[i].X + (levelWidth / 2);
                    int yEnd = towerPositions[i].Y - (levelHeight * (l + levelOffset)) - 1;

                    int gapSize = 2;
                    int gapOffset = Main.Random.Next(-1, 2);

                    for (int y = yStart; y <= yEnd; y++)
                    {
                        for (int x = xStart; x <= xEnd; x++)
                        {
                            if (y < yStart + tops[x - xStart] && l == levelCount - 1)
                            {
                                continue;
                            }

                            world.RemoveTileAt(x, y, World.TilemapType.Solids);
                            world.RemoveTileAt(x, y, World.TilemapType.Walls);

                            if (x == xStart || y == yStart || x == xEnd || (y == yEnd && l == 0))
                            {
                                bool placeTile = true;

                                if (y == yStart)
                                {
                                    if (l < levelCount - 1)
                                    {
                                        if (x >= ((xStart + xEnd - gapSize) / 2) + gapOffset && x <= ((xStart + xEnd + gapSize) / 2) + gapOffset)
                                        {
                                            placeTile = false;
                                        }
                                    }
                                    else
                                    {
                                        if (x > xStart && x < xEnd)
                                        {
                                            placeTile = false;
                                        }
                                    }
                                }

                                if (placeTile)
                                {
                                    world.AddTileAt(x, y, World.TilemapType.Solids, Tile.Brick);
                                }
                            }

                            world.AddTileAt(x, y, World.TilemapType.Walls, Tile.BrickWall);
                        }
                    }
                }
            }
        }

        private bool ValidTowerPosition(int index)
        {
            for (int i = index - 1; i >= 0; i--)
            {
                if (Math.Abs(towerPositions[i].X - towerPositions[index].X) < towerPositionsGap)
                {
                    return false;
                }
            }

            return true;
        }
    }
}