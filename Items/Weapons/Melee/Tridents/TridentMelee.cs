namespace UnderwaterGame.Items.Weapons.Melee.Tridents
{
    using Microsoft.Xna.Framework.Audio;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class TridentMelee : MeleeWeapon
    {
        protected float swingSpeed = 1f;

        protected float swingLength = 4f;

        public override void WhileUse()
        {
            if(World.player.heldItem.useState == 0)
            {
                if(World.player.heldItem.lengthOffset < swingLength)
                {
                    World.player.heldItem.lengthOffset += swingSpeed;
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
                    World.player.heldItem.lengthOffset -= swingSpeed;
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
                _ => Main.soundLibrary.ITEMS_WEAPONS_MELEE_TRIDENT0.asset,
            };
            SoundUtilities.PlaySound(soundEffect);
            Camera.Shake(1f, World.player.heldItem.angleBase);
            return base.Swing();
        }
    }
}