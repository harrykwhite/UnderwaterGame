namespace UnderwaterGame.Entities.Projectiles.Throwables
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;

    public class PinkJellyShuriken : ThrowableProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.pinkJellyShuriken, true);
            depth = 0.7125f;
            angleAcc = MathHelper.Pi / 36f;
            angleRelative = false;
            speed = 8f;
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
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.speed = speed / 2f;
                blood.direction = direction - MathHelper.Pi + ((MathHelper.Pi / 12f) * (i - ((particleCount - 1f) / 2f)));
                blood.blend = new Color(216, 143, 172);
            }
        }
    }
}