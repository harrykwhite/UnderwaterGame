namespace UnderwaterGame.Entities.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Threading;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Items;
    using UnderwaterGame.Items.Armours;
    using UnderwaterGame.Items.Armours.Chests;
    using UnderwaterGame.Items.Armours.Feet;
    using UnderwaterGame.Items.Armours.Heads;
    using UnderwaterGame.Items.Armours.Legs;
    using UnderwaterGame.Items.Weapons;
    using UnderwaterGame.Sound;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements;
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
            Crafting,
            Other
        }

        private int bubbleTime;

        private int bubbleTimeMax = 4;

        private float swimAngleTo;

        private float swimAngleToMult = 0.2f;

        private float swimSpeedHor;

        private float swimSpeedVer;

        private float swimSpeedAcc = 0.1f;

        private float swimSpeedMax = 2.5f;

        private float swimSpeedWaterMult;

        private float swimSpeedWaterMultAcc = 0.01f;

        public float magic;

        public float magicMax;

        public ArmourItem armourHead;

        public ArmourItem armourChest;

        public ArmourItem armourLegs;

        public ArmourItem armourFeet;

        public Inventory inventory;

        public ItemEntity heldItem;

        public bool wielding;

        public override void Draw()
        {
            DrawSelf();
            int armourIndex = animator.sprite == Sprite.playerIdle ? Sprite.playerSwim.textures.Length - 1 : (int)animator.index;
            Color armourColor = flashTime > 0 ? Color.White : blend;
            float armourDepth = depth + 0.0005f;
            if(armourHead != null)
            {
                DrawSelf(flashTime > 0 ? armourHead.wearSprite.texturesFilled[armourIndex] : armourHead.wearSprite.textures[armourIndex], color: armourColor, depth: armourDepth);
            }
            if(armourChest != null)
            {
                DrawSelf(flashTime > 0 ? armourChest.wearSprite.texturesFilled[armourIndex] : armourChest.wearSprite.textures[armourIndex], color: armourColor, depth: armourDepth);
            }
            if(armourLegs != null)
            {
                DrawSelf(flashTime > 0 ? armourLegs.wearSprite.texturesFilled[armourIndex] : armourLegs.wearSprite.textures[armourIndex], color: armourColor, depth: armourDepth);
            }
            if(armourFeet != null)
            {
                DrawSelf(flashTime > 0 ? armourFeet.wearSprite.texturesFilled[armourIndex] : armourFeet.wearSprite.textures[armourIndex], color: armourColor, depth: armourDepth);
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
            inventory.groups[(int)InventoryGroup.Crafting] = new Inventory.InventoryGroup(5, 1, false);
            inventory.groups[(int)InventoryGroup.Other] = new Inventory.InventoryGroup(5, 4, true);
            inventory.groups[(int)InventoryGroup.Wield].predicate = (Item item) => item is WeaponItem;
            inventory.groups[(int)InventoryGroup.Hotbar].predicate = (Item item) => !inventory.groups[(int)InventoryGroup.Wield].predicate(item);
            inventory.groups[(int)InventoryGroup.ArmourHead].predicate = (Item item) => item is HeadArmour;
            inventory.groups[(int)InventoryGroup.ArmourChest].predicate = (Item item) => item is ChestArmour;
            inventory.groups[(int)InventoryGroup.ArmourLegs].predicate = (Item item) => item is LegArmour;
            inventory.groups[(int)InventoryGroup.ArmourFeet].predicate = (Item item) => item is FeetArmour;
            heldItem = (ItemEntity)EntityManager.AddEntity<ItemEntity>(position);
            healthMax = 100f;
            health = healthMax;
            magicMax = 100f;
            magic = magicMax;
            Item[] items = Item.items.ToArray();
            for(int i = 0; i < items.Length; i++)
            {
                inventory.AddItem(items[i], 99);
            }
        }

        public override void Update()
        {
            bool keyRight = Control.KeyHeld(Keys.D);
            bool keyLeft = Control.KeyHeld(Keys.A);
            bool keyDown = Control.KeyHeld(Keys.S);
            bool keyUp = Control.KeyHeld(Keys.W);
            bool keyWield = Control.KeyPressed(Keys.F);
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
            if(inWater)
            {
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
                if(swimSpeedWaterMult < 1f)
                {
                    swimSpeedWaterMult += Math.Min(swimSpeedWaterMultAcc, 1f - swimSpeedWaterMult);
                }
            }
            else
            {
                if(swimSpeedWaterMult > 0f)
                {
                    swimSpeedWaterMult -= Math.Min(swimSpeedWaterMultAcc, swimSpeedWaterMult);
                }
            }
            velocity = new Vector2(swimSpeedHor, swimSpeedVer) * swimSpeedWaterMult;
            angleOffset = MathHelper.Pi / 2f;
            if(new Vector2(swimSpeedHor, swimSpeedVer).Length() * swimSpeedWaterMult > 0f)
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
            CheckForDamage(collider);
            UpdateStatus();
            UpdateGravity();
            velocity.Y += gravity;
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
            LockInWorld();
            bool idle = true;
            if(inWater)
            {
                if(new Vector2(swimSpeedHor, swimSpeedVer).Length() * swimSpeedWaterMult > 0f)
                {
                    if(bubbleTime < bubbleTimeMax)
                    {
                        bubbleTime++;
                    }
                    else
                    {
                        Bubble bubble = (Bubble)EntityManager.AddEntity<Bubble>(position);
                        bubble.position += MathUtilities.LengthDirection(10f, angle + angleOffset);
                        bubble.direction = angle + angleOffset;
                        bubbleTime = 0;
                    }
                    animator.sprite = Sprite.playerSwim;
                    animator.speed = 0.25f;
                    idle = false;
                }
            }
            if(idle)
            {
                animator.sprite = Sprite.playerIdle;
                animator.index = 0f;
                animator.speed = 0f;
            }
            animator.Update();
            UpdateWater();
            if(heldItem.useTimeCurrent >= heldItem.useTimeMax)
            {
                if(keyWield)
                {
                    PlayerMenu playerMenu = (PlayerMenu)UiManager.GetElement<PlayerMenu>();
                    playerMenu.selectedSlotX = 0;
                    playerMenu.selectedSlotY = 0;
                    wielding = !wielding;
                }
            }
            RefreshDefense();
            RefreshArmour();
            RefreshHeldItem();
            velocity = Vector2.Zero;
        }

        public override void Destroy()
        {
            base.Destroy();
            heldItem.Destroy();
        }

        public override bool Hurt(HitData hitData)
        {
            bool damaged = base.Hurt(hitData);
            if(damaged)
            {
                ScreenFlashElement screenFlash = (ScreenFlashElement)UiManager.GetElement<ScreenFlashElement>();
                screenFlash.SetFlash(bloodParticleColor, 0.2f);
            }
            return damaged;
        }

        public override void Kill()
        {
            base.Kill();
            SoundManager.PlaySound(Main.soundLibrary.CHARACTERS_PLAYER_DEATH.asset, SoundManager.Category.Sound);
            Main.Restart(true);
        }

        public void RefreshDefense()
        {
            defense = 0f;
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

        public bool HurtMagic(float damage)
        {
            damage = Math.Max(damage, 0f);
            if(magic <= 0f)
            {
                return false;
            }
            magic -= damage;
            magic = MathUtilities.Clamp(magic, 0f, magicMax);
            return true;
        }

        public bool HealMagic(float amount)
        {
            if(invincibleTime > 0 || magic >= magicMax)
            {
                return false;
            }
            amount = Math.Max(amount, 0f);
            magic += amount;
            magic = MathUtilities.Clamp(magic, 0f, magicMax);
            invincibleTime = invincibleTimeMax;
            flickerTime = flickerTimeMax;
            flashTime = flashTimeMax;
            for(int i = 0; i < bloodParticleCount; i++)
            {
                Blood blood = (Blood)EntityManager.AddEntity<Blood>(position);
                blood.direction = ((MathHelper.Pi * 2f) / bloodParticleCount) * i;
            }
            return true;
        }
    }
}