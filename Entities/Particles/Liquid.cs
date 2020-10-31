namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class Liquid : ParticleEntity
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.liquid);
            depth = 0.725f;
            speed = RandomUtilities.Range(3.5f, 4f);
            direction = -MathHelper.Pi / 2f;
        }

        public override void Update()
        {
            if(life > 30)
            {
                scaleMult = 0.9f;
            }
            UpdateParticle();
            UpdateGravity();
            velocity.Y += gravity;
            if(velocity.Y >= 0f)
            {
                if(TileCollision(position + velocity, World.Tilemap.Liquids))
                {
                    Destroy();
                }
            }
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}