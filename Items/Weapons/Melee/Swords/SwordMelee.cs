namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Utilities;

    public abstract class SwordMelee : MeleeWeapon
    {
        protected float swingAngleRange = MathHelper.Pi / 3f;

        public override void WhileUse(ItemEntity entity)
        {
            SwingUpdate(entity);
        }

        protected override HitEntity Swing(ItemEntity entity)
        {
            HitEntity hitEntity = base.Swing(entity);
            float from = swingAngleRange * (MathUtilities.AngleLeftHalf(entity.angleBaseRelative) ? 1f : -1f);
            float to = -from;
            entity.SetAngleHoldOffset(from, to, useTime);
            SoundEffect soundEffect = (Main.random.Next(4)) switch
            {
                1 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD1.asset,
                2 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD2.asset,
                3 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD3.asset,
                _ => Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD0.asset,
            };
            SoundUtilities.PlaySound(soundEffect);
            Camera.Shake(1f, entity.angleBase);
            return hitEntity;
        }
    }
}