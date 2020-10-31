namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Utilities;

    public class FloatingTextElement : UiElement
    {
        public override void Draw()
        {
            List<Entity> floatingTextEntities = EntityManager.entities.FindAll((Entity entity) => entity is FloatingTextEntity);
            foreach(Entity floatingTextEntity in floatingTextEntities)
            {
                FloatingTextEntity floatingText = (FloatingTextEntity)floatingTextEntity;
                Vector2 position = UiManager.WorldToUi(floatingText.position);
                SpriteFont font = Main.fontLibrary.ARIALMEDIUM.asset;
                DrawUtilities.DrawStringExt(font, new DrawUtilities.Text(floatingText.text), position, floatingText.blend * floatingText.alpha, floatingText.angle, floatingText.scale, DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
            }
        }

        public override void Init()
        {
        }

        public override void Update()
        {
        }
    }
}