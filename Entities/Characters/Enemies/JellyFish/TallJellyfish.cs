namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;

    public class TallJellyfish : JellyfishEnemy
    {
        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.tallJellyfish);
            animator = new Animator(sprite);
            healthMax = 30f;
            health = healthMax;
            touchDamage = 6f;
            touchDamagePlayer = true;
            bloodParticleColor = new Color(164, 132, 183);
            swimPositionToTime = swimPositionToTimeMax;
            swimPositionToTimeMax = 60;
            swimTargeted = true;
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