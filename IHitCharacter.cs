using UnderwaterGame.Entities;

namespace UnderwaterGame
{
    public interface IHitCharacter
    {
        HitInfo HitCharacter(Entity target);
    }
}