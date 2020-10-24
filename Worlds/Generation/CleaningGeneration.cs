namespace UnderwaterGame.Worlds.Generation
{
    public class CleaningGeneration : WorldGeneration
    {
        public override void Generate(World world)
        {
            for (int y = 0; y < world.Height; y++)
            {
                for (int x = 0; x < world.Width; x++)
                {
                    if (world.GetTileAt(x, y, World.TilemapType.Solids) != null)
                    {
                        if (world.GetTileAt(x - 1, y, World.TilemapType.Solids) == null && world.GetTileAt(x + 1, y, World.TilemapType.Solids) == null && world.GetTileAt(x, y - 1, World.TilemapType.Solids) == null && world.GetTileAt(x, y + 1, World.TilemapType.Solids) == null)
                        {
                            world.RemoveTileAt(x, y, World.TilemapType.Solids);
                        }
                    }
                }
            }
        }
    }
}