namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters;

    public class HealthElement : UiElement
    {
        public override void Draw()
        {
            List<Entity> characterEntities = EntityManager.entities.FindAll((Entity entity) => entity is CharacterEntity);
            foreach(Entity characterEntity in characterEntities)
            {
                CharacterEntity character = (CharacterEntity)characterEntity;
                int height = (int)Math.Ceiling(character.healthMax / 3f);
                for(int y = 0; y < height; y++)
                {
                    int width = Math.Min(character.healthMax - (y * 3), 3);
                    for(int x = 0; x < width; x++)
                    {
                        Main.spriteBatch.Draw(Main.textureLibrary.UI_OTHER_HEALTH.asset, UiManager.WorldToUi(character.position) + new Vector2((Main.textureLibrary.UI_OTHER_HEALTH.asset.Width + 1f) * 2f * (x - ((width - 1f) / 2f)), ((Main.textureLibrary.UI_OTHER_HEALTH.asset.Height + 1f) * 2f * y) + character.healthOffset), null, Color.White * (character.health > (y * 3) + x ? 1f : 0.5f), 0f, new Vector2(Main.textureLibrary.UI_OTHER_HEALTH.asset.Width, Main.textureLibrary.UI_OTHER_HEALTH.asset.Height) / 2f, UiManager.scale, SpriteEffects.None, 1f);
                    }
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