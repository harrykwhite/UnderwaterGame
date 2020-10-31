namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Sound;
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
            SoundEffect soundEffect;
            switch(Main.random.Next(4))
            {
                case 1:
                    soundEffect = Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD1.asset;
                    break;

                case 2:
                    soundEffect = Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD2.asset;
                    break;

                case 3:
                    soundEffect = Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD3.asset;
                    break;

                default:
                    soundEffect = Main.soundLibrary.ITEMS_WEAPONS_MELEE_SWORD0.asset;
                    break;
            }
            SoundManager.PlaySound(soundEffect, SoundManager.Category.Sound);
            Camera.Shake(1f, entity.angleBase);
            return hitEntity;
        }
    }
}