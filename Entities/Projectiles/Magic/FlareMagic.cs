namespace UnderwaterGame.Entities.Projectiles.Magic
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;

    public class FlareMagic : MagicProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.flareMagic, true);
            depth = 0.7125f;
            speed = 4f;
            hasGravity = false;
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
            int particleCount = 2;
            for(int i = 0; i < particleCount; i++)
            {
                Fire fire = (Fire)EntityManager.AddEntity<Fire>(position);
                fire.direction = direction - MathHelper.Pi + ((MathHelper.Pi / 36f) * (i - ((particleCount - 1f) / 2f)));
            }
        }
    }
}