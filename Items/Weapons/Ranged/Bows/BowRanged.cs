using UnderwaterGame.Entities;

namespace UnderwaterGame.Items.Weapons.Ranged.Bows
{
    public abstract class BowRanged : RangedWeapon
    {
        public override void OnUse(ItemEntity entity)
        {
            entity.Animator.index = 0f;
            entity.Animator.speed = (float)entity.Animator.sprite.Textures.Length / (float)UseTime;
        }
    }
}