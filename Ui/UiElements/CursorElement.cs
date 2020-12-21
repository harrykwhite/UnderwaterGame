namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using UnderwaterGame.Items;
    using UnderwaterGame.Utilities;

    public class CursorElement : UiElement
    {
        public string text = "";

        public Item dragItem;

        public int dragQuantity;

        public Vector2 scale = Vector2.One;

        public override void Draw()
        {
            Vector2 mousePosition = Control.GetMousePosition();
            Main.spriteBatch.Draw(Main.textureLibrary.UI_OTHER_CURSOR.asset, mousePosition, null, Color.White, 0f, new Vector2(Main.textureLibrary.UI_OTHER_CURSOR.asset.Width, Main.textureLibrary.UI_OTHER_CURSOR.asset.Height) / 2f, scale * UiManager.scale, SpriteEffects.None, 1f);
            scale += (Vector2.One - scale) * 0.2f;
            if(dragItem != null)
            {
                Texture2D itemTextureOutlined = dragItem.sprite.texturesOutlined[0];
                Vector2 itemPosition = mousePosition + new Vector2(16f);
                Main.spriteBatch.Draw(itemTextureOutlined, itemPosition, null, Color.White, 0f, new Vector2(dragItem.sprite.bound.X + (dragItem.sprite.bound.Width / 2f), dragItem.sprite.bound.Y + (dragItem.sprite.bound.Height / 2f)) + Vector2.One, UiManager.scale, SpriteEffects.None, 1f);
                if(dragItem.stack)
                {
                    DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(dragQuantity.ToString()), itemPosition + new Vector2(24f, 28f), Color.White, DrawUtilities.HorizontalAlign.Right, DrawUtilities.VerticalAlign.Bottom);
                }
            }
            else
            {
                if(text != "")
                {
                    DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(text), mousePosition + new Vector2(4f), Color.White, DrawUtilities.HorizontalAlign.Left, DrawUtilities.VerticalAlign.Top);
                }
            }
            text = "";
        }

        public override void Init()
        {
        }

        public override void Update()
        {
        }
    }
}