using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Characters.Enemies;
using UnderwaterGame.Entities.Particles;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Worlds.Areas
{
    public abstract class WorldArea
    {
        public class SpawnData
        {
            public Type SpawnType { get; private set; }
            public float SpawnChance { get; private set; }

            public SpawnData(Type type, float chance)
            {
                SpawnType = type;
                SpawnChance = chance;
            }
        }

        public int SpawnMax { get; protected set; }

        public int SpawnTime { get; protected set; }
        public int SpawnTimeMax { get; protected set; }

        public List<SpawnData> SpawnPool { get; protected set; } = new List<SpawnData>();

        protected WorldArea()
        {
            Init();
            SpawnTime = SpawnTimeMax;
        }

        public void UpdateSpawn()
        {
            if (SpawnTime > 0)
            {
                SpawnTime--;
            }
            else
            {
                SpawnData spawnData = GetSpawnData();

                if (spawnData != null)
                {
                    if (EntityManager.GetEntityCount<EnemyCharacter>() < SpawnMax)
                    {
                        EnemyCharacter enemy = (EnemyCharacter)EntityManager.AddEntity(spawnData.SpawnType, Vector2.Zero);
                        enemy.position = GetSpawnPosition(enemy);

                        int smokeCount = 5;

                        for (int i = 0; i < smokeCount; i++)
                        {
                            Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(enemy.position);
                            smoke.direction = ((MathHelper.Pi * 2f) / smokeCount) * i;
                        }
                    }
                }

                SpawnTime = SpawnTimeMax;
            }
        }

        public void UpdateSpawnPool(Type type, float chance)
        {
            SpawnData spawnData = new SpawnData(type, chance);
            SpawnPool.Add(spawnData);
        }

        protected abstract void Init();

        protected virtual Vector2 GetSpawnPosition(EnemyCharacter enemy)
        {
            Shape spawnShape = Camera.Shape;
            Vector2 spawnPosition = Vector2.Zero;

            do
            {
                spawnPosition = new Vector2(RandomUtilities.Range(spawnShape.position.X, spawnShape.position.X + spawnShape.width), RandomUtilities.Range(spawnShape.position.Y, spawnShape.position.Y + spawnShape.height));
            } while (enemy.TileCollision(spawnPosition, World.TilemapType.Solids) || !enemy.TileCollision(spawnPosition, World.TilemapType.Liquids));

            return spawnPosition;
        }

        protected virtual SpawnData GetSpawnData()
        {
            foreach (SpawnData data in SpawnPool)
            {
                if (RandomUtilities.Chance(data.SpawnChance))
                {
                    return data;
                }
            }

            return null;
        }
    }
}