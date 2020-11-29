namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class SwordMelee : MeleeWeapon
    {
        protected float swingAngleRange = MathHelper.Pi / 3f;

        public override void WhileUse()
        {
            SwingUpdate();
        }

        protected override HitEntity Swing()
        {
            HitEntity hitEntity = base.Swing();
            float from = swingAngleRange * (MathUtilities.AngleLeftHalf(World.player.heldItem.angleBaseRelative) ? 1f : -1f);
            float to = -from;
            World.player.heldItem.SetAngleHoldOffset(from, to, useTime);
            SoundEffect soundEffect = (Main.random.Next(4)) switch
            {
                1 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD1.asset,
                2 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD2.asset,
                3 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD3.asset,
                _ => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD0.asset,
            };
            SoundUtilities.PlaySound(soundEffect);
            Camera.Shake(1f, World.player.heldItem.angleBase);
            return hitEntity;
        }
    }
}