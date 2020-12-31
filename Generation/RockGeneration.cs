namespace UnderwaterGame.Generation
{
    using System;
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Worlds;

    public class RockGeneration : Generation
    {
        public override void Generate()
        {
            int[] rockPositions = new int[16];
            int rockPositionsGap = 16;
            for(int i = 0; i < rockPositions.Length; i++)
            {
                int width = Main.random.Next(6, 8);
                do
                {
                    rockPositions[i] = Main.random.Next(width / 2, World.width - (width / 2));
                } while(!ValidRockPosition());
                int xStart = rockPositions[i] - (width / 2);
                int xEnd = rockPositions[i] + (width / 2);
                for(int x = xStart; x <= xEnd; x++)
                {
                    for(int y = 0; y < World.height; y++)
                    {
                        WorldTile worldTile = World.GetTileAt(x, y, World.Tilemap.Solids);
                        Tile tile = Tile.GetTileById(worldTile?.id ?? 0);
                        if(tile == Tile.sand)
                        {
                            Environmental environmental = Environmental.rockSmall;
                            if(x == (xStart + xEnd) / 2)
                            {
                                environmental = Environmental.rockLarge;
                            }
                            bool valid;
                            do
                            {
                                valid = true;
                                if(World.AddEnvironmentalAt(x, y, environmental))
                                {
                                    x += (environmental.sprite.textures[0].Width / Tile.size) - 1;
                                }
                                else if(environmental != Environmental.rockSmall)
                                {
                                    environmental = Environmental.rockSmall;
                                    valid = false;
                                }
                            } while(!valid);
                            break;
                        }
                    }
                }
                bool ValidRockPosition()
                {
                    SpawnGeneration spawnGeneration = (SpawnGeneration)World.generations.Find((Generation generation) => generation is SpawnGeneration);
                    TowerGeneration towerGeneration = (TowerGeneration)World.generations.Find((Generation generation) => generation is TowerGeneration);
                    if((rockPositions[i] >= (World.width - spawnGeneration.width - width) / 2) && (rockPositions[i] <= (World.width + spawnGeneration.width + width) / 2))
                    {
                        return false;
                    }
                    for(int ii = 0; ii < towerGeneration.towerPositions.Length; ii++)
                    {
                        if(Math.Abs(rockPositions[i] - towerGeneration.towerPositions[ii].X) < (width + towerGeneration.width) / 2)
                        {
                            return false;
                        }
                    }
                    for(int ii = i - 1; ii >= 0; ii--)
                    {
                        if(Math.Abs(rockPositions[ii] - rockPositions[i]) < rockPositionsGap)
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