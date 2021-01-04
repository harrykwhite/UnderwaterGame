namespace UnderwaterGame.Generation
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Characters.Enemies.Jellyfish;
    using UnderwaterGame.Entities.Characters.Enemies.Sharks;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Worlds;

    public class HotspotGeneration : Generation
    {
        public override void Generate()
        {
            int count = 4;
            for(int i = 0; i < count; i++)
            {
                World.hotspots.Add(new Hotspot(new Vector2(((World.width * Tile.size) / (count + 1f)) * (i + 1f), 65f * Tile.size), new Spawn[3] { new Spawn(typeof(Jellyfish).FullName, 0.5f), new Spawn(typeof(TallJellyfish).FullName, 0.1f), new Spawn(typeof(Shark).FullName, 0.1f) }));
            }
        }
    }
}