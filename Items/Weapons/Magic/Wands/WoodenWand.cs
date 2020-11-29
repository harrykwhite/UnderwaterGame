namespace UnderwaterGame.Items.Weapons.Magic.Wands
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Projectiles.Magic;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Worlds;

    public class WoodenWand : WandMagic
    {
        protected override void Init()
        {
            name = "Wooden Wand";
            sprite = Sprite.woodenWand;
            useTime = 40;
            useStrength = 1f;
            damage = 8f;
            strength = 2f;
        }

        public override void OnUse()
        {
            if(World.player.HurtMagic(2f))
            {
                Shoot<FlareMagic>(World.player.heldItem.angleBase, 12f);
            }
        }
    }
}