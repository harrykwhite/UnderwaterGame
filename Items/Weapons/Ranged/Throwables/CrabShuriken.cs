namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

    public class CrabShuriken : ThrowableRanged
    {
        protected override void Init()
        {
            name = "Crab Shuriken";
            sprite = Sprite.crabShuriken;
            useTime = 15;
            useHide = true;
            damage = 2f;
        }

        public override void OnUse()
        {
            Shoot<Entities.Projectiles.Throwables.CrabShuriken>(World.player.heldItem.angleBase, 7f);
        }
    }
}