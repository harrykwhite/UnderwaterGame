namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class VignetteElement : UiElement
    {
        public Func<bool> getActive;

        public Color color = Color.Black;

        public float alphaAcc = 0.01f;

        public float alphaMax = 0.5f;
        
        public float alpha;

        public float alphaTo;

        public bool big;
        
        public override void Draw()
        {
            Texture2D texture = big ? Main.textureLibrary.UI_OTHER_BIGVIGNETTE.asset : Main.textureLibrary.UI_OTHER_SMALLVIGNETTE.asset;
            Main.spriteBatch.Draw(texture, Vector2.Zero, null, color * alpha, 0f, Vector2.Zero, UiManager.scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(texture, new Vector2(Main.GetBufferWidth(), 0f), null, color * alpha, MathHelper.Pi / 2f, Vector2.Zero, UiManager.scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(texture, new Vector2(0f, Main.GetBufferHeight()), null, color * alpha, (MathHelper.Pi * 3f) / 2f, Vector2.Zero, UiManager.scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(texture, new Vector2(Main.GetBufferWidth(), Main.GetBufferHeight()), null, color * alpha, MathHelper.Pi, Vector2.Zero, UiManager.scale, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
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