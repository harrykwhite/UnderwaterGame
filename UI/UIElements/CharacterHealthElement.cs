using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Characters;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIElements
{
    public class CharacterHealthElement : UIElement
    {
        public override void Draw()
        {
            List<Entity> characterEntities = EntityManager.Entities.FindAll((Entity entity) => entity is CharacterEntity);

            foreach (Entity characterEntity in characterEntities)
            {
                CharacterEntity character = (CharacterEntity)characterEntity;

                Shape barShape = new Shape(Shape.Fill.Rectangle, 24, 1);
                Vector2 barPosition = UIManager.WorldToUI(character.position - new Vector2(0f, character.Sprite.Origin.Y));

                if (character.Sprite?.Textures[0] != null)
                {
                    barPosition.Y += (((character.Sprite.Bound.Y + character.Sprite.Bound.Height) * Camera.Scale) / UIManager.Scale) + 8f;
                }

                barShape.position.X = (int)(barPosition.X - (barShape.width / 2f));
                barShape.position.Y = (int)(barPosition.Y - (barShape.height / 2f));

                DrawUtilities.DrawBar(barShape, character.Health / character.HealthMax, Color.White, Color.White * 0.5f, 0);

                if (Main.World.player == character)
                {
                    barShape.position.Y += 4f;
                    DrawUtilities.DrawBar(barShape, Main.World.player.Magic / Main.World.player.MagicMax, Color.White, Color.White * 0.5f, 0);
                }
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