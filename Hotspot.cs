namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Particles;

    public class Hotspot
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

        public Vector2 position;

        public Spawn[] spawns;

        public int count;

        public float countScale = 1f;

        public float countScaleMax = 1.5f;

        public Hotspot(Vector2 position, Spawn[] spawns, int count)
        {
            this.position = position;
            this.spawns = spawns;
            this.count = count;
        }

        public void Update()
        {
            Vector2 particlePosition;
            do
            {
                particlePosition = position + new Vector2(Main.random.Next(-Main.textureLibrary.OTHER_HOTSPOTOUTER.asset.Width / 2, Main.textureLibrary.OTHER_HOTSPOTOUTER.asset.Width / 2), Main.random.Next(-Main.textureLibrary.OTHER_HOTSPOTOUTER.asset.Height / 2, Main.textureLibrary.OTHER_HOTSPOTOUTER.asset.Height / 2));
            } while(Vector2.Distance(particlePosition, position) > Main.textureLibrary.OTHER_HOTSPOTOUTER.asset.Width / 2f);
            EntityManager.AddEntity<HotspotParticle>(particlePosition);
            countScale += (1f - countScale) * 0.2f;
        }
    }
}