namespace UnderwaterGame.Items.Edibles.Health
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Worlds;

    public abstract class HealthEdible : EdibleItem
    {
        protected float healthAmount;

        public override bool CanUse(ItemEntity entity)
        {
            return World.player.health < World.player.healthMax;
        }

        public override void OnUse(ItemEntity entity)
        {
            if(World.player.Heal(healthAmount))
            {
                entity.RemoveItem(1);
            }
        }
    }
}