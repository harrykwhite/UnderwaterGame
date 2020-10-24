using UnderwaterGame.Utilities;

namespace UnderwaterGame.Entities.Characters.Enemies
{
    public abstract class EnemyCharacter : CharacterEntity, IHitCharacter
    {
        public float TouchDamage { get; protected set; }
        public bool TouchDamageEnemy { get; protected set; }
        public bool TouchDamagePlayer { get; protected set; }

        public HitInfo HitCharacter(Entity target)
        {
            HitInfo hitInfo = new HitInfo
            {
                damage = TouchDamage,
                at = target.position,
                direction = MathUtilities.PointDirection(position, target.position),

                hitPlayer = TouchDamagePlayer,
                hitEnemy = TouchDamageEnemy
            };

            return hitInfo;
        }
    }
}
