namespace UnderwaterGame.Items.Weapons.Melee
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class MeleeWeapon : WeaponItem
    {
        protected Sprite swingSprite;

        protected HitEntity hitEntity;

        protected float hitboxOffset = 30f;

        protected int hitboxSize = 23;

        public override void OnUse()
        {
            Swing();
        }

        protected virtual HitEntity Swing()
        {
            hitEntity = (HitEntity)EntityManager.AddEntity<HitEntity>(World.player.heldItem.position);
            hitEntity.position += MathUtilities.LengthDirection(hitboxOffset + World.player.heldItem.lengthOffset, World.player.heldItem.angleBase);
            hitEntity.SetHitData(damage, strength, hitEntity.position, World.player.heldItem.angleBase, false, true);
            hitEntity.collider.shape.width = hitEntity.collider.shape.height = hitboxSize;
            hitEntity.collider.shape.Clear();
            hitEntity.depth = World.player.heldItem.depth + 0.001f;
            World.player.heldItem.SetSwingEffect(swingSprite, hitboxOffset);
            World.player.knockbackSpeed += useStrength;
            World.player.knockbackDirection = World.player.heldItem.angleBase;
            ((GameCursorElement)UiManager.GetElement<GameCursorElement>()).scale += new Vector2(0.5f);
            return hitEntity;
        }

        protected virtual void SwingUpdate()
        {
            if(hitEntity.GetExists())
            {
                hitEntity.position = World.player.heldItem.position + MathUtilities.LengthDirection(hitboxOffset, World.player.heldItem.angleBase);
            }
        }
    }
}