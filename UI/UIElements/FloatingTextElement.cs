using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UnderwaterGame.Entities;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIElements
{
    public class FloatingTextElement : UIElement
    {
        public override void Draw()
        {
            List<Entity> floatingTextEntities = EntityManager.Entities.FindAll((Entity entity) => entity is FloatingTextEntity);

            foreach (Entity floatingTextEntity in floatingTextEntities)
            {
                FloatingTextEntity floatingText = (FloatingTextEntity)floatingTextEntity;

                Vector2 position = UIManager.WorldToUI(floatingText.position);
                SpriteFont font = Main.FontLibrary.ARIALMEDIUM.Asset;

                DrawUtilities.DrawStringExt(font, new DrawUtilities.Text(floatingText.text), position, floatingText.blend * floatingText.alpha, floatingText.angle, floatingText.scale, DrawUtilities.HAlign.Middle, DrawUtilities.VAlign.Middle);
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