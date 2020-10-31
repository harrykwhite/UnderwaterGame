namespace UnderwaterGame.Entities.Projectiles.Arrows
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;

    public class WoodenArrow : ArrowProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.woodenArrow);
            depth = 0.7125f;
            speed = 8f;
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
                Wood wood = (Wood)EntityManager.AddEntity<Wood>(position);
                wood.direction = direction - MathHelper.Pi + ((MathHelper.Pi / 36f) * (i - ((particleCount - 1f) / 2f)));
            }
        }
    }
}