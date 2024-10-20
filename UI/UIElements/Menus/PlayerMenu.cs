﻿namespace UnderwaterGame.Ui.UiElements.Menus
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Ui.UiComponents;
    using UnderwaterGame.Ui.UiComponents.Buttons;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class PlayerMenu : MenuElement
    {
        private SlotButton[][,] slots;

        private bool slotsInit;

        public int selectedSlotX;

        public int selectedSlotY;

        public int selectedGroup;

        public override void Draw()
        {
        }

        public override void Init()
        {
            InitIconButtons();
            IconButton optionsButton = AddIconButton(0, Main.textureLibrary.UI_BUTTONS_ICONS_OTHER_OPTIONSICON.asset, () => true);
            optionsButton.selectedInteractAction = delegate ()
            {
                OptionsMenu optionsMenu = (OptionsMenu)UiManager.GetElement<OptionsMenu>();
                optionsMenu.ToggleOpen(true);
            };
            IconButton restartButton = AddIconButton(0, Main.textureLibrary.UI_BUTTONS_ICONS_OTHER_RESTARTICON.asset, () => true);
            restartButton.selectedInteractAction = delegate ()
            {
                Main.restart = true;
                Main.restartTime = 0;
            };
        }

        public override void Update()
        {
            if(World.player?.inventory == null)
            {
                return;
            }
            if(!slotsInit)
            {
                InitSlots();
                LoadSlotGroups();
                slotsInit = true;
            }
            if(Control.KeyPressed(Keys.Escape) && (UiManager.menuCurrent ?? this) == this)
            {
                ToggleOpen();
                if(!open)
                {
                    selectedSlotX = 0;
                    selectedSlotY = 0;
                }
            }
            UpdateAlpha();
            UpdateLoadingWaitComplete();
            if(!open)
            {
                int selectedSlotXMax;
                selectedGroup = World.player.wielding ? (int)PlayerCharacter.InventoryGroup.Wield : (int)PlayerCharacter.InventoryGroup.Hotbar;
                selectedSlotXMax = World.player.inventory.groups[selectedGroup].contents.GetLength(0) - 1;
                selectedSlotX = MathUtilities.Clamp(selectedSlotX, 0, selectedSlotXMax);
                selectedSlotY = 0;
                if(World.player.heldItem.useTimeCurrent >= World.player.heldItem.useTimeMax)
                {
                    if(World.player.wielding)
                    {
                        if(Control.MouseLeftHeld())
                        {
                            selectedSlotX = 0;
                        }
                        if(Control.MouseRightHeld())
                        {
                            selectedSlotX = 1;
                        }
                    }
                    else
                    {
                        if(Control.MouseScrollUp())
                        {
                            if(selectedSlotX > 0f)
                            {
                                selectedSlotX--;
                            }
                            else
                            {
                                selectedSlotX = selectedSlotXMax;
                            }
                        }
                        if(Control.MouseScrollDown())
                        {
                            if(selectedSlotX < selectedSlotXMax)
                            {
                                selectedSlotX++;
                            }
                            else
                            {
                                selectedSlotX = 0;
                            }
                        }
                    }
                }
                World.player.RefreshHeldItem();
            }
        }

        protected void InitSlots()
        {
            int groupCount = World.player.inventory.groups.Length;
            slots = new SlotButton[groupCount][,];
            for(int g = 0; g < groupCount; g++)
            {
                int width = World.player.inventory.groups[g].contents.GetLength(0);
                int height = World.player.inventory.groups[g].contents.GetLength(1);
                slots[g] = new SlotButton[width, height];
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        int tempG = g;
                        int tempX = x;
                        int tempY = y;
                        slots[g][x, y] = (SlotButton)AddComponent<SlotButton>();
                        slots[g][x, y].menuElement = this;
                        slots[g][x, y].slotX = x;
                        slots[g][x, y].slotY = y;
                        slots[g][x, y].slotGroup = g;
                        slots[g][x, y].getInventory = () => World.player.inventory;
                        slots[g][x, y].getSelected = () => open ? slots[tempG][tempX, tempY].touching : selectedGroup == tempG && selectedSlotX == tempX && selectedSlotY == tempY;
                        slots[g][x, y].getSelectedInteract = () => slots[tempG][tempX, tempY].touching;
                        slots[g][x, y].selectedAction += delegate ()
                        {
                            selectedSlotX = tempX;
                            selectedSlotY = tempY;
                            selectedGroup = tempG;
                        };
                    }
                }
            }
        }

        protected void LoadSlotGroup(int groupIndex, Texture2D slotIcon, Func<Vector2> getPosition, Func<float> getAlpha)
        {
            int width = World.player.inventory.groups[groupIndex].contents.GetLength(0);
            int height = World.player.inventory.groups[groupIndex].contents.GetLength(1);
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    int tempX = x;
                    int tempY = y;
                    slots[groupIndex][x, y].icon = slotIcon;
                    slots[groupIndex][x, y].getPosition = () => getPosition() + (new Vector2(tempX, tempY) * UiComponent.gap);
                    slots[groupIndex][x, y].getAlpha = getAlpha;
                }
            }
        }

        protected void LoadSlotGroups()
        {
            Func<Vector2> getPosition = () => new Vector2(Main.GetBufferWidth(), Main.GetBufferHeight()) / 2f;
            Func<int, Vector2> getWieldPosition = (int count) => new Vector2(getPosition().X + (UiComponent.gap * 2.5f * (count == 0 ? -1f : 1f)), Main.GetBufferHeight() - UiComponent.gap);
            Func<int, Vector2> getArmourPosition = (int count) => getPosition() - ((new Vector2(GetInventoryWidth(), GetInventoryHeight()) - Vector2.One) * UiComponent.gap * 0.5f) + new Vector2(0f, UiComponent.gap * count);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.Wield, Main.textureLibrary.UI_BUTTONS_ICONS_INVENTORY_WIELDICON.asset, () => Vector2.Zero, () => 1f);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.Hotbar, Main.textureLibrary.UI_BUTTONS_ICONS_INVENTORY_HOTBARICON.asset, () => new Vector2(getPosition().X - (2f * UiComponent.gap * 0.5f), Main.GetBufferHeight() - UiComponent.gap), () => 1f);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.ArmourHead, Main.textureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.asset, () => getArmourPosition(0), () => alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.ArmourChest, Main.textureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.asset, () => getArmourPosition(1), () => alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.ArmourLegs, Main.textureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.asset, () => getArmourPosition(2), () => alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.ArmourFeet, Main.textureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.asset, () => getArmourPosition(3), () => alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryGroup.Other, null, () => getPosition() - ((new Vector2(GetInventoryWidth(), GetInventoryHeight()) - Vector2.One) * UiComponent.gap * 0.5f) + new Vector2(UiComponent.gap, 0f), () => alpha);
            slots[(int)PlayerCharacter.InventoryGroup.Wield][0, 0].getPosition = () => getWieldPosition(0);
            slots[(int)PlayerCharacter.InventoryGroup.Wield][1, 0].getPosition = () => getWieldPosition(1);
        }

        private int GetInventoryWidth()
        {
            return World.player.inventory.groups[(int)PlayerCharacter.InventoryGroup.ArmourHead].contents.GetLength(0) + World.player.inventory.groups[(int)PlayerCharacter.InventoryGroup.Other].contents.GetLength(0);
        }

        private int GetInventoryHeight()
        {
            return World.player.inventory.groups[(int)PlayerCharacter.InventoryGroup.Other].contents.GetLength(1);
        }

        public bool GetSelected()
        {
            return selectedSlotX != -1 && selectedSlotY != -1 && selectedGroup != -1;
        }

        public Vector2 GetSlotSize()
        {
            return new Vector2(Main.textureLibrary.UI_BUTTONS_OTHER_BUTTON.asset.Width, Main.textureLibrary.UI_BUTTONS_OTHER_BUTTON.asset.Height);
        }
    }
}