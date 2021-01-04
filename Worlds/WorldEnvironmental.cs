namespace UnderwaterGame.Worlds
{
    public class WorldEnvironmental
    {
        public byte id;

        public byte texture;

        public int x;

        public int y;

        public WorldEnvironmental(byte id, byte texture, int x, int y)
        {
            this.id = id;
            this.texture = texture;
            this.x = x;
            this.y = y;
        }
    }
}