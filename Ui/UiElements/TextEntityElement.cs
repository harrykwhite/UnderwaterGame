namespace UnderwaterGame.Ui.UiElements
{
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Utilities;

    public class TextEntityElement : UiElement
    {
        public override void Draw()
        {
            List<Entity> textEntities = EntityManager.entities.FindAll((Entity entity) => entity is TextEntity);
            foreach(Entity textEntity in textEntities)
            {
                TextEntity text = (TextEntity)textEntity;
                DrawUtilities.DrawStringExt(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(text.text), UiManager.WorldToUi(text.position), text.blend * text.alpha, text.angle, text.scale, DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
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