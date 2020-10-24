using Microsoft.Xna.Framework;
using UnderwaterGame.Sprites;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Entities.Particles
{
    class Smoke : ParticleEntity
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.Smoke);
            depth = 0.725f;

            speed = RandomUtilities.Range(0.5f, 1f);
            speedMult = 0.95f;

            direction = MathHelper.ToRadians(Main.Random.Next(360));

            angle = MathHelper.ToRadians(Main.Random.Next(360));
            angleRelative = false;
        }

        public override void Update()
        {
            if (life > 30)
            {
                scaleMult = 0.9f;
            }

            UpdateParticle();

            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}
