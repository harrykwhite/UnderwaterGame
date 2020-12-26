namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters.Enemies;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class Hotspot
    {
        public Vector2 position;

        public Spawn[] spawns;
        
        public static int spawnTime;

        public static int spawnTimeMax = 300;

        public static int spawnMax = 8;

        public Hotspot(Vector2 position, Spawn[] spawns)
        {
            this.position = position;
            this.spawns = spawns;
        }

        public void Update()
        {
            if(spawnTime > 0)
            {
                spawnTime--;
            }
            else
            {
                if(World.hotspotCurrent == this)
                {
                    if(EntityManager.entities.FindAll((Entity entity) => entity is EnemyCharacter enemyCharacter && (enemyCharacter.hotspot == this)).Count < spawnMax)
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
                        enemy.hotspot = this;
                        do
                        {
                            enemy.position = position + MathUtilities.LengthDirection(RandomUtilities.Range(0f, Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f), MathHelper.ToRadians(Main.random.Next(360)));
                            trials--;
                        } while((enemy.TileCollision(enemy.position, World.Tilemap.Solids) || !enemy.TileCollision(enemy.position, World.Tilemap.Liquids) || Vector2.Distance(enemy.position, World.player.position) <= 128f) && trials > 0);
                        if(trials > 0)
                        {
                            int smokeCount = 6;
                            float smokeDirectionOffset = MathHelper.ToRadians(Main.random.Next(360));
                            for(int i = 0; i < smokeCount; i++)
                            {
                                Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(enemy.position);
                                smoke.direction = (((MathHelper.Pi * 2f) / smokeCount) * i) + smokeDirectionOffset;
                            }
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
    }
}