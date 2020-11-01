namespace UnderwaterGame.Entities.Projectiles.Throwables
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Utilities;

    public class CrabGrenade : ThrowableProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.crabGrenade);
            depth = 0.7125f;
            angleAcc = MathHelper.Pi / 36f;
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
            int particleCount = 5;
            for(int i = 0; i < particleCount; i++)
            {
                Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(position);
                smoke.direction = ((MathHelper.Pi * 2f) / particleCount) * i;
            }
            HitEntity hitEntity = (HitEntity)EntityManager.AddEntity<HitEntity>(position);
            hitEntity.SetHitInfo(damage, hitEntity.position, RandomUtilities.Range(0f, MathHelper.Pi), hitPlayer, hitEnemy);
            hitEntity.SetScaleInfo(20, 20);
            Camera.Shake(2f, position);
        }
    }
}