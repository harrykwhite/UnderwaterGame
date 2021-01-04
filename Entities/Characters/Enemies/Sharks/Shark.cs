namespace UnderwaterGame.Entities.Characters.Enemies.Sharks
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class Shark : SharkEnemy
    {
        public override void Draw()
        {
            DrawSelf();
            DrawFlash();
        }

        public override void Init()
        {
            SetSprite(Sprite.shark, true);
            animator = new Animator(sprite);
            healthMax = 3;
            health = healthMax;
            healthOffset = 22f;
            touchDamage = 1;
        }

        public override void Update()
        {
            SharkUpdate();
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
