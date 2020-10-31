namespace UnderwaterGame
{
    using System;
    using UnderwaterGame.Items;

    public class Inventory
    {
        public class InventorySlot
        {
            public Item item;

            public int quantity;

            public bool AcceptItem(Item item)
            {
                return this.item == item && (this.item?.stack ?? false);
            }
        }

        public class InventoryGroup
        {
            public InventorySlot[,] contents;

            public Predicate<Item> predicate = (Item item) => true;

            public bool auto;

            public InventoryGroup(int width, int height, bool auto)
            {
                contents = new InventorySlot[width, height];
                this.auto = auto;
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        contents[x, y] = new InventorySlot();
                    }
                }
            }

            public bool AcceptItem(Item item)
            {
                if(!predicate(item))
                {
                    return false;
                }
                for(int y = 0; y < contents.GetLength(1); y++)
                {
                    for(int x = 0; x < contents.GetLength(0); x++)
                    {
                        InventorySlot slot = contents[x, y];
                        if(slot.AcceptItem(item))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public InventoryGroup[] groups;

        public Inventory(int groupCount)
        {
            groups = new InventoryGroup[groupCount];
        }

        public bool AddItem(Item item, int quantity)
        {
            if(item.stack)
            {
                for(int g = 0; g < groups.Length; g++)
                {
                    if(!groups[g].auto || !groups[g].predicate(item))
                    {
                        continue;
                    }
                    for(int y = 0; y < groups[g].contents.GetLength(1); y++)
                    {
                        for(int x = 0; x < groups[g].contents.GetLength(0); x++)
                        {
                            InventorySlot slot = groups[g].contents[x, y];
                            if(slot.item == item)
                            {
                                slot.quantity += quantity;
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                quantity = 1;
            }
            for(int g = 0; g < groups.Length; g++)
            {
                if(!groups[g].auto || !groups[g].predicate(item))
                {
                    continue;
                }
                for(int y = 0; y < groups[g].contents.GetLength(1); y++)
                {
                    for(int x = 0; x < groups[g].contents.GetLength(0); x++)
                    {
                        InventorySlot slot = groups[g].contents[x, y];
                        if(slot.item == null)
                        {
                            slot.item = item;
                            slot.quantity = quantity;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool AddItemAt(int group, Item item, int quantity)
        {
            if(!groups[group].predicate(item))
            {
                return false;
            }
            if(item.stack)
            {
                for(int y = 0; y < groups[group].contents.GetLength(1); y++)
                {
                    for(int x = 0; x < groups[group].contents.GetLength(0); x++)
                    {
                        InventorySlot slot = groups[group].contents[x, y];
                        if(slot.item == item)
                        {
                            slot.quantity += quantity;
                            return true;
                        }
                    }
                }
            }
            else
            {
                quantity = 1;
            }
            for(int y = 0; y < groups[group].contents.GetLength(1); y++)
            {
                for(int x = 0; x < groups[group].contents.GetLength(0); x++)
                {
                    InventorySlot slot = groups[group].contents[x, y];
                    if(slot.item == null)
                    {
                        slot.item = item;
                        slot.quantity = quantity;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AddItemAt(int x, int y, int group, Item item, int quantity)
        {
            if(!item.stack)
            {
                quantity = 1;
            }
            InventorySlot slot = groups[group].contents[x, y];
            if(slot.AcceptItem(item))
            {
                slot.item = item;
                slot.quantity += quantity;
                return true;
            }
            return false;
        }

        public int RemoveItem(Item item, int quantity)
        {
            for(int g = 0; g < groups.Length; g++)
            {
                if(!groups[g].auto || !groups[g].predicate(item))
                {
                    continue;
                }
                for(int y = 0; y < groups[g].contents.GetLength(1); y++)
                {
                    for(int x = 0; x < groups[g].contents.GetLength(0); x++)
                    {
                        InventorySlot slot = groups[g].contents[x, y];
                        if(slot.item == item)
                        {
                            slot.quantity -= quantity;
                            quantity = Math.Abs(Math.Min(slot.quantity, 0));
                            slot.quantity = Math.Max(slot.quantity, 0);
                            if(slot.quantity == 0)
                            {
                                slot.item = null;
                            }
                            if(quantity <= 0)
                            {
                                return quantity;
                            }
                        }
                    }
                }
            }
            return quantity;
        }

        public int RemoveItemAt(int group, Item item, int quantity)
        {
            if(!groups[group].predicate(item))
            {
                return quantity;
            }
            for(int y = 0; y < groups[group].contents.GetLength(1); y++)
            {
                for(int x = 0; x < groups[group].contents.GetLength(0); x++)
                {
                    InventorySlot slot = groups[group].contents[x, y];
                    if(slot.item == item)
                    {
                        slot.quantity -= quantity;
                        quantity = Math.Abs(Math.Min(slot.quantity, 0));
                        slot.quantity = Math.Max(slot.quantity, 0);
                        if(slot.quantity == 0)
                        {
                            slot.item = null;
                        }
                        if(quantity <= 0)
                        {
                            return quantity;
                        }
                    }
                }
            }
            return quantity;
        }

        public int RemoveItemAt(int x, int y, int group, Item item, int quantity)
        {
            InventorySlot slot = groups[group].contents[x, y];
            if(slot.item == item)
            {
                slot.quantity -= quantity;
                quantity = Math.Abs(Math.Min(slot.quantity, 0));
                slot.quantity = Math.Max(slot.quantity, 0);
                if(slot.quantity == 0)
                {
                    slot.item = null;
                }
            }
            return quantity;
        }
    }
}