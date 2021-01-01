namespace UnderwaterGame.Entities.Characters.Enemies.Jellyfish
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Items;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

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
            bloodParticleColor = new Color(216, 143, 172);
            itemDropType = new Item[2] { Item.pinkJelly, Item.pinkJellyShuriken };
            itemDropQuantity = new int[2] { Main.random.Next(2) + 1, Main.random.Next(2) + 2 };
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