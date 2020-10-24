using System;
using System.Collections.Generic;
using UnderwaterGame.Entities;
using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items
{
    public abstract partial class Item
    {
        public byte id;

        public Sprite Sprite { get; protected set; }

        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public bool Stack { get; protected set; }

        public int UseTime { get; protected set; }
        public float UseOffset { get; protected set; }
        public bool UseAngleUpdate { get; protected set; }
        public bool UseHide { get; protected set; }
        public bool UsePress { get; protected set; }

        public List<Item> Ingredients { get; protected set; } = new List<Item>();

        public Item()
        {
            Items.Add(this);
            Init();
        }

        private static T Load<T>(byte id) where T : Item
        {
            T item = Activator.CreateInstance<T>();
            item.id = id;

            return item;
        }

        public static Item GetItemByID(byte id)
        {
            return Items.Find((Item item) => item.id == id);
        }

        protected abstract void Init();

        public virtual void OnUse(ItemEntity entity)
        {

        }

        public virtual void WhileUse(ItemEntity entity)
        {

        }

        public virtual void EndUse(ItemEntity entity)
        {

        }

        public virtual bool CanUse(ItemEntity entity) => true;
    }
}