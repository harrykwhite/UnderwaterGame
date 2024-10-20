﻿namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class FadeElement : UiElement
    {
        public Func<bool> getActive;

        public float alphaAcc = 0.1f;

        public float alphaMax = 0.5f;

        public float alpha;

        public float alphaTo;

        public Color color = Color.Black;

        public override void Draw()
        {
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, Vector2.Zero, null, color * alpha, 0f, Vector2.Zero, new Vector2(Main.GetBufferWidth(), Main.GetBufferHeight()), SpriteEffects.None, 1f);
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
                alpha += Math.Min(alphaAcc, alphaTo - alpha);
            }
            else if(alpha > alphaTo)
            {
                alpha -= Math.Min(alphaAcc, alpha - alphaTo);
            }
        }
    }
}