namespace UnderwaterGame.Worlds
{
    using System;

    [Serializable]
    public class WorldEnvironmental
    {
        public byte id;

        public int x;

        public int y;

        public WorldEnvironmental(byte id, int x, int y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }
}