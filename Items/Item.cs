namespace UnderwaterGame.Items
{
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Sprites;

    public abstract partial class Item
    {
        public byte id;

        public Sprite sprite;

        public string name;

        public string description;

        public bool stack;

        public int useTime;

        public float useOffset;

        public float useStrength;

        public bool useAngleUpdate;

        public bool useHide;

        public bool usePress;

        public Item()
        {
            items.Add(this);
            Init();
        }

        private static T Load<T>(byte id) where T : Item
        {
            T item = Activator.CreateInstance<T>();
            item.id = id;
            return item;
        }

        public static Item GetItemById(byte id)
        {
            return items.Find((Item item) => item.id == id);
        }

        protected abstract void Init();

        public virtual void OnUse()
        {
        }

        public virtual void WhileUse()
        {
        }

        public virtual void EndUse()
        {
        }

        public virtual bool CanUse()
        {
            return true;
        }
    }
}