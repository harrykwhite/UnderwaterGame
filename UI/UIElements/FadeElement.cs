using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UnderwaterGame.UI.UIElements
{
    public class FadeElement : UIElement
    {
        public Func<bool> getActive;

        public float alphaSpeed = 0.05f;
        public float alphaMax = 0.5f;

        public float Alpha { get; private set; }
        public float AlphaTo { get; private set; }

        public override void Draw()
        {
            Main.SpriteBatch.Draw(Main.TextureLibrary.OTHER_PIXEL.Asset, Vector2.Zero, null, Color.Black * Alpha, 0f, Vector2.Zero, UIManager.Size, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            Alpha = alphaMax;
            AlphaTo = Alpha;
        }

        public override void Update()
        {
            AlphaTo = getActive.Invoke() ? alphaMax : 0f;

            if (Alpha < AlphaTo)
            {
                Alpha += Math.Min(alphaSpeed, AlphaTo - Alpha);
            }
            else if (Alpha > AlphaTo)
            {
                Alpha -= Math.Min(alphaSpeed, Alpha - AlphaTo);
            }
        }
    }
}