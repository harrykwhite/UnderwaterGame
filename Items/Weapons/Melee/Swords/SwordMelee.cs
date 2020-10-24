using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using UnderwaterGame.Entities;
using UnderwaterGame.Sound;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
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

            entity.SetAngleHoldOffset(from, to, UseTime);

            SoundEffect soundEffect;

            switch (Main.Random.Next(4))
            {
                case 1:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_SWORD1.Asset;
                    break;

                case 2:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_SWORD2.Asset;
                    break;

                case 3:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_SWORD3.Asset;
                    break;

                default:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_SWORD0.Asset;
                    break;
            }

            SoundManager.PlaySound(soundEffect, SoundManager.Category.Sound);

            Camera.Shake(1f, entity.angleBase);

            return hitEntity;
        }
    }
}