using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UnderwaterGame.UI.UIElements
{
    public class ScreenFlashElement : UIElement
    {
        private float flashAlpha;
        private float flashAlphaSpeed;
        private Color flashColor = Color.White;

        public override void Draw()
        {
            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, Vector2.Zero, null, flashColor * flashAlpha, 0f, Vector2.Zero, UIManager.Size, SpriteEffects.None, 1f);
        }

        public override void Init()
        {

        }

        public override void Update()
        {
            if (flashAlpha > 0f)
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