namespace UnderwaterGame.Items.Weapons.Ranged.Bows
{
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Projectiles.Arrows;
    using UnderwaterGame.Sprites;

    public class WoodenBow : BowRanged
    {
        protected override void Init()
        {
            name = "Wooden Bow";
            sprite = Sprite.woodenBow;
            useTime = 35;
            useAngleUpdate = true;
            damage = 3f;
        }

        public override void WhileUse(ItemEntity entity)
        {
            if(entity.useState != 0 || (int)entity.animator.index != 3)
            {
                return;
            }
            Shoot<WoodenArrow>(entity, entity.angleBase, 7f);
            entity.useState = 1;
        }
    }
}