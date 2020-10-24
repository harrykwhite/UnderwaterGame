using System;
using UnderwaterGame.Sprites;

namespace UnderwaterGame
{
    public class Animator
    {
        public Sprite sprite;

        public float index;
        public float speed;

        public bool hidden;

        public Action loopAction;

        public Animator(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Update()
        {
            if (hidden)
            {
                return;
            }

            index += speed;

            if (index >= sprite.Textures.Length)
            {
                index -= sprite.Textures.Length;
                loopAction?.Invoke();
            }

            if (index < 0f)
            {
                index += sprite.Textures.Length;
                loopAction?.Invoke();
            }
        }
    }
}