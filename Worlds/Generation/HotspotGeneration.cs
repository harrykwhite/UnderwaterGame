namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Entities.Characters.Enemies.Jellyfish;

    public class HotspotGeneration : WorldGeneration
    {
        public override void Generate()
        {
            World.AddHotspotAt(0, 0, World.width, World.height, new WorldHotspot.Spawn[2] { new WorldHotspot.Spawn(typeof(Jellyfish).FullName, 0.5f), new WorldHotspot.Spawn(typeof(TallJellyfish).FullName, 0.25f) });
        }
    }
}