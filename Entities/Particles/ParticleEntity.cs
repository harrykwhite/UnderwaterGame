namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class ParticleEntity : Entity
    {
        public int lifeMax = 30;

        public float speed;

        public float speedAcc = 0.1f;

        public float direction;

        public float alphaAcc = 0.1f;

        public bool water = true;

        public bool stop;
        
        protected void UpdateParticle()
        {
            if(life > lifeMax)
            {
                if(speed > 0f)
                {
                    speed -= Math.Min(speedAcc, speed);
                }
                else
                {
                    if(alpha > 0f)
                    {
                        alpha -= Math.Min(alphaAcc, alpha);
                    }
                    else
                    {
                        Destroy();
                    }
                }
            }
            else
            {
                if(alpha < 1f)
                {
                    alpha += Math.Min(alphaAcc, 1f - alpha);
                }
            }
            if(!stop)
            {
                velocity = MathUtilities.LengthDirection(speed, direction);
                if(!water)
                {
                    UpdateGravity();
                    velocity.Y += gravity;
                }
                for(float i = 0f; i < velocity.Length(); i += Math.Min(1f, velocity.Length() - i))
                {
                    if(TileTypeCollision(position, Tile.water, World.Tilemap.Liquids) ^ water)
                    {
                        life = lifeMax;
                        speed = 0f;
                        gravity = 0f;
                        stop = true;
                        break;
                    }
                    position += Vector2.Normalize(velocity) * Math.Min(1f, velocity.Length() - i);
                }
            }
        }
    }
}