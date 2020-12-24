namespace UnderwaterGame.Worlds
{
    using System;

    [Serializable]
    public class WorldItemDrop
    {
        public byte id;
        
        public int quantity;
        
        public float x;

        public float y;

        public WorldItemDrop(byte id, int quantity, float x, float y)
        {
            this.id = id;
            this.quantity = quantity;
            this.x = x;
            this.y = y;
        }
    }
}