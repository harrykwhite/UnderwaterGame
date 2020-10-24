using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using UnderwaterGame.Entities.Characters;
using UnderwaterGame.Input;
using UnderwaterGame.Items;
using UnderwaterGame.UI.UIComponents;
using UnderwaterGame.UI.UIComponents.Buttons;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIElements.Menus
{
    public class PlayerMenu : MenuElement
    {
        private SlotButton[][,] slots;
        private bool slotsInit;

        public int selectedSlotX;
        public int selectedSlotY;
        public int selectedGroup;

        private int InventoryWidth => Inventory.Groups[(int)PlayerCharacter.InventoryType.ArmourHead].contents.GetLength(0) + Inventory.Groups[(int)PlayerCharacter.InventoryType.Other].contents.GetLength(0);
        private int InventoryHeight => Inventory.Groups[(int)PlayerCharacter.InventoryType.Other].contents.GetLength(1) + Inventory.Groups[(int)PlayerCharacter.InventoryType.Crafting].contents.GetLength(1) + 1;

        public Inventory Inventory => Main.World?.player?.Inventory;
        public bool Selected => selectedSlotX != -1 && selectedSlotY != -1 && selectedGroup != -1;

        public Vector2 SlotSize => new Vector2(Main.TextureLibrary.UI_BUTTONS_OTHER_BUTTON.Asset.Width, Main.TextureLibrary.UI_BUTTONS_OTHER_BUTTON.Asset.Height);

        public override void Draw()
        {

        }

        public override void Init()
        {
            InitIconButtons();

            IconButton optionsButton = AddIconButton(0, Main.TextureLibrary.UI_BUTTONS_ICONS_OTHER_OPTIONSICON.Asset, () => true);
            optionsButton.selectedInteractAction = delegate ()
            {
                OptionsMenu optionsMenu = (OptionsMenu)UIManager.GetElement<OptionsMenu>();
                optionsMenu.ToggleOpen(true);
            };
        }

        public override void Update()
        {
            if (Inventory == null)
            {
                return;
            }

            if (!slotsInit)
            {
                InitSlots();
                LoadSlotGroups();

                slotsInit = true;
            }

            if (InputManager.KeyPressed(Keys.Escape) && (!UIManager.InMenu || UIManager.MenuCurrent == this) && ((!Main.World.player.HeldItem?.Using ?? false) || Open))
            {
                ToggleOpen();

                if (!Open)
                {
                    selectedSlotX = 0;
                    selectedSlotY = 0;
                }
            }

            UpdateAlpha();
            UpdateLoadingWaitComplete();

            if (!Open)
            {
                int selectedSlotXMax;

                selectedGroup = Main.World.player.Wielding ? (int)PlayerCharacter.InventoryType.Wield : (int)PlayerCharacter.InventoryType.Hotbar;
                selectedSlotXMax = Inventory.Groups[selectedGroup].contents.GetLength(0) - 1;

                selectedSlotX = MathUtilities.Clamp(selectedSlotX, 0, selectedSlotXMax);
                selectedSlotY = 0;

                if (!Main.World.player.HeldItem?.Using ?? false)
                {
                    if (Main.World.player.Wielding)
                    {
                        if (InputManager.MouseLeftHeld())
                        {
                            selectedSlotX = 0;
                        }

                        if (InputManager.MouseRightHeld())
                        {
                            selectedSlotX = 1;
                        }
                    }
                    else
                    {
                        if (InputManager.MouseScrollUp())
                        {
                            if (selectedSlotX > 0f)
                            {
                                selectedSlotX--;
                            }
                            else
                            {
                                selectedSlotX = selectedSlotXMax;
                            }
                        }

                        if (InputManager.MouseScrollDown())
                        {
                            if (selectedSlotX < selectedSlotXMax)
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

                Main.World.player.RefreshHeldItem();
            }
        }

        protected void InitSlots()
        {
            int groupCount = Inventory.Groups.Length;
            slots = new SlotButton[groupCount][,];

            for (int g = 0; g < groupCount; g++)
            {
                int width = Inventory.Groups[g].contents.GetLength(0);
                int height = Inventory.Groups[g].contents.GetLength(1);

                slots[g] = new SlotButton[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int tempG = g;
                        int tempX = x;
                        int tempY = y;

                        slots[g][x, y] = (SlotButton)AddComponent<SlotButton>();
                        slots[g][x, y].menuElement = this;

                        slots[g][x, y].slotX = x;
                        slots[g][x, y].slotY = y;
                        slots[g][x, y].slotGroup = g;

                        slots[g][x, y].getInventory = () => Inventory;
                        slots[g][x, y].getSelected = () => Open ? slots[tempG][tempX, tempY].touching : selectedGroup == tempG && selectedSlotX == tempX && selectedSlotY == tempY;
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
            int width = Inventory.Groups[groupIndex].contents.GetLength(0);
            int height = Inventory.Groups[groupIndex].contents.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int tempX = x;
                    int tempY = y;

                    slots[groupIndex][x, y].icon = slotIcon;

                    slots[groupIndex][x, y].getPosition = () => getPosition.Invoke() + (new Vector2(tempX, tempY) * UIComponent.Gap);
                    slots[groupIndex][x, y].getAlpha = getAlpha;
                }
            }
        }

        protected void LoadSlotGroups()
        {
            Func<int, float> getWieldAlpha = delegate (int count)
            {
                if (Main.World?.player == null)
                {
                    return 0f;
                }

                bool opaque = Main.World.player.Wielding;

                if (count == 1)
                {
                    opaque = !opaque;
                }

                return Math.Max(Alpha, opaque ? 1f : 0.5f);
            };

            Func<Vector2> getPosition = () => UIManager.Size / 2f;

            Func<int, Vector2> getWieldPosition = (int count) => new Vector2(getPosition.Invoke().X + (UIComponent.Gap * 2.5f * (count == 0 ? -1f : 1f)), UIManager.Size.Y - UIComponent.Gap);
            Func<int, Vector2> getArmourPosition = (int count) => getPosition.Invoke() - ((new Vector2(InventoryWidth, InventoryHeight) - Vector2.One) * UIComponent.Gap * 0.5f) + new Vector2(0f, UIComponent.Gap * count);

            LoadSlotGroup((int)PlayerCharacter.InventoryType.Wield, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_WIELDICON.Asset, () => Vector2.Zero, () => getWieldAlpha.Invoke(0));
            LoadSlotGroup((int)PlayerCharacter.InventoryType.Hotbar, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_HOTBARICON.Asset, () => new Vector2(getPosition.Invoke().X - (2f * UIComponent.Gap * 0.5f), UIManager.Size.Y - UIComponent.Gap), () => getWieldAlpha.Invoke(1));
            LoadSlotGroup((int)PlayerCharacter.InventoryType.ArmourHead, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.Asset, () => getArmourPosition.Invoke(0), () => Alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryType.ArmourChest, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.Asset, () => getArmourPosition.Invoke(1), () => Alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryType.ArmourLegs, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.Asset, () => getArmourPosition.Invoke(2), () => Alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryType.ArmourFeet, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_ARMOURICON.Asset, () => getArmourPosition.Invoke(3), () => Alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryType.Crafting, Main.TextureLibrary.UI_BUTTONS_ICONS_INVENTORY_CRAFTINGICON.Asset, () => getPosition.Invoke() - ((new Vector2(InventoryWidth, InventoryHeight) - Vector2.One) * UIComponent.Gap * 0.5f) + new Vector2(0f, (InventoryHeight - 1f) * UIComponent.Gap), () => Alpha);
            LoadSlotGroup((int)PlayerCharacter.InventoryType.Other, null, () => getPosition.Invoke() - ((new Vector2(InventoryWidth, InventoryHeight) - Vector2.One) * UIComponent.Gap * 0.5f) + new Vector2(UIComponent.Gap, 0f), () => Alpha);

            slots[(int)PlayerCharacter.InventoryType.Wield][0, 0].getPosition = () => getWieldPosition.Invoke(0);
            slots[(int)PlayerCharacter.InventoryType.Wield][1, 0].getPosition = () => getWieldPosition.Invoke(1);

            IconButton produceButton = (IconButton)AddComponent<IconButton>();

            produceButton.menuElement = this;
            produceButton.icon = Main.TextureLibrary.UI_BUTTONS_ICONS_OTHER_PRODUCEICON.Asset;

            produceButton.getAlpha = () => Alpha;
            produceButton.getActive = () => true;

            produceButton.getPosition = () => getPosition.Invoke() - ((new Vector2(InventoryWidth, InventoryHeight) - Vector2.One) * UIComponent.Gap * 0.5f) + ((new Vector2(InventoryWidth, InventoryHeight) - Vector2.One) * UIComponent.Gap);

            produceButton.selectedInteractAction = delegate ()
            {
                List<Item> ingredients = new List<Item>();

                for (int y = 0; y < Main.World.player.InventoryCrafting.contents.GetLength(1); y++)
                {
                    for (int x = 0; x < Main.World.player.InventoryCrafting.contents.GetLength(0); x++)
                    {
                        ingredients.Add(Main.World.player.InventoryCrafting.contents[x, y].item);
                    }
                }

                foreach (Item item in Item.Items)
                {
                    bool match = true;

                    if (item.Ingredients.Count <= 0)
                    {
                        continue;
                    }

                    foreach (Item ingredient in item.Ingredients)
                    {
                        if (!ingredients.Contains(ingredient))
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        foreach (Item ingredient in item.Ingredients)
                        {
                            Main.World.player.Inventory.RemoveItemAt((int)PlayerCharacter.InventoryType.Crafting, ingredient, 1);
                        }

                        Main.World.player.Inventory.AddItem(item, 1);
                    }
                }
            };
        }
    }
}