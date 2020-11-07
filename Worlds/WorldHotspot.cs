namespace UnderwaterGame.Worlds
{
    using System;

    [Serializable]
    public class WorldHotspot
    {
        [Serializable]
        public class Spawn
        {
            public string type;
            public float chance;

            public Spawn(string type, float chance)
            {
                this.type = type;
                this.chance = chance;
            }
        }

        public int x;

        public int y;

        public int width;

        public int height;

        public Spawn[] spawns;

        public WorldHotspot(int x, int y, int width, int height, Spawn[] spawns)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.spawns = spawns;
        }
    }
}