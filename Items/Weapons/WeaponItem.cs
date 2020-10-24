using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Projectiles;
using UnderwaterGame.UI;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Items.Weapons
{
    public abstract class WeaponItem : Item
    {
        public float Damage { get; protected set; }

        protected void Shoot<T>(ItemEntity entity, float direction, float outLength) where T : ProjectileEntity
        {
            ProjectileEntity projectile = (ProjectileEntity)EntityManager.AddEntity<T>(entity.position);
            projectile.position += MathUtilities.LengthDirection(outLength, direction);

            projectile.direction = direction;
            projectile.angle = projectile.direction;

            projectile.hitEnemy = true;
            projectile.damage += Damage;

            projectile.depth = entity.depth - 0.001f;

            ((GameCursorElement)UIManager.GetElement<GameCursorElement>()).Expand(0.5f);
            Camera.Shake(1f, projectile.direction);
        }
    }
}