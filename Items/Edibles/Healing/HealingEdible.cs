namespace UnderwaterGame.Items.Edibles.Healing
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Worlds;

    public abstract class HealingEdible : EdibleItem
    {
        protected float healAmount;

        public override bool CanUse(ItemEntity entity)
        {
            return World.player.health < World.player.healthMax;
        }

        public override void OnUse(ItemEntity entity)
        {
            if(World.player.Heal(healAmount))
            {
                entity.RemoveItem(1);
            }
        }
    }
}