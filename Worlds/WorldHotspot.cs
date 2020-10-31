namespace UnderwaterGame.Worlds
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters.Enemies;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Utilities;

    public class WorldHotspot
    {
        public Shape shape;

        public int spawnTime;

        public int spawnTimeMax;

        public List<Type> spawnTypes = new List<Type>();

        public WorldHotspot(int size, int spawnTimeMax, List<Type> spawnTypes)
        {
            shape = new Shape(Shape.Fill.Circle, size, size);
            this.spawnTimeMax = spawnTimeMax;
            this.spawnTypes = spawnTypes;
        }

        public void Init()
        {
        }

        public void Update()
        {
            if(spawnTime < spawnTimeMax)
            {
                spawnTime++;
            }
            else
            {
                EnemyCharacter enemy = (EnemyCharacter)EntityManager.AddEntity(spawnTypes[Main.random.Next(spawnTypes.Count)], Vector2.Zero);
                enemy.position = GetSpawnPosition(enemy);
                int smokeCount = 5;
                for(int i = 0; i < smokeCount; i++)
                {
                    Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(enemy.position);
                    smoke.direction = ((MathHelper.Pi * 2f) / smokeCount) * i;
                }
                spawnTime = 0;
            }
        }

        private Vector2 GetSpawnPosition(EnemyCharacter enemy)
        {
            Shape shape = Camera.GetShape();
            Vector2 position = Vector2.Zero;
            do
            {
                position = new Vector2(RandomUtilities.Range(shape.position.X, shape.position.X + shape.width), RandomUtilities.Range(shape.position.Y, shape.position.Y + shape.height));
            } while(enemy.TileCollision(position, World.Tilemap.Solids) || !enemy.TileCollision(position, World.Tilemap.Liquids));
            return position;
        }
    }
}