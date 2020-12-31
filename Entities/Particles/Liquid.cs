namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class Liquid : ParticleEntity
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.liquid, true);
            depth = 0.725f;
            speed = 3f;
            direction = -MathHelper.Pi / 2f;
            water = false;
        }

        public override void Update()
        {
            UpdateParticle();
            velocity = Vector2.Zero;
        }
    }
}