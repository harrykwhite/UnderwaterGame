using Microsoft.Xna.Framework.Audio;
using UnderwaterGame.Entities;
using UnderwaterGame.Sound;

namespace UnderwaterGame.Items.Weapons.Melee.Tridents
{
    public abstract class TridentMelee : MeleeWeapon
    {
        protected float swingSpeed = 1f;
        protected float swingLength = 4f;

        public override void WhileUse(ItemEntity entity)
        {
            if (entity.useState == 0)
            {
                if (entity.lengthOffset < swingLength)
                {
                    entity.lengthOffset += swingSpeed;
                }
                else
                {
                    entity.useState = 1;
                }
            }

            if (entity.useState == 1)
            {
                if (entity.lengthOffset > 0f)
                {
                    entity.lengthOffset -= swingSpeed;
                }
                else
                {
                    entity.lengthOffset = 0f;
                }
            }

            SwingUpdate(entity);
        }

        protected override HitEntity Swing(ItemEntity entity)
        {
            SoundEffect soundEffect;

            switch (Main.Random.Next(4))
            {
                case 1:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT1.Asset;
                    break;

                case 2:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT2.Asset;
                    break;

                case 3:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT3.Asset;
                    break;

                default:
                    soundEffect = Main.SoundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT0.Asset;
                    break;
            }

            SoundManager.PlaySound(soundEffect, SoundManager.Category.Sound);

            Camera.Shake(1f, entity.angleBase);

            return base.Swing(entity);
        }
    }
}