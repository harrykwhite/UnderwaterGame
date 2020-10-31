namespace UnderwaterGame
{
    using System;
    using UnderwaterGame.Sprites;

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
            if(hidden)
            {
                return;
            }
            index += speed;
            if(index >= sprite.textures.Length)
            {
                index -= sprite.textures.Length;
                loopAction?.Invoke();
            }
            if(index < 0f)
            {
                index += sprite.textures.Length;
                loopAction?.Invoke();
            }
        }
    }
}