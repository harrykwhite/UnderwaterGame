namespace UnderwaterGame.Worlds
{
    public class WorldEnvironmental
    {
        public byte id;

        public byte texture;

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