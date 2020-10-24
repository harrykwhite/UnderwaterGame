using UnderwaterGame.Environmentals;
using UnderwaterGame.Tiles;

namespace UnderwaterGame.Worlds.Generation
{
    public class SurfaceEnvironmentalGeneration : WorldGeneration
    {
        public override void Generate(World world)
        {
            int interval = 0;
            int intervalMax = 2;

            for (int x = 0; x < world.Width; x++)
            {
                if (interval < intervalMax)
                {
                    interval++;
                }
                else
                {
                    for (int y = 0; y < world.Height; y++)
                    {
                        WorldTile worldTile = world.GetTileAt(x, y, World.TilemapType.Solids);

                        if (worldTile?.TileType == Tile.Sand)
                        {
                            Environmental environmental = null;

                            if (Main.Random.Next(6) == 0)
                            {
                                environmental = Environmental.SmallSeaweed;
                            }

                            if (Main.Random.Next(18) == 0)
                            {
                                environmental = Environmental.BigSeaweed;
                            }

                            if (environmental != null)
                            {
                                if (world.AddEnvironmentalAt(x, y, environmental))
                                {
                                    x += (environmental.Sprite.Width / Tile.Size) - 1;
                                    interval = 0;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
}