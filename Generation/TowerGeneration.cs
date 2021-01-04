namespace UnderwaterGame.Generation
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Items;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Worlds;

    public class TowerGeneration : Generation
    {
        public Point[] towerPositions = new Point[4];

        public int width = 8;

        public override void Generate()
        {
            int towerPositionsGap = 64;
            for(int i = 0; i < towerPositions.Length; i++)
            {
                int levelHeight = 16;
                int levelCount = Main.random.Next(6, 8);
                int levelOffset = Main.random.Next(2) - levelCount + 1;
                int topOffset = Main.random.Next(3);
                int topOffsetMax = 2;
                int topIntervalMax = 4;
                int topInterval = topIntervalMax / 2;
                int[] tops = new int[width + 1];
                for(int t = 0; t < tops.Length; t++)
                {
                    if(topInterval > 0)
                    {
                        topInterval--;
                    }
                    else
                    {
                        int offset = topOffset;
                        do
                        {
                            offset += Main.random.Next(2) == 0 ? 1 : -1;
                        } while(offset == topOffset || offset < 0 || offset > topOffsetMax);
                        topOffset = offset;
                        topInterval = topIntervalMax;
                    }
                    tops[t] = topOffset;
                }
                do
                {
                    towerPositions[i].X = Main.random.Next(width / 2, World.width - (width / 2));
                    towerPositions[i].Y = 0;
                    while(World.GetTileAt(towerPositions[i].X, towerPositions[i].Y, World.Tilemap.Solids) == null)
                    {
                        towerPositions[i].Y++;
                    }
                } while(!ValidTowerPosition());
                for(int l = 0; l < levelCount; l++)
                {
                    int xStart = towerPositions[i].X - (width / 2);
                    int yStart = towerPositions[i].Y - (levelHeight * (l + levelOffset + 1));
                    int xEnd = towerPositions[i].X + (width / 2);
                    int yEnd = towerPositions[i].Y - (levelHeight * (l + levelOffset)) - 1;
                    int gapSize = 2;
                    int gapOffset = Main.random.Next(3) - 1;
                    for(int y = yStart; y <= yEnd; y++)
                    {
                        for(int x = xStart; x <= xEnd; x++)
                        {
                            if(y < yStart + tops[x - xStart] && l == levelCount - 1)
                            {
                                continue;
                            }
                            World.RemoveTileAt(x, y, World.Tilemap.Solids);
                            World.RemoveTileAt(x, y, World.Tilemap.Walls);
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
                                    World.AddTileAt(x, y, World.Tilemap.Solids, Tile.brick);
                                }
                            }
                            World.AddTileAt(x, y, World.Tilemap.Walls, Tile.brick);
                        }
                    }
                }
                Item item = Item.healthKelp;
                int quantity = 5;
                switch(i)
                {
                    case 1:
                        item = Item.woodenBow;
                        quantity = 1;
                        break;

                    case 2:
                        item = Item.woodenSword;
                        quantity = 1;
                        break;
                }
                ItemDropEntity itemDrop = (ItemDropEntity)EntityManager.AddEntity<ItemDropEntity>(new Vector2((towerPositions[i].X + 0.5f) * Tile.size, (towerPositions[i].Y - (levelHeight * (levelOffset + 0.5f)) + 0.5f) * Tile.size));
                itemDrop.SetItem(item, quantity);
                bool ValidTowerPosition()
                {
                    SpawnGeneration spawnGeneration = (SpawnGeneration)World.generations.Find((Generation generation) => generation is SpawnGeneration);
                    if((towerPositions[i].X >= (World.width - spawnGeneration.width - width) / 2) && (towerPositions[i].X <= (World.width + spawnGeneration.width + width) / 2))
                    {
                        return false;
                    }
                    for(int ii = i - 1; ii >= 0; ii--)
                    {
                        if(Math.Abs(towerPositions[ii].X - towerPositions[i].X) < towerPositionsGap)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
    }
}