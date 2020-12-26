namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

    public class PinkJellyShuriken : ThrowableRanged
    {
        protected override void Init()
        {
            name = "Pink Jelly Shuriken";
            sprite = Sprite.pinkJellyShuriken;
            stack = true;
            useTime = 20;
            useStrength = 1f;
            useHide = true;
            damage = 1;
            strength = 2f;
        }

        public override void OnUse()
        {
            Shoot<Entities.Projectiles.Throwables.PinkJellyShuriken>(World.player.heldItem.angleBase, 7f);
            World.player.heldItem.RemoveItem(1);
        }
    }
}