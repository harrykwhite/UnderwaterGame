namespace UnderwaterGame.Items.Weapons.Ranged.Bows
{
    using UnderwaterGame.Entities;

    public abstract class BowRanged : RangedWeapon
    {
        public override void OnUse(ItemEntity entity)
        {
            entity.animator.index = 0f;
            entity.animator.speed = entity.animator.sprite.textures.Length / (float)useTime;
        }
    }
}