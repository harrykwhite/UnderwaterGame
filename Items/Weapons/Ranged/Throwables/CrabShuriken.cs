namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

    public class CrabShuriken : ThrowableRanged
    {
        protected override void Init()
        {
            name = "Crab Shuriken";
            sprite = Sprite.crabShuriken;
            useTime = 15;
            useStrength = 1f;
            useHide = true;
            damage = 2f;
            strength = 2f;
        }

        public override void OnUse()
        {
            Shoot<Entities.Projectiles.Throwables.CrabShuriken>(World.player.heldItem.angleBase, 7f);
        }
    }
}