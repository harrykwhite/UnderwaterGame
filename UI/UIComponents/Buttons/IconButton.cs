namespace UnderwaterGame.Ui.UiComponents.Buttons
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Ui.UiElements;

    public class IconButton : ButtonComponent
    {
        public Texture2D texture;

        public Texture2D icon;

        public Shape shape;

        public override void Draw()
        {
            Vector2 position = getPosition();
            float alpha = getAlpha();
            Main.spriteBatch.Draw(texture, position, null, Color.White * alpha, 0f, new Vector2(texture.Width, texture.Height) / 2f, scale, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(icon, position, null, Color.White * alpha, 0f, new Vector2(icon.Width, icon.Height) / 2f, scale, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            texture = Main.textureLibrary.UI_BUTTONS_OTHER_BUTTON.asset;
            shape = new Shape(Shape.Fill.Circle, texture.Width, texture.Height);
        }

        public override void Update()
        {
            UpdateButton();
        }

        protected override bool IsTouching()
        {
            if(!CanTouch)
            {
                return false;
            }
            Shape shape = this.shape;
            shape.position = getPosition() - new Vector2((int)Math.Ceiling(texture.Width / 2f), (int)Math.Ceiling(texture.Height / 2f));
            return shape.Intersects(((GameCursorElement)UiManager.GetElement<GameCursorElement>()).GetShape());
        }
    }
}