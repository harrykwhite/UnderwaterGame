namespace UnderwaterGame
{
    using UnderwaterGame.Entities;

    public interface IHitCharacter
    {
        HitInfo HitCharacter(Entity target);
    }
}