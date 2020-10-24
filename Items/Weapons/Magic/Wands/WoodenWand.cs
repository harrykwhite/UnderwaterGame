using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Projectiles.Magic;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Weapons.Magic.Wands
{
    public class WoodenWand : WandMagic
    {
        protected override void Init()
        {
            Name = "Wooden Wand";
            Sprite = Sprite.WoodenWand;

            UseTime = 40;

            Damage = 8f;
        }

        public override void OnUse(ItemEntity entity)
        {
            if (Main.World.player.HurtMagic(5f))
            {
                Shoot<FlareMagic>(entity, entity.angleBase, 12f);
            }
        }
    }
}