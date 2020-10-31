namespace UnderwaterGame.Entities.Projectiles.Throwables
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;

    public class CrabShuriken : ThrowableProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.crabShuriken);
            depth = 0.7125f;
            angleChange = MathHelper.Pi / 36f;
            angleChangeRelative = true;
            angleRelative = false;
            speed = 6f;
            pierce = true;
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
            int particleCount = 3;
            for(int i = 0; i < particleCount; i++)
            {
                CrabShell crabShell = (CrabShell)EntityManager.AddEntity<CrabShell>(position);
                crabShell.direction = direction - MathHelper.Pi + ((MathHelper.Pi / 12f) * (i - ((particleCount - 1f) / 2f)));
            }
        }
    }
}