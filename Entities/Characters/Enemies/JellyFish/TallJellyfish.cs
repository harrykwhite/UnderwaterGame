namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Items;
    using UnderwaterGame.Sprites;

    public class TallJellyfish : JellyfishEnemy
    {
        public override void Draw()
        {
            DrawSelf();
            DrawFlash();
        }

        public override void Init()
        {
            SetSprite(Sprite.tallJellyfish, true);
            animator = new Animator(sprite);
            healthMax = 6;
            health = healthMax;
            healthOffset = 29f;
            touchDamage = 1;
            touchDamagePlayer = true;
            bloodParticleColor = new Color(164, 132, 183);
            swimBreakTimeMax = 30;
            itemDropType = new Item[2] { Item.purpleJelly, Item.purpleJellyShuriken };
            itemDropQuantity = new int[2] { Main.random.Next(2) + 2, Main.random.Next(2) + 3 };
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