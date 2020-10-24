using UnderwaterGame.Entities;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    public class CrabShuriken : ThrowableRanged
    {
        protected override void Init()
        {
            Name = "Crab Shuriken";
            Sprite = Sprite.CrabShuriken;

            UseTime = 15;
            UseHide = true;

            Damage = 2f;
        }

        public override void OnUse(ItemEntity entity)
        {
            Shoot<Entities.Projectiles.Throwables.CrabShuriken>(entity, entity.angleBase, 7f);
        }
    }
}