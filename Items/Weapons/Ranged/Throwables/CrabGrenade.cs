using UnderwaterGame.Entities;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    public class CrabGrenade : ThrowableRanged
    {
        protected override void Init()
        {
            Name = "Crab Grenade";
            Sprite = Sprite.CrabGrenade;

            UseTime = 45;
            UseHide = true;

            Damage = 8f;
        }

        public override void OnUse(ItemEntity entity)
        {
            Shoot<Entities.Projectiles.Throwables.CrabGrenade>(entity, entity.angleBase, 7f);
        }
    }
}