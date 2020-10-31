namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Sprites;

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

        public override void OnUse(ItemEntity entity)
        {
            Shoot<Entities.Projectiles.Throwables.CrabShuriken>(entity, entity.angleBase, 7f);
        }
    }
}