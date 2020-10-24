using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.Input;
using UnderwaterGame.Items;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIElements
{
    public class GameCursorElement : UIElement
    {
        public string text = "";

        public Item dragItem;
        public int dragQuantity;

        public Vector2 scaleOffset;

        public Shape shape;

        public override void Draw()
        {
            Vector2 mousePosition = InputManager.GetMousePositionUI();
            Main.SpriteBatch.Draw(Main.TextureLibrary.UI_OTHER_CURSOR.Asset, mousePosition, null, Color.White, 0f, new Vector2(Main.TextureLibrary.UI_OTHER_CURSOR.Asset.Width, Main.TextureLibrary.UI_OTHER_CURSOR.Asset.Height) / 2f, Vector2.One + scaleOffset, SpriteEffects.None, 1f);

            scaleOffset *= 0.9f;

            if (dragItem != null)
            {
                Texture2D itemTextureOutlined = dragItem.Sprite.TexturesOutlined[0];
                Vector2 itemPosition = mousePosition + new Vector2(8f);

                Main.SpriteBatch.Draw(itemTextureOutlined, itemPosition, null, Color.White, 0f, new Vector2(dragItem.Sprite.Bound.X + (dragItem.Sprite.Bound.Width / 2f), dragItem.Sprite.Bound.Y + (dragItem.Sprite.Bound.Height / 2f)) + Vector2.One, 1f, SpriteEffects.None, 1f);

                if (dragItem.Stack)
                {
                    DrawUtilities.DrawString(Main.FontLibrary.ARIALSMALL.Asset, new DrawUtilities.Text(dragQuantity.ToString()), itemPosition + new Vector2(12f, 14f), Color.White, DrawUtilities.HAlign.Right, DrawUtilities.VAlign.Bottom);
                }
            }
            else
            {
                if (text != "")
                {
                    DrawUtilities.DrawString(Main.FontLibrary.ARIALSMALL.Asset, new DrawUtilities.Text(text), mousePosition + new Vector2(4f), Color.White, DrawUtilities.HAlign.Left, DrawUtilities.VAlign.Top);
                }
            }

            text = "";
        }

        public override void Init()
        {
            shape = new Shape(Shape.Fill.Circle, Main.TextureLibrary.UI_OTHER_CURSOR.Asset.Width, Main.TextureLibrary.UI_OTHER_CURSOR.Asset.Height);
        }

        public override void Update()
        {

        }

        public void Expand(float amount)
        {
            scaleOffset += new Vector2(amount);
        }

        public Shape GetShape()
        {
            Shape shape = this.shape;
            shape.position = InputManager.GetMousePositionUI() - new Vector2((int)Math.Ceiling(Main.TextureLibrary.UI_OTHER_CURSOR.Asset.Width / 2f), (int)Math.Ceiling(Main.TextureLibrary.UI_OTHER_CURSOR.Asset.Height / 2f));

            return shape;
        }
    }
}