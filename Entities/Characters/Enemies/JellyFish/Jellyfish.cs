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
            itemDropType = new Item[2] { Item.pinkJelly, Item.pinkJellyShuriken };
            itemDropQuantity = new int[2] { Main.random.Next(2) + 1, Main.random.Next(2) + 2 };
            itemDropChance = new float[2] { 0.5f, 0.1f };
        }

        public override void Update()
        {
            JellyfishUpdate();
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

        public override void EndUpdate()
        {
            CheckForDamage();
        }
    }
}