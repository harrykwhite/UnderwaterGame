using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.UI.UIElements;

namespace UnderwaterGame.UI.UIComponents.Buttons
{
    public class IconButton : ButtonComponent
    {
        public Texture2D texture;
        public Texture2D icon;

        public Shape shape;

        public override void Draw()
        {
            Main.SpriteBatch.Draw(texture, Position, null, Color.White * Alpha, 0f, new Vector2(texture.Width, texture.Height) / 2f, scale, SpriteEffects.None, 1f);
            Main.SpriteBatch.Draw(icon, Position, null, Color.White * Alpha, 0f, new Vector2(icon.Width, icon.Height) / 2f, scale, SpriteEffects.None, 1f);
        }

        public override void Init()
        {
            texture = Main.TextureLibrary.UI_BUTTONS_OTHER_BUTTON.Asset;
            shape = new Shape(Shape.Fill.Circle, texture.Width, texture.Height);
        }

        public override void Update()
        {
            UpdateButton();
        }

        protected override bool IsTouching()
        {
            if (!CanTouch)
            {
                return false;
            }

            Shape shape = this.shape;
            shape.position = Position - new Vector2((int)Math.Ceiling(texture.Width / 2f), (int)Math.Ceiling(texture.Height / 2f));

            return shape.Intersects(((GameCursorElement)UIManager.GetElement<GameCursorElement>()).GetShape());
        }
    }
}