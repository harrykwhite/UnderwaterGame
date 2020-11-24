namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Tiles;

    public class SurfaceEnvironmentalGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int interval = 0;
            int intervalMax = 4;
            int intervalOffset = 2;
            for(int x = 0; x < World.width; x++)
            {
                if(interval > 0)
                {
                    interval--;
                }
                else
                {
                    for(int y = 0; y < World.height; y++)
                    {
                        WorldTile worldTile = World.GetTileAt(x, y, World.Tilemap.FirstSolids);
                        if(Tile.GetTileById(worldTile?.id ?? 0) == Tile.sand)
                        {
                            Environmental environmental = Environmental.smallSeaweed;
                            if(Main.random.Next(4) == 0)
                            {
                                environmental = Environmental.bigSeaweed;
                            }
                            if(World.AddEnvironmentalAt(x, y, environmental))
                            {
                                x += (environmental.sprite.textures[0].Width / Tile.size) - 1;
                            }
                            break;
                        }
                    }
                    interval = intervalMax + Main.random.Next(-intervalOffset, intervalOffset);
                }
            }
        }
    }
}