namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Utilities;

    public class Blood : ParticleEntity
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.blood);
            depth = 0.725f;
            speed = RandomUtilities.Range(2.5f, 3f);
            speedMult = 0.95f;
            direction = MathHelper.ToRadians(Main.random.Next(360));
            blend = Color.White;
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