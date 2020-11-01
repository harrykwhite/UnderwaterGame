namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class Jellyfish : JellyfishEnemy
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.jellyfish);
            animator = new Animator(sprite);
            healthMax = 20f;
            health = healthMax;
            touchDamage = 6f;
            touchDamagePlayer = true;
            bloodParticleColor = new Color(216, 143, 172);
            swimPositionToTime = swimPositionToTimeMax;
        }

        public override void Update()
        {
            JellyfishUpdate();
            CheckForDamage(collider);
            UpdateStatus();
            UpdateGravity();
            velocity.Y += gravity;
            TileCollisions();
            position += velocity;
            LockInWorld();
            animator.speed = 0.1f;
            animator.Update();
            UpdateWater();
            velocity = Vector2.Zero;
        }
    }
}