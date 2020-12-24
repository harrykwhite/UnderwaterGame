namespace UnderwaterGame.Items.Edibles.Health
{
    using UnderwaterGame.Worlds;

    public abstract class HealthEdible : EdibleItem
    {
        protected int healthAmount;

        public override bool CanUse()
        {
            return World.player.health < World.player.healthMax;
        }

        public override void OnUse()
        {
            if(World.player.Heal(healthAmount))
            {
                World.player.heldItem.RemoveItem(1);
            }
        }
    }
}