namespace UnderwaterGame.Ui.UiComponents.Buttons
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using UnderwaterGame.Ui.UiElements;

    public class IconButton : ButtonComponent
    {
        public Texture2D texture;

        public Texture2D icon;

        public override void Draw()
        {
            Main.spriteBatch.Draw(texture, getPosition(), null, Color.White * getAlpha(), 0f, new Vector2(texture.Width, texture.Height) / 2f, scale * UiManager.scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(icon, getPosition(), null, Color.White * getAlpha(), 0f, new Vector2(icon.Width, icon.Height) / 2f, scale * UiManager.scale, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            texture = Main.textureLibrary.UI_BUTTONS_OTHER_BUTTON.asset;
        }

        public override void Update()
        {
            UpdateButton();
        }

        protected override bool IsTouching()
        {
            return GetCanTouch() && Vector2.Distance(getPosition(), Control.GetMousePosition()) <= (texture.Width + Main.textureLibrary.UI_OTHER_CURSOR.asset.Width) * 0.5f * UiManager.scale;
        }
    }
}