namespace UnderwaterGame.Generation
{
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Worlds;

    public class SeaweedGeneration : Generation
    {
        public override void Generate()
        {
            int intervalMax = 8;
            int interval = intervalMax / 2;
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
                        WorldTile worldTile = World.GetTileAt(x, y, World.Tilemap.Solids);
                        Tile tile = Tile.GetTileById(worldTile?.id ?? 0);
                        if(tile == Tile.sand)
                        {
                            if(World.AddEnvironmentalAt(x, y, Environmental.seaweed))
                            {
                                x += (Environmental.seaweed.sprite.textures[0].Width / Tile.size) - 1;
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