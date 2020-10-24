using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.Input;
using UnderwaterGame.Items;
using UnderwaterGame.Sprites;
using UnderwaterGame.UI;
using UnderwaterGame.UI.UIElements.Menus;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Entities
{
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

        public Item ItemType { get; private set; }

        public int UseTimeCurrent { get; private set; }
        public int UseTimeMax { get; private set; }

        public bool DoUpdate => (ItemType?.UseTime ?? 0) != 0;
        public bool DoDraw => Using && (!ItemType?.UseHide ?? false);

        public bool Using => UseTimeCurrent < UseTimeMax;

        public void SetItem(Item itemType)
        {
            ItemType = itemType;

            if (ItemType != null)
            {
                SetSprite(ItemType.Sprite);
                Animator = new Animator(Sprite);
            }

            angleHoldOffset = 0f;
            angleHoldOffsetTo = 0f;
            angleHoldOffsetSpeed = 0f;

            useState = 0;
            UseTimeCurrent = 0;
            UseTimeMax = 0;

            UpdateAngle();
        }

        public void RemoveItem(int quantity)
        {
            PlayerMenu playerMenu = (PlayerMenu)UIManager.GetElement<PlayerMenu>();

            if (!playerMenu.Open && playerMenu.Selected)
            {
                Main.World.player.Inventory.RemoveItemAt(playerMenu.selectedSlotX, playerMenu.selectedSlotY, playerMenu.selectedGroup, ItemType, quantity);
            }
        }

        public override void Draw()
        {
            if (DoDraw)
            {
                DrawSelf();

                if (swingEffect && !swingEffectAnimator.hidden)
                {
                    Texture2D swingTexture = swingEffectAnimator.sprite.Textures[(int)swingEffectAnimator.index];
                    Vector2 swingOffset = MathUtilities.LengthDirection(swingEffectLength + lengthOffset, angleBase);

                    if (swingTexture != null)
                    {
                        Main.SpriteBatch.Draw(swingTexture, position + swingOffset, null, Color.White, angleBase, swingEffectAnimator.sprite.Origin, 1f, Flip, depth + 0.001f);
                    }
                }
            }
        }

        public override void Init()
        {
            swingEffectAnimator = new Animator(Sprite.LongSwing)
            {
                loopAction = () => swingEffectAnimator.hidden = true
            };
        }

        public override void Update()
        {
            if (!DoUpdate)
            {
                return;
            }

            bool use = false;
            bool useLeft = true;

            bool buttonUse;

            if (Main.World.player.Wielding)
            {
                PlayerMenu playerInventory = (PlayerMenu)UIManager.GetElement<PlayerMenu>();
                useLeft = playerInventory.selectedSlotX == 0;
            }

            buttonUse = useLeft ? (ItemType.UsePress ? InputManager.MouseLeftPressed() : InputManager.MouseLeftHeld()) : (ItemType.UsePress ? InputManager.MouseRightPressed() : InputManager.MouseRightHeld());

            if (UseTimeCurrent < UseTimeMax)
            {
                use = true;
                UseTimeCurrent++;

                ItemType.WhileUse(this);

                if (ItemType.UseAngleUpdate)
                {
                    UpdateAngleBase();
                }

                if (swingEffect)
                {
                    swingEffectAnimator.speed = 0.25f;
                }
            }

            if (UseTimeCurrent >= UseTimeMax)
            {
                if (use)
                {
                    ItemType.EndUse(this);
                }

                swingEffect = false;
                swingEffectAnimator.index = 0f;
                swingEffectAnimator.hidden = false;

                if (buttonUse)
                {
                    if (ItemType.CanUse(this))
                    {
                        UpdateAngleBase();

                        useState = 0;

                        UseTimeCurrent = 0;
                        UseTimeMax = ItemType.UseTime;

                        ItemType.OnUse(this);
                    }
                }
            }

            UpdateAngle();

            Animator.Update();
            swingEffectAnimator.Update();
        }

        public override void EndUpdate()
        {
            float outLength = lengthOffset;
            position = Main.World.player.position;

            if (!DoUpdate)
            {
                return;
            }

            outLength += ItemType.UseOffset;

            position += MathUtilities.LengthDirection(outLength, angleBase);
            position += positionOffset;

            flipVer = MathUtilities.AngleLeftHalf(angleBaseRelative);
            depth = Main.World.player.depth + 0.001f;
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
            angleBase = Main.World.player.AngleToMouse;
            angleBaseRelative = angleBase - Main.World.player.angle;
        }
    }
}