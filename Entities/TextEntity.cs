namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Utilities;

    public class TextEntity : Entity
    {
        public float speed;

        public float speedAcc = 0.1f;

        public float direction;

        public string text;

        public int lifeMax = 90;

        public override void Draw()
        {
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            if(lifeMax != -1)
            {
                if(life > lifeMax)
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
            }
            if(speed > 0f)
            {
                speed -= Math.Min(speedAcc, speed);
            }
            velocity = MathUtilities.LengthDirection(speed, direction);
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}