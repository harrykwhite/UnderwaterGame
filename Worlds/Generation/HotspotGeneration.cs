namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Entities.Characters.Enemies.Jellyfish;
    using UnderwaterGame.Tiles;

    public class HotspotGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int surfaceCount = 2;
            for(int i = 0; i < surfaceCount; i++)
            {
                World.AddHotspotAt(((World.width * Tile.size) / (surfaceCount + 1f)) * (i + 1f), 52f * Tile.size, new WorldHotspot.Spawn[2] { new WorldHotspot.Spawn(typeof(Jellyfish).FullName, 0.5f), new WorldHotspot.Spawn(typeof(TallJellyfish).FullName, 0.25f) }, 60);
            }
        }
    }
}