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

        public float x;

        public float y;

        public Spawn[] spawns;

        public int count;

        public WorldHotspot(float x, float y, Spawn[] spawns, int count)
        {
            this.x = x;
            this.y = y;
            this.spawns = spawns;
            this.count = count;
        }
    }
}