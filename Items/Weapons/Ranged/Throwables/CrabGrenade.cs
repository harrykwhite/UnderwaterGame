namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

    public class CrabGrenade : ThrowableRanged
    {
        protected override void Init()
        {
            name = "Crab Grenade";
            sprite = Sprite.crabGrenade;
            useTime = 45;
            useStrength = 1f;
            useHide = true;
            damage = 8f;
            strength = 4f;
        }

        public override void OnUse()
        {
            Shoot<Entities.Projectiles.Throwables.CrabGrenade>(World.player.heldItem.angleBase, 7f);
        }
    }
}