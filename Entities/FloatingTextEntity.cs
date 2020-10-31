namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;

    public class FloatingTextEntity : Entity
    {
        public float speed;

        public float direction;

        public string text;

        public override void Draw()
        {
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            if(life >= 90)
            {
                if(alpha > 0f)
                {
                    alpha -= Math.Min(0.1f, alpha);
                }
                else
                {
                    Destroy();
                }
            }
            speed *= 0.9f;
            velocity = MathUtilities.LengthDirection(speed, direction);
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}