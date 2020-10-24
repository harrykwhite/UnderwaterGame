using UnderwaterGame.Entities;

namespace UnderwaterGame.Items.Edibles.Healing
{
    public abstract class HealingEdible : EdibleItem
    {
        protected float healAmount;

        public override bool CanUse(ItemEntity entity) => Main.World.player.Health < Main.World.player.HealthMax;

        public override void OnUse(ItemEntity entity)
        {
            if (Main.World.player.Heal(healAmount))
            {
                entity.RemoveItem(1);
            }
        }
    }
}