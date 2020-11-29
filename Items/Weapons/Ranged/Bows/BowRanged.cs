namespace UnderwaterGame.Items.Weapons.Ranged.Bows
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Worlds;

    public abstract class BowRanged : RangedWeapon
    {
        public override void OnUse()
        {
            World.player.heldItem.animator.index = 0f;
            World.player.heldItem.animator.speed = World.player.heldItem.animator.sprite.textures.Length / (float)useTime;
        }
    }
}