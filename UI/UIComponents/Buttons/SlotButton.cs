namespace UnderwaterGame.Ui.UiComponents.Buttons
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using UnderwaterGame.Items;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Ui.UiElements.Menus;
    using UnderwaterGame.Utilities;

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
            if(getInventory() == null)
            {
                return;
            }
            float slotWidth = texture.Width * scale.X;
            float slotHeight = texture.Height * scale.Y;
            Inventory.InventorySlot slot = getInventory().groups[slotGroup].contents[slotX, slotY];
            Main.spriteBatch.Draw(texture, getPosition(), null, Color.White * getAlpha(), 0f, new Vector2(texture.Width, texture.Height) / 2f, scale * 1f, SpriteEffects.None, 1f);
            if(slot.item != null)
            {
                Main.spriteBatch.Draw(slot.item.sprite.texturesOutlined[0], getPosition(), null, Color.White * getAlpha(), 0f, new Vector2(slot.item.sprite.bound.X + (slot.item.sprite.bound.Width / 2f), slot.item.sprite.bound.Y + (slot.item.sprite.bound.Height / 2f)) + Vector2.One, 1f, SpriteEffects.None, 1f);
                if(slot.item.stack)
                {
                    DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(slot.quantity.ToString()), getPosition() + new Vector2(12f, 14f), Color.White * getAlpha(), DrawUtilities.HorizontalAlign.Right, DrawUtilities.VerticalAlign.Bottom);
                }
            }
            else
            {
                if(icon != null)
                {
                    Main.spriteBatch.Draw(icon, getPosition(), null, Color.White * getAlpha() * 0.5f, 0f, new Vector2(icon.Width, icon.Height) / 2f, scale, SpriteEffects.None, 1f);
                }
            }
        }

        public override void Init()
        {
            texture = Main.textureLibrary.UI_BUTTONS_OTHER_BUTTON.asset;
            shape = new Shape(Shape.Fill.Circle, texture.Width, texture.Height);
            selectedAction = delegate ()
            {
                if(!touching)
                {
                    return;
                }
                CursorElement cursor = (CursorElement)UiManager.GetElement<CursorElement>();
                Inventory.InventorySlot slot = getInventory().groups[slotGroup].contents[slotX, slotY];
                if(menuElement is PlayerMenu menu)
                {
                    menu.selectedSlotX = slotX;
                    menu.selectedSlotY = slotY;
                    menu.selectedGroup = slotGroup;
                }
                if(slot.item != null)
                {
                    cursor.text = slot.item.name;
                }
            };
            selectedInteractAction = delegate ()
            {
                Inventory.InventorySlot slot = getInventory().groups[slotGroup].contents[slotX, slotY];
                CursorElement cursor = (CursorElement)UiManager.GetElement<CursorElement>();
                Item slotItemPrevious = slot.item;
                int slotQuantityPrevious = slot.quantity;
                if(cursor.dragItem != null)
                {
                    if(getInventory().groups[slotGroup].predicate(cursor.dragItem))
                    {
                        getInventory().AddItemAt(slotX, slotY, slotGroup, cursor.dragItem, cursor.dragQuantity);
                        if(slot.item == cursor.dragItem && slot.item.stack)
                        {
                            cursor.dragItem = null;
                            cursor.dragQuantity = 0;
                        }
                        else
                        {
                            slot.item = cursor.dragItem;
                            slot.quantity = cursor.dragQuantity;
                            cursor.dragItem = slotItemPrevious;
                            cursor.dragQuantity = slotQuantityPrevious;
                        }
                    }
                }
                else
                {
                    slot.item = null;
                    slot.quantity = 0;
                    cursor.dragItem = slotItemPrevious;
                    cursor.dragQuantity = slotQuantityPrevious;
                }
            };
        }

        public override void Update()
        {
            UpdateButton();
        }

        protected override bool IsTouching()
        {
            if(!GetCanTouch())
            {
                return false;
            }
            Shape shape = this.shape;
            shape.position = getPosition() - new Vector2((int)Math.Ceiling(texture.Width / 2f), (int)Math.Ceiling(texture.Height / 2f));
            return shape.Intersects(((CursorElement)UiManager.GetElement<CursorElement>()).GetShape());
        }
    }
}