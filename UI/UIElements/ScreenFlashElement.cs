namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class ScreenFlashElement : UiElement
    {
        private float flashAlpha;

        private float flashAlphaSpeed;

        private Color flashColor = Color.White;

        public override void Draw()
        {
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, Vector2.Zero, null, flashColor * flashAlpha, 0f, Vector2.Zero, UiManager.GetSize(), SpriteEffects.None, 1f);
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            if(flashAlpha > 0f)
            {
                flashAlpha -= Math.Min(flashAlphaSpeed, flashAlpha);
            }
        }

        public void SetFlash(Color color, float alpha, float alphaSpeed = 0.01f)
        {
            flashColor = color;
            flashAlpha = alpha;
            flashAlphaSpeed = alphaSpeed;
        }
    }
}