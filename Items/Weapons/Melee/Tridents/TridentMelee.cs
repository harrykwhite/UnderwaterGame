namespace UnderwaterGame.Items.Weapons.Melee.Tridents
{
    using Microsoft.Xna.Framework.Audio;
    using System;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class TridentMelee : MeleeWeapon
    {
        protected float swingLength = 4f;
        
        public override void WhileUse()
        {
            float swingSpeed = (swingLength * 2f) / (useTime / 2f);
            if(World.player.heldItem.useState == 0)
            {
                if(World.player.heldItem.lengthOffset < swingLength)
                {
                    World.player.heldItem.lengthOffset += Math.Min(swingSpeed, swingLength - World.player.heldItem.lengthOffset);
                }
                else
                {
                    World.player.heldItem.useState = 1;
                }
            }
            if(World.player.heldItem.useState == 1)
            {
                if(World.player.heldItem.lengthOffset > 0f)
                {
                    World.player.heldItem.lengthOffset -= Math.Min(swingSpeed, World.player.heldItem.lengthOffset);
                }
                else
                {
                    World.player.heldItem.lengthOffset = 0f;
                }
            }
            SwingUpdate();
        }

        protected override HitEntity Swing()
        {
            SoundEffect soundEffect = (Main.random.Next(4)) switch
            {
                1 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT1.asset,
                2 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT2.asset,
                3 => Main.soundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT3.asset,
                _ => Main.soundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT0.asset
            };
            World.player.heldItem.SetSwingEffect(swingSprite, (float)swingSprite.textures.Length / (float)(useTime / 2f), hitboxOffset);
            SoundUtilities.PlaySound(soundEffect);
            Camera.Shake(1f, World.player.heldItem.angleBase);
            return base.Swing();
        }
    }
}