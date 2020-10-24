using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using UnderwaterGame.Entities.Particles;
using UnderwaterGame.Input;
using UnderwaterGame.Items;
using UnderwaterGame.Items.Armours;
using UnderwaterGame.Items.Armours.Chests;
using UnderwaterGame.Items.Armours.Feet;
using UnderwaterGame.Items.Armours.Heads;
using UnderwaterGame.Items.Armours.Legs;
using UnderwaterGame.Items.Weapons;
using UnderwaterGame.Sound;
using UnderwaterGame.Sprites;
using UnderwaterGame.UI;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.UI.UIElements.Menus;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Entities.Characters
{
    public class PlayerCharacter : CharacterEntity
    {
        public enum InventoryType
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

        private float swimSpeedThreshold = 1f;

        public float Magic { get; protected set; }
        public float MagicMax { get; protected set; }

        public ArmourItem ArmourHead { get; private set; }
        public ArmourItem ArmourChest { get; private set; }
        public ArmourItem ArmourLegs { get; private set; }
        public ArmourItem ArmourFeet { get; private set; }

        public Inventory Inventory { get; private set; }

        public Inventory.InventoryGroup InventoryWield => Inventory.Groups[(int)InventoryType.Wield];
        public Inventory.InventoryGroup InventoryHotbar => Inventory.Groups[(int)InventoryType.Hotbar];
        public Inventory.InventoryGroup InventoryArmourHead => Inventory.Groups[(int)InventoryType.ArmourHead];
        public Inventory.InventoryGroup InventoryArmourChest => Inventory.Groups[(int)InventoryType.ArmourChest];
        public Inventory.InventoryGroup InventoryArmourLegs => Inventory.Groups[(int)InventoryType.ArmourLegs];
        public Inventory.InventoryGroup InventoryArmourFeet => Inventory.Groups[(int)InventoryType.ArmourFeet];
        public Inventory.InventoryGroup InventoryCrafting => Inventory.Groups[(int)InventoryType.Crafting];
        public Inventory.InventoryGroup InventoryOther => Inventory.Groups[(int)InventoryType.Other];

        public ItemEntity HeldItem { get; private set; }
        public bool Wielding { get; private set; }

        public float AngleToMouse => MathUtilities.PointDirection(position, InputManager.GetMousePositionWorld());

        public override void Draw()
        {
            DrawSelf();

            int armourIndex = Animator.sprite == Sprite.PlayerIdle ? Sprite.PlayerSwim.Textures.Length - 1 : (int)Animator.index;
            Color armourColor = Flash ? Color.White : blend;
            float armourDepth = depth + 0.0005f;

            if (ArmourHead != null)
            {
                DrawSelf(Flash ? ArmourHead.WearSprite.TexturesFilled[armourIndex] : ArmourHead.WearSprite.Textures[armourIndex], color: armourColor, depth: armourDepth);
            }

            if (ArmourChest != null)
            {
                DrawSelf(Flash ? ArmourChest.WearSprite.TexturesFilled[armourIndex] : ArmourChest.WearSprite.Textures[armourIndex], color: armourColor, depth: armourDepth);
            }

            if (ArmourLegs != null)
            {
                DrawSelf(Flash ? ArmourLegs.WearSprite.TexturesFilled[armourIndex] : ArmourLegs.WearSprite.Textures[armourIndex], color: armourColor, depth: armourDepth);
            }

            if (ArmourFeet != null)
            {
                DrawSelf(Flash ? ArmourFeet.WearSprite.TexturesFilled[armourIndex] : ArmourFeet.WearSprite.Textures[armourIndex], color: armourColor, depth: armourDepth);
            }
        }

        public override void Init()
        {
            SetSprite(Sprite.PlayerSwim);
            Animator = new Animator(Sprite);

            depth = 0.55f;

            Inventory = new Inventory(Enum.GetNames(typeof(InventoryType)).Length);

            Inventory.Groups[(int)InventoryType.Wield] = new Inventory.InventoryGroup(2, 1, true);
            Inventory.Groups[(int)InventoryType.Hotbar] = new Inventory.InventoryGroup(3, 1, true);
            Inventory.Groups[(int)InventoryType.ArmourHead] = new Inventory.InventoryGroup(1, 1, false);
            Inventory.Groups[(int)InventoryType.ArmourChest] = new Inventory.InventoryGroup(1, 1, false);
            Inventory.Groups[(int)InventoryType.ArmourLegs] = new Inventory.InventoryGroup(1, 1, false);
            Inventory.Groups[(int)InventoryType.ArmourFeet] = new Inventory.InventoryGroup(1, 1, false);
            Inventory.Groups[(int)InventoryType.Crafting] = new Inventory.InventoryGroup(5, 1, false);
            Inventory.Groups[(int)InventoryType.Other] = new Inventory.InventoryGroup(5, 4, true);

            Inventory.Groups[(int)InventoryType.Wield].predicate = (Item item) => item is WeaponItem;
            Inventory.Groups[(int)InventoryType.Hotbar].predicate = (Item item) => !Inventory.Groups[(int)InventoryType.Wield].predicate.Invoke(item);
            Inventory.Groups[(int)InventoryType.ArmourHead].predicate = (Item item) => item is HeadArmour;
            Inventory.Groups[(int)InventoryType.ArmourChest].predicate = (Item item) => item is ChestArmour;
            Inventory.Groups[(int)InventoryType.ArmourLegs].predicate = (Item item) => item is LegArmour;
            Inventory.Groups[(int)InventoryType.ArmourFeet].predicate = (Item item) => item is FeetArmour;

            HeldItem = (ItemEntity)EntityManager.AddEntity<ItemEntity>(position);

            HealthMax = 100f;
            Health = HealthMax;

            MagicMax = 100f;
            Magic = MagicMax;
        }

        public override void Update()
        {
            bool keyRight = InputManager.KeyHeld(Keys.D);
            bool keyLeft = InputManager.KeyHeld(Keys.A);

            bool keyDown = InputManager.KeyHeld(Keys.S);
            bool keyUp = InputManager.KeyHeld(Keys.W);

            bool keyWield = InputManager.KeyPressed(Keys.F);

            Vector2 swimVector = Vector2.Zero;

            if (keyRight)
            {
                swimVector.X += 1f;
            }

            if (keyLeft)
            {
                swimVector.X += -1f;
            }

            if (keyDown)
            {
                swimVector.Y += 1f;
            }

            if (keyUp)
            {
                swimVector.Y += -1f;
            }

            if (InWater)
            {
                if (swimVector.X == 1f)
                {
                    if (swimSpeedHor < swimSpeedMax)
                    {
                        swimSpeedHor += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeedHor);
                    }
                }
                else if (swimVector.X == -1f)
                {
                    if (swimSpeedHor > -swimSpeedMax)
                    {
                        swimSpeedHor -= Math.Min(swimSpeedAcc, swimSpeedHor + swimSpeedMax);
                    }
                }
                else if (swimVector.X == 0f)
                {
                    if (swimSpeedHor != 0f)
                    {
                        swimSpeedHor -= Math.Sign(swimSpeedHor) * Math.Min(Math.Abs(swimSpeedHor), swimSpeedAcc);
                    }
                }

                if (swimVector.Y == 1f)
                {
                    if (swimSpeedVer < swimSpeedMax)
                    {
                        swimSpeedVer += Math.Min(swimSpeedAcc, swimSpeedMax - swimSpeedVer);
                    }
                }
                else if (swimVector.Y == -1f)
                {
                    if (swimSpeedVer > -swimSpeedMax)
                    {
                        swimSpeedVer -= Math.Min(swimSpeedAcc, swimSpeedVer + swimSpeedMax);
                    }
                }
                else if (swimVector.Y == 0f)
                {
                    if (swimSpeedVer != 0f)
                    {
                        swimSpeedVer -= Math.Sign(swimSpeedVer) * Math.Min(Math.Abs(swimSpeedVer), swimSpeedAcc);
                    }
                }

                if (swimSpeedWaterMult < 1f)
                {
                    swimSpeedWaterMult += Math.Min(swimSpeedWaterMultAcc, 1f - swimSpeedWaterMult);
                }
            }
            else
            {
                if (swimSpeedWaterMult > 0f)
                {
                    swimSpeedWaterMult -= Math.Min(swimSpeedWaterMultAcc, swimSpeedWaterMult);
                }
            }

            velocity = new Vector2(swimSpeedHor, swimSpeedVer) * swimSpeedWaterMult;

            angleOffset = MathHelper.Pi / 2f;

            if (new Vector2(swimSpeedHor, swimSpeedVer).Length() * swimSpeedWaterMult >= swimSpeedThreshold)
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

            CheckForDamage(Collider);
            UpdateStatus();

            UpdateGravity();
            velocity.Y += gravity;

            TileCollisions(Vector2.Zero);

            if (HeldItem.Using)
            {
                flipHor = MathUtilities.AngleLeftHalf(HeldItem.angleBase - angle);
            }
            else
            {
                if (swimSpeedHor != 0f)
                {
                    flipHor = Math.Sign(swimSpeedHor) == -1f;
                }
            }

            position += velocity;
            LockInWorld();

            bool idle = true;

            if (InWater)
            {
                if (new Vector2(swimSpeedHor, swimSpeedVer).Length() * swimSpeedWaterMult >= swimSpeedThreshold)
                {
                    if (bubbleTime > 0)
                    {
                        bubbleTime--;
                    }
                    else
                    {
                        Bubble bubble = (Bubble)EntityManager.AddEntity<Bubble>(position);
                        bubble.position += MathUtilities.LengthDirection(10f, angle + angleOffset);
                        bubble.direction = angle + angleOffset;

                        bubbleTime = bubbleTimeMax;
                    }

                    Animator.sprite = Sprite.PlayerSwim;
                    Animator.speed = 0.25f;

                    idle = false;
                }
            }

            if (idle)
            {
                Animator.sprite = Sprite.PlayerIdle;
                Animator.index = 0f;
                Animator.speed = 0f;
            }

            Animator.Update();

            UpdateWater();

            if (!HeldItem?.Using ?? true)
            {
                if (keyWield)
                {
                    PlayerMenu playerMenu = (PlayerMenu)UIManager.GetElement<PlayerMenu>();
                    playerMenu.selectedSlotX = 0;
                    playerMenu.selectedSlotY = 0;

                    Wielding = !Wielding;
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
            HeldItem.Destroy();
        }

        public override bool Hurt(HitInfo hitInfo)
        {
            bool damaged = base.Hurt(hitInfo);

            if (damaged)
            {
                ScreenFlashElement screenFlash = (ScreenFlashElement)UIManager.GetElement<ScreenFlashElement>();
                screenFlash.SetFlash(bloodParticleColor, 0.2f);
            }

            return damaged;
        }

        public override bool Heal(float amount)
        {
            return base.Heal(amount);
        }

        public override void Kill()
        {
            base.Kill();

            SoundManager.PlaySound(Main.SoundLibrary.CHARACTERS_PLAYER_DEATH.Asset, SoundManager.Category.Sound);

            Main.loading = new Thread(delegate ()
            {
                Respawn();
                Main.loading = null;
            });

            Main.loading.Start();
        }

        public void Respawn()
        {
            while (UIManager.FadeElements[2].Alpha < UIManager.FadeElements[2].alphaMax)
            {
                continue;
            }

            Main.World.player = (PlayerCharacter)EntityManager.AddEntity<PlayerCharacter>(Main.World.playerSpawn);

            Camera.positionTo = Main.World.player.position;
            Camera.position = Camera.positionTo;
        }

        public void RefreshDefense()
        {
            Defense = 0f;
        }

        public void RefreshArmour()
        {
            ArmourHead = (ArmourItem)InventoryArmourHead.contents[0, 0].item;
            ArmourChest = (ArmourItem)InventoryArmourChest.contents[0, 0].item;
            ArmourLegs = (ArmourItem)InventoryArmourLegs.contents[0, 0].item;
            ArmourFeet = (ArmourItem)InventoryArmourFeet.contents[0, 0].item;

            if (ArmourHead != null)
            {
                Defense += ArmourHead.WearDefense;
            }

            if (ArmourChest != null)
            {
                Defense += ArmourChest.WearDefense;
            }

            if (ArmourLegs != null)
            {
                Defense += ArmourLegs.WearDefense;
            }

            if (ArmourFeet != null)
            {
                Defense += ArmourFeet.WearDefense;
            }
        }

        public void RefreshHeldItem()
        {
            PlayerMenu playerInventory = (PlayerMenu)UIManager.GetElement<PlayerMenu>();
            Item item = null;

            if (playerInventory.Open)
            {
                return;
            }

            if (playerInventory.Selected)
            {
                item = Inventory.Groups[playerInventory.selectedGroup].contents[playerInventory.selectedSlotX, playerInventory.selectedSlotY].item;
            }

            if (HeldItem.ItemType != item)
            {
                HeldItem.SetItem(item);
            }
        }

        public bool HealMagic(float amount)
        {
            amount = Math.Max(amount, 0f);

            if (Magic >= MagicMax)
            {
                return false;
            }

            Magic += amount;
            Magic = MathUtilities.Clamp(Magic, 0f, MagicMax);

            return true;
        }

        public bool HurtMagic(float damage)
        {
            damage = Math.Max(damage, 0f);

            if (Magic <= 0f)
            {
                return false;
            }

            Magic -= damage;
            Magic = MathUtilities.Clamp(Magic, 0f, MagicMax);

            return true;
        }
    }
}