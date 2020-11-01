namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Items;
    using UnderwaterGame.Utilities;

    public class GameCursorElement : UiElement
    {
        public string text = "";

        public Item dragItem;

        public int dragQuantity;

        public Vector2 scale = Vector2.One;

        public Shape shape;

        public override void Draw()
        {
            Vector2 mousePosition = Control.GetMousePositionUi();
            Main.spriteBatch.Draw(Main.textureLibrary.UI_OTHER_CURSOR.asset, mousePosition, null, Color.White, 0f, new Vector2(Main.textureLibrary.UI_OTHER_CURSOR.asset.Width, Main.textureLibrary.UI_OTHER_CURSOR.asset.Height) / 2f, scale, SpriteEffects.None, 1f);
            scale += (Vector2.One - scale) * 0.2f;
            if(dragItem != null)
            {
                Texture2D itemTextureOutlined = dragItem.sprite.texturesOutlined[0];
                Vector2 itemPosition = mousePosition + new Vector2(8f);
                Main.spriteBatch.Draw(itemTextureOutlined, itemPosition, null, Color.White, 0f, new Vector2(dragItem.sprite.bound.X + (dragItem.sprite.bound.Width / 2f), dragItem.sprite.bound.Y + (dragItem.sprite.bound.Height / 2f)) + Vector2.One, 1f, SpriteEffects.None, 1f);
                if(dragItem.stack)
                {
                    DrawUtilities.DrawString(Main.fontLibrary.ARIALSMALL.asset, new DrawUtilities.Text(dragQuantity.ToString()), itemPosition + new Vector2(12f, 14f), Color.White, DrawUtilities.HorizontalAlign.Right, DrawUtilities.VerticalAlign.Bottom);
                }
            }
            else
            {
                if(text != "")
                {
                    DrawUtilities.DrawString(Main.fontLibrary.ARIALSMALL.asset, new DrawUtilities.Text(text), mousePosition + new Vector2(4f), Color.White, DrawUtilities.HorizontalAlign.Left, DrawUtilities.VerticalAlign.Top);
                }
            }
            text = "";
        }

        public override void Init()
        {
            shape = new Shape(Shape.Fill.Circle, Main.textureLibrary.UI_OTHER_CURSOR.asset.Width, Main.textureLibrary.UI_OTHER_CURSOR.asset.Height);
        }

        public override void Update()
        {
        }

        public void Expand(float amount)
        {
            scale += new Vector2(amount);
        }

        public Shape GetShape()
        {
            Shape shape = this.shape;
            shape.position = Control.GetMousePositionUi() - new Vector2((int)Math.Ceiling(Main.textureLibrary.UI_OTHER_CURSOR.asset.Width / 2f), (int)Math.Ceiling(Main.textureLibrary.UI_OTHER_CURSOR.asset.Height / 2f));
            return shape;
        }
    }
}