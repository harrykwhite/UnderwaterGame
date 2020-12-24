namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Items;
    using UnderwaterGame.Sprites;

    public class Jellyfish : JellyfishEnemy
    {
        public override void Draw()
        {
            DrawSelf();
            DrawFlash();
        }

        public override void Init()
        {
            SetSprite(Sprite.jellyfish, true);
            animator = new Animator(sprite);
            healthMax = 3;
            health = healthMax;
            healthOffset = 23f;
            touchDamage = 1;
            touchDamagePlayer = true;
            bloodParticleColor = new Color(216, 143, 172);
            itemDropType = Item.pinkJelly;
            itemDropQuantity = Main.random.Next(2) + 1;
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