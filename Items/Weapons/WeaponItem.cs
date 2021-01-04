namespace UnderwaterGame.Items.Weapons
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Projectiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class WeaponItem : Item
    {
        public int damage;

        public float strength;

        protected void Shoot<T>(float direction, float outLength) where T : ProjectileEntity
        {
            ProjectileEntity projectile = (ProjectileEntity)EntityManager.AddEntity<T>(World.player.heldItem.position);
            projectile.position += MathUtilities.LengthDirection(outLength, direction);
            projectile.direction = direction;
            projectile.angle = projectile.direction;
            projectile.damage = damage;
            projectile.strength = strength;
            projectile.hitEnemy = true;
            projectile.depth = World.player.heldItem.depth - 0.001f;
            World.player.knockbackSpeed += useStrength;
            World.player.knockbackDirection = World.player.heldItem.angleBase;
            ((CursorElement)UiManager.GetElement<CursorElement>()).scale += new Vector2(0.5f);
            Camera.Shake(1f, projectile.direction);
        }
    }
}