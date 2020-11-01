namespace UnderwaterGame.Worlds
{
    using System;

    [Serializable]
    public class WorldHotspot
    {
        public int x;

        public int y;

        public int width;

        public int height;

        public string[] types;

        public WorldHotspot(int x, int y, int width, int height, string[] types)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.types = types;
        }
    }
}