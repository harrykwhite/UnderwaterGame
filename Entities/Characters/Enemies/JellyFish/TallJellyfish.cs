namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Items;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

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
            bloodParticleColor = new Color(164, 132, 183);
            itemDropType = new Item[2] { Item.purpleJelly, Item.purpleJellyShuriken };
            itemDropQuantity = new int[2] { Main.random.Next(2) + 2, Main.random.Next(2) + 3 };
            itemDropChance = new float[2] { 0.5f, 0.1f };
        }

        public override void Update()
        {
            JellyfishUpdate();
            UpdateStatus();
            TileCollisions();
            position += velocity;
            position.X = MathUtilities.Clamp(position.X, 0f, World.width * Tile.size);
            position.Y = MathUtilities.Clamp(position.Y, 0f, World.height * Tile.size);
            animator.speed = 0.1f;
            animator.Update();
            UpdateTouchDamage();
            velocity = Vector2.Zero;
        }
    }
}