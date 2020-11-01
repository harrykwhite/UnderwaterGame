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
            damage = 8f;
        }

        public override void OnUse(ItemEntity entity)
        {
            if(World.player.HurtMagic(2f))
            {
                Shoot<FlareMagic>(entity, entity.angleBase, 12f);
            }
        }
    }
}