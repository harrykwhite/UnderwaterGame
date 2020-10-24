using Microsoft.Xna.Framework;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    public class TallJellyfish : JellyfishEnemy
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.TallJellyfish);
            Animator = new Animator(Sprite);

            HealthMax = 30f;
            Health = HealthMax;

            TouchDamage = 6f;
            TouchDamagePlayer = true;

            bloodParticleColor = new Color(214, 85, 85);
            swimPositionToTime = swimPositionToTimeMax;

            swimPositionToTimeMax = 60;
            swimTargeted = true;
        }

        public override void Update()
        {
            JellyfishUpdate();

            CheckForDamage(Collider);
            UpdateStatus();

            UpdateGravity();
            velocity.Y += gravity;

            TileCollisions(Vector2.Zero);

            position += velocity;
            LockInWorld();

            Animator.speed = 0.1f;
            Animator.Update();

            UpdateWater();

            velocity = Vector2.Zero;
        }
    }
}
