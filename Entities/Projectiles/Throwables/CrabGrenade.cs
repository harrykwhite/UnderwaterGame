using Microsoft.Xna.Framework;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Entities.Projectiles.Throwables
{
    public class CrabGrenade : ThrowableProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.CrabGrenade);
            depth = 0.7125f;

            angleChange = MathHelper.Pi / 36f;
            angleChangeRelative = true;
            angleRelative = false;

            speed = 6f;
        }

        public override void Update()
        {
            UpdateProjectile();

            position += velocity;

            UpdateWater();

            velocity = Vector2.Zero;
        }

        protected override void Hit()
        {
            base.Hit();
            Explosion.Spawn(damage, position, new Vector2(20f), hitPlayer, hitEnemy);
        }
    }
}
