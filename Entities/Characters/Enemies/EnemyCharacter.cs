namespace UnderwaterGame.Entities.Characters.Enemies
{
    public abstract class EnemyCharacter : CharacterEntity, IHitCharacter
    {
        public float touchDamage;

        public bool touchDamageEnemy;

        public bool touchDamagePlayer;

        public HitData HitCharacter(Entity target)
        {
            return new HitData { damage = touchDamage, at = target.position, direction = null, hitPlayer = touchDamagePlayer, hitEnemy = touchDamageEnemy };
        }
    }
}