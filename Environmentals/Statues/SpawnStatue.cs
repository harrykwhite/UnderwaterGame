namespace UnderwaterGame.Environmentals.Statues
{
    using UnderwaterGame.Sprites;
    
    public class SpawnStatue : StatueEnvironmental
    {
        public override void Init()
        {
            sprite = Sprite.spawnStatue;
        }
    }
}