namespace UnderwaterGame.Worlds.Generation
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Characters.Enemies.Jellyfish;
    using UnderwaterGame.Tiles;

    public class HotspotGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int surfaceCount = 2;
            for(int i = 0; i < surfaceCount; i++)
            {
                World.AddHotspotAt(new Vector2(((World.width * Tile.size) / (surfaceCount + 1f)) * (i + 1f), 56f * Tile.size), new Hotspot.Spawn[2] { new Hotspot.Spawn(typeof(Jellyfish).FullName, 0.5f), new Hotspot.Spawn(typeof(TallJellyfish).FullName, 0.25f) }, 60);
            }
        }
    }
}