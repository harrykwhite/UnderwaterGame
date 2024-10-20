﻿namespace UnderwaterGame.Entities.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Items;
    using UnderwaterGame.Items.Armours;
    using UnderwaterGame.Items.Armours.Chests;
    using UnderwaterGame.Items.Armours.Feet;
    using UnderwaterGame.Items.Armours.Heads;
    using UnderwaterGame.Items.Armours.Legs;
    using UnderwaterGame.Items.Weapons;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements.Menus;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class PlayerCharacter : CharacterEntity
    {
        public enum InventoryGroup
        {
            Wield,

            Hotbar,

            ArmourHead,

            ArmourChest,

            ArmourLegs,

            ArmourFeet,

            Other
        }

        private float bubbleSmallTime;

        private float bubbleSmallTimeMax = 10f;

        private float swimAngleTo;

        private float swimAngleToMult = 0.1f;

        private float swimSpeedHor;

        private float swimSpeedVer;

        private float swimSpeedAcc = 0.1f;

        private float swimSpeedMax = 2f;

        public ArmourItem armourHead;

        public ArmourItem armourChest;

        public ArmourItem armourLegs;

        public ArmourItem armourFeet;

        public Inventory inventory;

        public ItemEntity heldItem;

        public bool wielding = true;

        public override void Draw()
        {
            DrawSelf();
            DrawFlash();
            int armourIndex = animator.sprite == Sprite.playerIdle ? Sprite.playerSwim.textures.Length - 1 : (int)animator.index;
            float armourDepth = depth + 0.002f;
            if(armourHead != null)
            {
                DrawSelf(armourHead.wearSprite.textures[armourIndex], depth: armourDepth);
            }
            if(armourChest != null)
            {
                DrawSelf(armourChest.wearSprite.textures[armourIndex], depth: armourDepth);
            }
            if(armourLegs != null)
            {
                DrawSelf(armourLegs.wearSprite.textures[armourIndex], depth: armourDepth);
            }
            if(armourFeet != null)
            {
                DrawSelf(armourFeet.wearSprite.textures[armourIndex], depth: armourDepth);
            }
        }

        public override void Init()
        {
            SetSprite(Sprite.playerSwim, true);
            animator = new Animator(sprite);
            depth = 0.55f;
            inventory = new Inventory(Enum.GetNames(typeof(InventoryGroup)).Length);
            inventory.groups[(int)InventoryGroup.Wield] = new Inventory.InventoryGroup(2, 1, true);
            inventory.groups[(int)InventoryGroup.Hotbar] = new Inventory.InventoryGroup(3, 1, true);
            inventory.groups[(int)InventoryGroup.ArmourHead] = new Inventory.InventoryGroup(1, 1, false);
            inventory.groups[(int)InventoryGroup.ArmourChest] = new Inventory.InventoryGroup(1, 1, false);
            inventory.groups[(int)InventoryGroup.ArmourLegs] = new Inventory.InventoryGroup(1, 1, false);
            inventory.groups[(int)InventoryGroup.ArmourFeet] = new Inventory.InventoryGroup(1, 1, false);
            inventory.groups[(int)InventoryGroup.Other] = new Inventory.InventoryGroup(5, 4, true);
            inventory.groups[(int)InventoryGroup.Wield].predicate = (Item item) => item is WeaponItem;
            inventory.groups[(int)InventoryGroup.Hotbar].predicate = (Item item) => !inventory.groups[(int)InventoryGroup.Wield].predicate(item);
            inventory.groups[(int)InventoryGroup.ArmourHead].predicate = (Item item) => item is HeadArmour;
            inventory.groups[(int)InventoryGroup.ArmourChest].predicate = (Item item) => item is ChestArmour;
            inventory.groups[(int)InventoryGroup.ArmourLegs].predicate = (Item item) => item is LegArmour;
            inventory.groups[(int)InventoryGroup.ArmourFeet].predicate = (Item item) => item is FeetArmour;
            heldItem = (ItemEntity)EntityManager.AddEntity<ItemEntity>(position);
            healthMax = 6;
            health = healthMax;
        }

        public override void Update()
        {
            bool keyRight = Control.KeyHeld(Keys.D);
            bool keyLeft = Control.KeyHeld(Keys.A);
            bool keyDown = Control.KeyHeld(Keys.S);
            bool keyUp = Control.KeyHeld(Keys.W);
            bool keyWieldPressed = Control.KeyPressed(Keys.F);
            Vector2 swimVector = Vector2.Zero;
            if(keyRight)
            {
                swimVector.X += 1f;
            }
            if(keyLeft)
            {
                swimVector.X += -1f;
            }
            if(keyDown)
            {
                swimVector.Y += 1f;
            }
            if(keyUp)
            {
                swimVector.Y += -1f;
            }
            if(swimVector.X == 1f)
            {
                if(swimSpeedHor < swimSpeedMax)
                {
                    swimSpeedHor += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeedHor);
                }
            }
            else if(swimVector.X == -1f)
            {
                if(swimSpeedHor > -swimSpeedMax)
                {
                    swimSpeedHor -= Math.Min(swimSpeedAcc, swimSpeedHor + swimSpeedMax);
                }
            }
            else if(swimVector.X == 0f)
            {
                if(swimSpeedHor != 0f)
                {
                    swimSpeedHor -= Math.Sign(swimSpeedHor) * Math.Min(Math.Abs(swimSpeedHor), swimSpeedAcc);
                }
            }
            if(swimVector.Y == 1f)
            {
                if(swimSpeedVer < swimSpeedMax)
                {
                    swimSpeedVer += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeedVer);
                }
            }
            else if(swimVector.Y == -1f)
            {
                if(swimSpeedVer > -swimSpeedMax)
                {
                    swimSpeedVer -= Math.Min(swimSpeedAcc, swimSpeedVer + swimSpeedMax);
                }
            }
            else if(swimVector.Y == 0f)
            {
                if(swimSpeedVer != 0f)
                {
                    swimSpeedVer -= Math.Sign(swimSpeedVer) * Math.Min(Math.Abs(swimSpeedVer), swimSpeedAcc);
                }
            }
            velocity = new Vector2(swimSpeedHor, swimSpeedVer);
            angleOffset = MathHelper.Pi / 2f;
            if(velocity.Length() > 0f)
            {
                swimAngleTo = MathUtilities.PointDirection(Vector2.Zero, velocity);
                swimAngleTo += angleOffset;
            }
            else
            {
                swimAngleTo = 0f;
            }
            float angleDifference = MathUtilities.AngleDifference(angle, swimAngleTo);
            angle += Math.Abs(angleDifference * swimAngleToMult) * Math.Sign(angleDifference);
            UpdateStatus();
            TileCollisions();
            if(heldItem.useTimeCurrent < heldItem.useTimeMax)
            {
                flipHor = MathUtilities.AngleLeftHalf(heldItem.angleBase - angle);
            }
            else
            {
                if(swimSpeedHor != 0f)
                {
                    flipHor = Math.Sign(swimSpeedHor) == -1f;
                }
            }
            position += velocity;
            position.X = MathUtilities.Clamp(position.X, 0f, World.width * Tile.size);
            position.Y = MathUtilities.Clamp(position.Y, 0f, World.height * Tile.size);
            bool idle = true;
            if(new Vector2(swimSpeedHor, swimSpeedVer).Length() > 0f)
            {
                if(bubbleSmallTime < bubbleSmallTimeMax)
                {
                    bubbleSmallTime += new Vector2(swimSpeedHor, swimSpeedVer).Length() / swimSpeedMax;
                }
                else
                {
                    BubbleSmall bubbleSmall = (BubbleSmall)EntityManager.AddEntity<BubbleSmall>(position);
                    bubbleSmall.position += MathUtilities.LengthDirection(10.5f, angle + angleOffset);
                    bubbleSmall.direction = angle + angleOffset;
                    bubbleSmallTime = 0f;
                }
                animator.sprite = Sprite.playerSwim;
                animator.speed = 0.2f;
                idle = false;
            }
            if(idle)
            {
                animator.sprite = Sprite.playerIdle;
                animator.index = 0f;
                animator.speed = 0f;
            }
            animator.Update();
            if(heldItem.useTimeCurrent >= heldItem.useTimeMax)
            {
                if(keyWieldPressed)
                {
                    PlayerMenu playerMenu = (PlayerMenu)UiManager.GetElement<PlayerMenu>();
                    playerMenu.selectedSlotX = 0;
                    playerMenu.selectedSlotY = 0;
                    wielding = !wielding;
                }
            }
            defense = 0;
            RefreshArmour();
            RefreshHeldItem();
            velocity = Vector2.Zero;
        }

        public override void Destroy()
        {
            base.Destroy();
            heldItem.Destroy();
        }

        public override bool Hurt(Hit hit)
        {
            bool hurt = base.Hurt(hit);
            if(hurt)
            {
                UiManager.fadeElements[0].alpha = UiManager.fadeElements[0].alphaMax;
            }
            return hurt;
        }

        public override void Kill()
        {
            base.Kill();
            Main.restart = true;
            Main.restartTime = 60;
        }

        public void RefreshArmour()
        {
            armourHead = (ArmourItem)inventory.groups[(int)InventoryGroup.ArmourHead].contents[0, 0].item;
            armourChest = (ArmourItem)inventory.groups[(int)InventoryGroup.ArmourChest].contents[0, 0].item;
            armourLegs = (ArmourItem)inventory.groups[(int)InventoryGroup.ArmourLegs].contents[0, 0].item;
            armourFeet = (ArmourItem)inventory.groups[(int)InventoryGroup.ArmourFeet].contents[0, 0].item;
            if(armourHead != null)
            {
                defense += armourHead.wearDefense;
            }
            if(armourChest != null)
            {
                defense += armourChest.wearDefense;
            }
            if(armourLegs != null)
            {
                defense += armourLegs.wearDefense;
            }
            if(armourFeet != null)
            {
                defense += armourFeet.wearDefense;
            }
        }

        public void RefreshHeldItem()
        {
            PlayerMenu playerInventory = (PlayerMenu)UiManager.GetElement<PlayerMenu>();
            Item item = null;
            if(playerInventory.open)
            {
                return;
            }
            if(playerInventory.GetSelected())
            {
                item = inventory.groups[playerInventory.selectedGroup].contents[playerInventory.selectedSlotX, playerInventory.selectedSlotY].item;
            }
            if(heldItem.itemType != item)
            {
                heldItem.SetItem(item);
            }
        }
    }
}