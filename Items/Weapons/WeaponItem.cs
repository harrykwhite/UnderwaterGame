namespace UnderwaterGame.Items.Weapons
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Projectiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Utilities;

    public abstract class WeaponItem : Item
    {
        public float damage;

        protected void Shoot<T>(ItemEntity entity, float direction, float outLength) where T : ProjectileEntity
        {
            ProjectileEntity projectile = (ProjectileEntity)EntityManager.AddEntity<T>(entity.position);
            projectile.position += MathUtilities.LengthDirection(outLength, direction);
            projectile.direction = direction;
            projectile.angle = projectile.direction;
            projectile.hitEnemy = true;
            projectile.damage += damage;
            projectile.depth = entity.depth - 0.001f;
            ((GameCursorElement)UiManager.GetElement<GameCursorElement>()).Expand(0.5f);
            Camera.Shake(1f, projectile.direction);
        }
    }
}