using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.Items;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.UI.UIElements.Menus;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.UI.UIComponents.Buttons
{
    public class SlotButton : ButtonComponent
    {
        public Func<Inventory> getInventory;

        public Texture2D texture;
        public Texture2D icon;

        public Shape shape;

        public int slotX;
        public int slotY;
        public int slotGroup;

        public override void Draw()
        {
            Inventory inventory = getInventory.Invoke();

            if (inventory == null)
            {
                return;
            }

            float slotWidth = texture.Width * scale.X;
            float slotHeight = texture.Height * scale.Y;

            Inventory.InventorySlot slot = inventory.Groups[slotGroup].contents[slotX, slotY];

            Main.SpriteBatch.Draw(texture, Position, null, Color.White * Alpha, 0f, new Vector2(texture.Width, texture.Height) / 2f, scale * 1f, SpriteEffects.None, 1f);

            if (slot.item != null)
            {
                Main.SpriteBatch.Draw(slot.item.Sprite.TexturesOutlined[0], Position, null, Color.White * Alpha, 0f, new Vector2(slot.item.Sprite.Bound.X + (slot.item.Sprite.Bound.Width / 2f), slot.item.Sprite.Bound.Y + (slot.item.Sprite.Bound.Height / 2f)) + Vector2.One, 1f, SpriteEffects.None, 1f);

                if (slot.item.Stack)
                {
                    DrawUtilities.DrawString(Main.FontLibrary.ARIALSMALL.Asset, new DrawUtilities.Text(slot.quantity.ToString()), Position + new Vector2(12f, 14f), Color.White * Alpha, DrawUtilities.HAlign.Right, DrawUtilities.VAlign.Bottom);
                }
            }
            else
            {
                if (icon != null)
                {
                    Main.SpriteBatch.Draw(icon, Position, null, Color.White * Alpha * 0.5f, 0f, new Vector2(icon.Width, icon.Height) / 2f, scale, SpriteEffects.None, 1f);
                }
            }
        }

        public override void Init()
        {
            texture = Main.TextureLibrary.UI_BUTTONS_OTHER_BUTTON.Asset;

            shape = new Shape(Shape.Fill.Circle, texture.Width, texture.Height);

            selectedAction = delegate ()
            {
                if (!touching)
                {
                    return;
                }

                Inventory inventory = getInventory.Invoke();
                GameCursorElement gameCursor = (GameCursorElement)UIManager.GetElement<GameCursorElement>();

                Inventory.InventorySlot slot = inventory.Groups[slotGroup].contents[slotX, slotY];

                if (menuElement is PlayerMenu)
                {
                    ((PlayerMenu)menuElement).selectedSlotX = slotX;
                    ((PlayerMenu)menuElement).selectedSlotY = slotY;
                    ((PlayerMenu)menuElement).selectedGroup = slotGroup;
                }

                if (slot.item != null)
                {
                    gameCursor.text = slot.item.Name;
                }
            };

            selectedInteractAction = delegate ()
            {
                Inventory inventory = getInventory.Invoke();

                Inventory.InventorySlot slot = inventory.Groups[slotGroup].contents[slotX, slotY];
                GameCursorElement gameCursor = (GameCursorElement)UIManager.GetElement<GameCursorElement>();

                Item slotItemPrevious = slot.item;
                int slotQuantityPrevious = slot.quantity;

                if (gameCursor.dragItem != null)
                {
                    if (inventory.Groups[slotGroup].predicate.Invoke(gameCursor.dragItem))
                    {
                        inventory.AddItemAt(slotX, slotY, slotGroup, gameCursor.dragItem, gameCursor.dragQuantity);

                        if (slot.item == gameCursor.dragItem && slot.item.Stack)
                        {
                            gameCursor.dragItem = null;
                            gameCursor.dragQuantity = 0;
                        }
                        else
                        {
                            slot.item = gameCursor.dragItem;
                            slot.quantity = gameCursor.dragQuantity;

                            gameCursor.dragItem = slotItemPrevious;
                            gameCursor.dragQuantity = slotQuantityPrevious;
                        }
                    }
                }
                else
                {
                    slot.item = null;
                    slot.quantity = 0;

                    gameCursor.dragItem = slotItemPrevious;
                    gameCursor.dragQuantity = slotQuantityPrevious;
                }
            };
        }

        public override void Update()
        {
            UpdateButton();
        }

        protected override bool IsTouching()
        {
            if (!CanTouch)
            {
                return false;
            }

            Shape shape = this.shape;
            shape.position = Position - new Vector2((int)Math.Ceiling(texture.Width / 2f), (int)Math.Ceiling(texture.Height / 2f));

            return shape.Intersects(((GameCursorElement)UIManager.GetElement<GameCursorElement>()).GetShape());
        }
    }
}