using UnderwaterGame.Entities;
using UnderwaterGame.Sprites;
using UnderwaterGame.UI;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Items.Weapons.Melee
{
    public abstract class MeleeWeapon : WeaponItem
    {
        protected Sprite swingSprite;

        protected HitEntity hitEntity;
        protected float hitboxOffset = 30f;
        protected int hitboxSize = 23;

        public override void OnUse(ItemEntity entity)
        {
            Swing(entity);
        }

        protected virtual HitEntity Swing(ItemEntity entity)
        {
            hitEntity = (HitEntity)EntityManager.AddEntity<HitEntity>(entity.position);
            hitEntity.position += MathUtilities.LengthDirection(hitboxOffset + entity.lengthOffset, entity.angleBase);

            hitEntity.SetHitInfo(Damage, hitEntity.position, entity.angleBase, false, true);
            hitEntity.SetScaleInfo(hitboxSize, hitboxSize);

            hitEntity.depth = entity.depth + 0.001f;

            entity.SetSwingEffect(swingSprite, hitboxOffset);
            ((GameCursorElement)UIManager.GetElement<GameCursorElement>()).Expand(0.5f);

            return hitEntity;
        }

        protected virtual void SwingUpdate(ItemEntity entity)
        {
            if (hitEntity.GetExists())
            {
                hitEntity.position = entity.position + MathUtilities.LengthDirection(hitboxOffset, entity.angleBase);
            }
        }
    }
}