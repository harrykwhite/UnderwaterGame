namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters.Enemies;

    public class Hotspot
    {
        public Vector2 position;

        public Spawn[] spawns;

        public int count;

        public float alpha;

        private float alphaAcc = 0.1f;

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
        }
    }
}