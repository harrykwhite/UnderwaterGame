using UnderwaterGame.Entities;

namespace UnderwaterGame.Items.Weapons.Magic
{
    public abstract class MagicWeapon : WeaponItem
    {
        public override bool CanUse(ItemEntity entity) => Main.World.player.Magic > 0f;
    }
}