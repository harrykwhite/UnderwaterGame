namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class HotspotCountElement : UiElement
    {
        public override void Draw()
        {
            foreach(WorldHotspot hotspot in World.hotspots)
            {
                DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(hotspot.count.ToString() + " enemies"), UiManager.WorldToUi(new Vector2(hotspot.x, hotspot.y)), Color.White, DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
            }
            List<Entity> characterEntities = EntityManager.entities.FindAll((Entity entity) => entity is CharacterEntity);
            foreach(Entity characterEntity in characterEntities)
            {
                CharacterEntity character = (CharacterEntity)characterEntity;
                Shape barShape = new Shape(Shape.Fill.Rectangle, 24, 1);
                Vector2 barPosition = UiManager.WorldToUi(character.position - new Vector2(0f, character.sprite.origin.Y));
                if(character.sprite?.textures[0] != null)
                {
                    barPosition.Y += (((character.sprite.bound.Y + character.sprite.bound.Height) * Camera.scale) / UiManager.scale) + 8f;
                }
                barShape.position.X = (int)(barPosition.X - (barShape.width / 2f));
                barShape.position.Y = (int)(barPosition.Y - (barShape.height / 2f));
                DrawUtilities.DrawBar(barShape, character.health / character.healthMax, Color.White, Color.White * 0.5f, 0);
                if(World.player == character)
                {
                    barShape.position.Y += 4f;
                    DrawUtilities.DrawBar(barShape, World.player.magic / World.player.magicMax, Color.White, Color.White * 0.5f, 0);
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