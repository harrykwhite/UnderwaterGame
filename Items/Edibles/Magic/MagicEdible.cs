namespace UnderwaterGame.Items.Edibles.Magic
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Worlds;

    public abstract class MagicEdible : EdibleItem
    {
        protected float magicAmount;

        public override bool CanUse()
        {
            return World.player.magic < World.player.magicMax;
        }

        public override void OnUse()
        {
            if(World.player.HealMagic(magicAmount))
            {
                World.player.heldItem.RemoveItem(1);
            }
        }
    }
}