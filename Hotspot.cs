namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters.Enemies;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

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

        public int spawnMax;

        public int spawnTime;

        public int spawnTimeMax;

        public int count;

        public Vector2 countScale = Vector2.One;

        public Vector2 countScaleMax = new Vector2(1.2f);

        public float alpha;

        private float alphaAcc = 0.01f;

        private int particleTime;

        private int particleTimeMax = 5;

        public Hotspot(Vector2 position, Spawn[] spawns, int spawnMax, int spawnTimeMax, int count)
        {
            this.position = position;
            this.spawns = spawns;
            this.spawnMax = spawnMax;
            this.spawnTimeMax = spawnTimeMax;
            this.count = count;
            alpha = count <= 0 ? 0f : 1f;
        }

        public void Update()
        {
            List<Entity> enemyCharacterEntities = EntityManager.entities.FindAll((Entity entity) => entity is EnemyCharacter enemyCharacter && (enemyCharacter.hotspot == this));
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
            else
            {
                if(World.hotspotCurrent == this)
                {
                    if(spawnTime <= 0)
                    {
                        if(enemyCharacterEntities.Count < spawnMax)
                        {
                            int trials = 100;
                            Type enemyType = null;
                            do
                            {
                                for(int i = 0; i < spawns.Length; i++)
                                {
                                    if(Main.random.Next(100) <= (spawns[i].chance * 100f))
                                    {
                                        enemyType = Type.GetType(spawns[i].type);
                                    }
                                }
                            } while(enemyType == null);
                            EnemyCharacter enemy = (EnemyCharacter)EntityManager.AddEntity(enemyType, Vector2.Zero);
                            do
                            {
                                enemy.position = position + MathUtilities.LengthDirection(RandomUtilities.Range(0f, Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f), MathHelper.ToRadians(Main.random.Next(360)));
                                trials--;
                            } while((enemy.TileCollision(enemy.position, World.Tilemap.Solids) || !enemy.TileCollision(enemy.position, World.Tilemap.Liquids)) && trials > 0);
                            if(trials > 0)
                            {
                                int smokeCount = 6;
                                for(int i = 0; i < smokeCount; i++)
                                {
                                    Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(enemy.position);
                                    smoke.direction = ((MathHelper.Pi * 2f) / smokeCount) * i;
                                }
                                enemy.hotspot = this;
                            }
                            else
                            {
                                enemy.Destroy();
                            }
                        }
                        spawnTime = spawnTimeMax;
                    }
                }
            }
            if(spawnTime > 0)
            {
                spawnTime--;
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