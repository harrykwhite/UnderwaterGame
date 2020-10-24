using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Projectiles.Arrows;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Weapons.Ranged.Bows
{
    public class WoodenBow : BowRanged
    {
        protected override void Init()
        {
            Name = "Wooden Bow";
            Sprite = Sprite.WoodenBow;

            UseTime = 35;
            UseAngleUpdate = true;

            Damage = 4f;
        }

        public override void WhileUse(ItemEntity entity)
        {
            if (entity.useState != 0 || (int)entity.Animator.index != 3)
            {
                return;
            }

            Shoot<WoodenArrow>(entity, entity.angleBase, 7f);
            entity.useState = 1;
        }
    }
}