namespace UnderwaterGame.Entities.Particles
{
    using System;
    using UnderwaterGame.Utilities;

    public abstract class ParticleEntity : Entity
    {
        public float speed;

        public float speedAcc = 0.1f;

        public float direction;

        public float alphaAcc = 0.1f;

        protected void UpdateParticle()
        {
            if(life > 30)
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
            velocity = MathUtilities.LengthDirection(speed, direction);
        }
    }
}