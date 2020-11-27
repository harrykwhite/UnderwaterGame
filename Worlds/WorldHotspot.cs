namespace UnderwaterGame.Worlds
{
    using System;

    [Serializable]
    public class WorldHotspot
    {
        public float x;

        public float y;

        public Hotspot.Spawn[] spawns;

        public int spawnMax;

        public float spawnTimeMax;

        public int count;

        public WorldHotspot(float x, float y, Hotspot.Spawn[] spawns, int spawnMax, float spawnTimeMax, int count)
        {
            this.x = x;
            this.y = y;
            this.spawns = spawns;
            this.spawnMax = spawnMax;
            this.spawnTimeMax = spawnTimeMax;
            this.count = count;
        }
    }
}