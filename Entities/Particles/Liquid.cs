namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

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
            speed = 3.5f;
            direction = -MathHelper.Pi / 2f;
        }

        public override void Update()
        {
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