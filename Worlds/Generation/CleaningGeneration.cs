namespace UnderwaterGame.Worlds.Generation
{
    public class CleaningGeneration : WorldGeneration
    {
        public override void Generate()
        {
            for(int y = 0; y < World.height; y++)
            {
                for(int x = 0; x < World.width; x++)
                {
                    if(World.GetTileAt(x, y, World.Tilemap.Solids) != null)
                    {
                        if(World.GetTileAt(x - 1, y, World.Tilemap.Solids) == null && World.GetTileAt(x + 1, y, World.Tilemap.Solids) == null && World.GetTileAt(x, y - 1, World.Tilemap.Solids) == null && World.GetTileAt(x, y + 1, World.Tilemap.Solids) == null)
                        {
                            World.RemoveTileAt(x, y, World.Tilemap.Solids);
                        }
                    }
                }
            }
        }
    }
}