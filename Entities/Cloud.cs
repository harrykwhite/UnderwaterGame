using Microsoft.Xna.Framework;
using UnderwaterGame.Sprites;
using UnderwaterGame.Tiles;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities
{
    public class Cloud : Entity
    {
        private float speed = 0.1f;

        public override void Draw()
        {
            DrawSelf();
        }

        public override void Init()
        {
            SetSprite(Sprite.cloud, false);
            animator = new Animator(sprite)
            {
                index = Main.random.Next(Sprite.cloud.textures.Length)
            };
            depth = 0.9f;
        }

        public override void Update()
        {
            float min = -sprite.textures[0].Width / 2f;
            float max = (World.width * Tile.size) + (sprite.textures[0].Width / 2f);
            if(position.X < min)
            {
                position.X = max;
                animator.index = Main.random.Next(Sprite.cloud.textures.Length);
            }
            if(position.X > max)
            {
                position.X = min;
                animator.index = Main.random.Next(Sprite.cloud.textures.Length);
            }
            velocity = new Vector2(speed, 0f);
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}