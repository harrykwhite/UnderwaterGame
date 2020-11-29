namespace UnderwaterGame.Items.Weapons.Magic
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Worlds;

    public abstract class MagicWeapon : WeaponItem
    {
        public override bool CanUse()
        {
            return World.player.magic > 0f;
        }
    }
}