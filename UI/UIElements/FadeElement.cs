namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class FadeElement : UiElement
    {
        public Func<bool> getActive;

        public float alphaSpeed = 0.05f;

        public float alphaMax = 0.5f;

        public float alpha;

        public float alphaTo;

        public override void Draw()
        {
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, Vector2.Zero, null, Color.Black * alpha, 0f, Vector2.Zero, UiManager.GetSize(), SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            alpha = alphaMax;
            alphaTo = alpha;
        }

        public override void Update()
        {
            alphaTo = getActive() ? alphaMax : 0f;
            if(alpha < alphaTo)
            {
                alpha += Math.Min(alphaSpeed, alphaTo - alpha);
            }
            else if(alpha > alphaTo)
            {
                alpha -= Math.Min(alphaSpeed, alpha - alphaTo);
            }
        }
    }
}