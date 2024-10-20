﻿namespace UnderwaterGame.Entities.Particles
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class Blood : ParticleEntity
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.blood, true);
            depth = 0.725f;
            speed = 2f;
            direction = MathHelper.ToRadians(Main.random.Next(360));
        }

        public override void Update()
        {
            UpdateParticle();
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}