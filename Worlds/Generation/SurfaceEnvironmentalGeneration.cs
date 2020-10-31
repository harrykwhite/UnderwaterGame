namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Tiles;

    public class SurfaceEnvironmentalGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int interval = 0;
            int intervalMax = 2;
            for(int x = 0; x < World.width; x++)
            {
                if(interval < intervalMax)
                {
                    interval++;
                }
                else
                {
                    for(int y = 0; y < World.height; y++)
                    {
                        WorldTile worldTile = World.GetTileAt(x, y, World.Tilemap.Solids);
                        if(Tile.GetTileById(worldTile.id) == Tile.sand)
                        {
                            Environmental environmental = null;
                            if(Main.random.Next(6) == 0)
                            {
                                environmental = Environmental.smallSeaweed;
                            }
                            if(Main.random.Next(18) == 0)
                            {
                                environmental = Environmental.bigSeaweed;
                            }
                            if(environmental != null)
                            {
                                if(World.AddEnvironmentalAt(x, y, environmental))
                                {
                                    x += (environmental.sprite.textures[0].Width / Tile.size) - 1;
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