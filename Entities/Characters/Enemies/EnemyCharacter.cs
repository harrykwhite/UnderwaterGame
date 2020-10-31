namespace UnderwaterGame.Entities.Characters.Enemies
{
    using UnderwaterGame.Utilities;

    public abstract class EnemyCharacter : CharacterEntity, IHitCharacter
    {
        public float touchDamage;

        public bool touchDamageEnemy;

        public bool touchDamagePlayer;

        public HitInfo HitCharacter(Entity target)
        {
            HitInfo hitInfo = new HitInfo { damage = touchDamage, at = target.position, direction = MathUtilities.PointDirection(position, target.position), hitPlayer = touchDamagePlayer, hitEnemy = touchDamageEnemy };
            return hitInfo;
        }
    }
}