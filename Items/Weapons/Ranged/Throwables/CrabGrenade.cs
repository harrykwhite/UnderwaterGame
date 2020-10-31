namespace UnderwaterGame.Items.Weapons.Ranged.Throwables
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Sprites;

    public class CrabGrenade : ThrowableRanged
    {
        protected override void Init()
        {
            name = "Crab Grenade";
            sprite = Sprite.crabGrenade;
            useTime = 45;
            useHide = true;
            damage = 8f;
        }

        public override void OnUse(ItemEntity entity)
        {
            Shoot<Entities.Projectiles.Throwables.CrabGrenade>(entity, entity.angleBase, 7f);
        }
    }
}