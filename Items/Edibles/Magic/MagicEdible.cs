namespace UnderwaterGame.Items.Edibles.Magic
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Worlds;

    public abstract class MagicEdible : EdibleItem
    {
        protected float magicAmount;

        public override bool CanUse(ItemEntity entity)
        {
            return World.player.magic < World.player.magicMax;
        }

        public override void OnUse(ItemEntity entity)
        {
            if(World.player.HealMagic(magicAmount))
            {
                entity.RemoveItem(1);
            }
        }
    }
}