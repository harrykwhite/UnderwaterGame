using UnderwaterGame.Entities.Characters.Enemies.Jellyfish;

namespace UnderwaterGame.Worlds.Areas
{
    public class SurfaceArea : WorldArea
    {
        protected override void Init()
        {
            SpawnPool.Add(new SpawnData(typeof(Jellyfish), 0.5f));
            SpawnPool.Add(new SpawnData(typeof(TallJellyfish), 0.25f));

            SpawnMax = 2;
            SpawnTimeMax = 60 * 15;
        }
    }
}