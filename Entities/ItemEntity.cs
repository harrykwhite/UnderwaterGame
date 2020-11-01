namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Items;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements.Menus;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class ItemEntity : Entity
    {
        private bool swingEffect;

        private float swingEffectLength;

        private Animator swingEffectAnimator;

        public int useState;

        public float angleBase;

        public float angleBaseRelative;

        private float angleHoldOffset;

        private float angleHoldOffsetTo;

        private float angleHoldOffsetSpeed;

        public Vector2 positionOffset;

        public float lengthOffset;

        public Item itemType;

        public int useTimeCurrent;

        public int useTimeMax;

        public void SetItem(Item itemType)
        {
            this.itemType = itemType;
            if(this.itemType != null)
            {
                SetSprite(this.itemType.sprite);
                animator = new Animator(sprite);
            }
            angleHoldOffset = 0f;
            angleHoldOffsetTo = 0f;
            angleHoldOffsetSpeed = 0f;
            useState = 0;
            useTimeCurrent = 0;
            useTimeMax = 0;
            UpdateAngle();
        }

        public void RemoveItem(int quantity)
        {
            PlayerMenu playerMenu = (PlayerMenu)UiManager.GetElement<PlayerMenu>();
            if(!playerMenu.open && playerMenu.GetSelected())
            {
                World.player.inventory.RemoveItemAt(playerMenu.selectedSlotX, playerMenu.selectedSlotY, playerMenu.selectedGroup, itemType, quantity);
            }
        }

        public override void Draw()
        {
            if(GetDraw())
            {
                DrawSelf();
                if(swingEffect && !swingEffectAnimator.hidden)
                {
                    Texture2D swingTexture = swingEffectAnimator.sprite.textures[(int)swingEffectAnimator.index];
                    Vector2 swingOffset = MathUtilities.LengthDirection(swingEffectLength + lengthOffset, angleBase);
                    if(swingTexture != null)
                    {
                        Main.spriteBatch.Draw(swingTexture, position + swingOffset, null, Color.White, angleBase, swingEffectAnimator.sprite.origin, 1f, GetFlip(), depth + 0.001f);
                    }
                }
            }
        }

        public override void Init()
        {
            swingEffectAnimator = new Animator(Sprite.longSwing) { loopAction = () => swingEffectAnimator.hidden = true };
        }

        public override void Update()
        {
            if(!GetUpdate())
            {
                return;
            }
            bool use = false;
            bool useLeft = true;
            bool buttonUse;
            if(World.player.wielding)
            {
                PlayerMenu playerInventory = (PlayerMenu)UiManager.GetElement<PlayerMenu>();
                useLeft = playerInventory.selectedSlotX == 0;
            }
            buttonUse = useLeft ? (itemType.usePress ? Control.MouseLeftPressed() : Control.MouseLeftHeld()) : (itemType.usePress ? Control.MouseRightPressed() : Control.MouseRightHeld());
            if(useTimeCurrent < useTimeMax)
            {
                use = true;
                useTimeCurrent++;
                itemType.WhileUse(this);
                if(itemType.useAngleUpdate)
                {
                    UpdateAngleBase();
                }
                if(swingEffect)
                {
                    swingEffectAnimator.speed = 0.25f;
                }
            }
            if(useTimeCurrent >= useTimeMax)
            {
                if(use)
                {
                    itemType.EndUse(this);
                }
                swingEffect = false;
                swingEffectAnimator.index = 0f;
                swingEffectAnimator.hidden = false;
                if(buttonUse)
                {
                    if(itemType.CanUse(this))
                    {
                        UpdateAngleBase();
                        useState = 0;
                        useTimeCurrent = 0;
                        useTimeMax = itemType.useTime;
                        itemType.OnUse(this);
                    }
                }
            }
            UpdateAngle();
            animator.Update();
            swingEffectAnimator.Update();
        }

        public override void EndUpdate()
        {
            float outLength = lengthOffset;
            position = World.player.position;
            if(!GetUpdate())
            {
                return;
            }
            outLength += itemType.useOffset;
            position += MathUtilities.LengthDirection(outLength, angleBase);
            position += positionOffset;
            flipVer = MathUtilities.AngleLeftHalf(angleBaseRelative);
            depth = World.player.depth + 0.001f;
        }

        public void SetSwingEffect(Sprite sprite, float length)
        {
            swingEffectAnimator.sprite = sprite;
            swingEffect = true;
            swingEffectLength = length;
        }

        public void SetAngleHoldOffset(float offset, float offsetTo, int time)
        {
            angleHoldOffset = offset;
            angleHoldOffsetTo = offsetTo;
            angleHoldOffsetSpeed = Math.Abs(MathUtilities.AngleDifference(angleHoldOffset, angleHoldOffsetTo)) / (time / 2);
        }

        private void UpdateAngle()
        {
            float difference = MathUtilities.AngleDifference(angleHoldOffset, angleHoldOffsetTo);
            angleHoldOffset += Math.Sign(difference) * Math.Min(angleHoldOffsetSpeed, Math.Abs(difference));
            angle = angleBase + angleHoldOffset;
        }

        private void UpdateAngleBase()
        {
            angleBase = MathUtilities.PointDirection(World.player.position, Control.GetMousePositionWorld());
            angleBaseRelative = angleBase - World.player.angle;
        }

        private bool GetUpdate()
        {
            return (itemType?.useTime ?? 0) != 0;
        }

        private bool GetDraw()
        {
            return useTimeCurrent < useTimeMax && (!itemType?.useHide ?? false);
        }
    }
}