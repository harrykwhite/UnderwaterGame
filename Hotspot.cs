namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters.Enemies;
    using UnderwaterGame.Entities.Particles;

    public class Hotspot
    {
        public Vector2 position;

        public Spawn[] spawns;

        public int count;

        public Vector2 countScale = Vector2.One;

        public Vector2 countScaleMax = new Vector2(1.2f);

        public float alpha;

        private float alphaAcc = 0.01f;

        private int particleTime;

        private int particleTimeMax = 5;

        public Hotspot(Vector2 position, Spawn[] spawns, int count)
        {
            this.position = position;
            this.spawns = spawns;
            this.count = count;
            alpha = count <= 0 ? 0f : 1f;
        }

        public void Update()
        {
            List<Entity> enemyCharacterEntities = EntityManager.entities.FindAll((Entity entity) => entity is EnemyCharacter enemyCharacter && enemyCharacter.hotspot == this);
            if(count <= 0)
            {
                if(alpha > 0f)
                {
                    alpha -= Math.Min(alphaAcc, alpha);
                    if(alpha == 0f)
                    {
                        foreach(Entity enemyCharacterEntity in enemyCharacterEntities)
                        {
                            EnemyCharacter enemyCharacter = (EnemyCharacter)enemyCharacterEntity;
                            enemyCharacter.hotspot = null;
                        }
                    }
                }
            }
            if(alpha > 0f)
            {
                if(particleTime < particleTimeMax)
                {
                    particleTime++;
                }
                else
                {
                    Vector2 particlePosition;
                    do
                    {
                        particlePosition = position + new Vector2(Main.random.Next(-Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2, Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2), Main.random.Next(-Main.textureLibrary.OTHER_HOTSPOT.asset.Height / 2, Main.textureLibrary.OTHER_HOTSPOT.asset.Height / 2));
                    } while(Vector2.Distance(particlePosition, position) > Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f);
                    EntityManager.AddEntity<HotspotParticle>(particlePosition);
                    particleTime = 0;
                }
            }
            countScale += (Vector2.One - countScale) * 0.1f;
        }
    }
}