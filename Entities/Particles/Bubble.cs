namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class Bubble : ParticleEntity
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.bubble0);
            depth = 0.725f;
            speed = 4f;
            speedMult = 0.95f;
            direction = MathHelper.ToRadians(Main.random.Next(360));
        }

        public override void Update()
        {
            if(life > 30)
            {
                scaleMult = 0.9f;
            }
            UpdateParticle();
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}