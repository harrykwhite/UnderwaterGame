using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UnderwaterGame.Entities
{
    public static class EntityManager
    {
        public static List<Entity> Entities { get; private set; } = new List<Entity>();

        public static void Update()
        {
            if (Main.IsPaused)
            {
                return;
            }

            UpdateEntities();
        }

        public static void Draw()
        {
            DrawEntities();
        }

        public static void UpdateEntities()
        {
            Entity[] tempEntities = Entities.ToArray();

            foreach (Entity entity in tempEntities)
            {
                entity.BeginUpdate();
            }

            foreach (Entity entity in tempEntities)
            {
                entity.Update();
            }

            foreach (Entity entity in tempEntities)
            {
                entity.EndUpdate();
                entity.life++;
            }
        }

        public static void DrawEntities()
        {
            Entity[] tempEntities = Entities.ToArray();

            foreach (Entity entity in tempEntities)
            {
                entity.BeginDraw();
            }

            foreach (Entity entity in tempEntities)
            {
                entity.Draw();
            }

            foreach (Entity entity in tempEntities)
            {
                entity.EndDraw();
            }
        }

        public static Entity AddEntity<T>(Vector2 position, bool skipInit = false) where T : Entity
        {
            Entity newEntity = (Entity)Activator.CreateInstance<T>();

            if (!skipInit)
            {
                newEntity.Init();
            }

            newEntity.position = position;
            Entities.Add(newEntity);

            return newEntity;
        }

        public static Entity AddEntity(Type type, Vector2 position, bool skipInit = false)
        {
            Entity newEntity = (Entity)Activator.CreateInstance(type);

            if (!skipInit)
            {
                newEntity.Init();
            }

            newEntity.position = position;
            Entities.Add(newEntity);

            return newEntity;
        }

        public static bool GetEntityExists<T>() where T : Entity
        {
            return Entities.Find((Entity entity) => entity is T) != null;
        }

        public static bool GetEntityExists(Type type)
        {
            return Entities.Find((Entity entity) => entity.GetType() == type) != null;
        }

        public static int GetEntityCount<T>() where T : Entity
        {
            return Entities.FindAll((Entity entity) => entity is T).Count;
        }

        public static int GetEntityCount(Type type)
        {
            return Entities.FindAll((Entity entity) => entity.GetType() == type).Count;
        }

        public static Entity GetEntityNearest(Vector2 position, float? maxDistance = null, Predicate<Entity> predicate = null)
        {
            Entity nearestEntity = null;
            float? nearestDistance = null;

            List<Entity> entities = predicate != null ? Entities.FindAll(predicate) : Entities;

            foreach (Entity entity in entities)
            {
                float distance = Vector2.Distance(position, entity.position);

                if (maxDistance != null)
                {
                    if (distance > maxDistance)
                    {
                        continue;
                    }
                }

                if (nearestDistance == null || distance < nearestDistance)
                {
                    nearestEntity = entity;
                    nearestDistance = distance;
                }
            }

            return nearestEntity;
        }
    }
}