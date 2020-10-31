namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Utilities;

    public abstract class ParticleEntity : Entity
    {
        public float speed;

        public float speedChange;

        public float speedMult = 1f;

        public float direction;

        public float directionChange;

        public float angleChange;

        public bool angleRelative = true;

        public Vector2 scaleChange;

        public float scaleMult = 1f;

        public float alphaChange;

        public float alphaMult = 1f;

        protected void UpdateParticle()
        {
            speed += speedChange;
            direction += directionChange;
            angle += angleChange;
            scale += scaleChange;
            alpha += alphaChange;
            speed *= speedMult;
            scale *= scaleMult;
            alpha *= alphaMult;
            if(angleRelative)
            {
                angle = direction;
            }
            if(alpha < 0.01f || scale.X < 0.01f || scale.Y < 0.01f)
            {
                Destroy();
            }
            velocity = MathUtilities.LengthDirection(speed, direction);
        }
    }
}