namespace UnderwaterGame.Worlds.Generation
{
    using UnderwaterGame.Entities.Characters.Enemies.Jellyfish;

    public class HotspotGeneration : WorldGeneration
    {
        public override void Generate()
        {
            World.AddHotspotAt(0, 0, World.width, World.height, new string[2] { typeof(Jellyfish).FullName, typeof(TallJellyfish).FullName });
        }
    }
}