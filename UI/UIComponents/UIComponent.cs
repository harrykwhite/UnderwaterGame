namespace UnderwaterGame.Ui.UiComponents
{
    using Microsoft.Xna.Framework;
    using System;

    public abstract class UiComponent
    {
        protected Vector2 scaleSpeed = new Vector2(0.2f);

        public Vector2 scale = Vector2.One;

        public Vector2 scaleTo = Vector2.One;

        public Vector2 scaleMax = new Vector2(1.2f);

        public Func<Vector2> getPosition = () => Vector2.Zero;

        public Func<float> getAlpha = () => 1f;

        public Func<bool> getActive = () => true;

        public static float gap = 44f;

        public abstract void Init();

        public abstract void Update();

        public abstract void Draw();

        protected void UpdateScale()
        {
            scale += (scaleTo - scale) * scaleSpeed;
        }
    }
}