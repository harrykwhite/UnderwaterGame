namespace UnderwaterGame
{
    using UnderwaterGame.Entities;

    public interface IHitCharacter
    {
        HitData HitCharacter(Entity target);
    }
}