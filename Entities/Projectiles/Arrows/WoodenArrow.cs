namespace UnderwaterGame.Entities.Projectiles.Arrows
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class WoodenArrow : ArrowProjectile
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.woodenArrow, true);
            depth = 0.7125f;
            speed = 8f;
            bloodParticleColor = new Color(117, 72, 72);
        }

        public override void Update()
        {
            UpdateProjectile();
            velocity = Vector2.Zero;
        }
    }
}