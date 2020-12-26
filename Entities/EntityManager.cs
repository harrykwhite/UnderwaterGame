namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Ui;

    public static class EntityManager
    {
        public static List<Entity> entities;
        public static Entity[] tempEntities;

        public static void Init()
        {
            entities = new List<Entity>();
            tempEntities = null;
        }

        public static void Update()
        {
            if(UiManager.menuCurrent != null)
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
            tempEntities = entities.ToArray();
            foreach(Entity entity in tempEntities)
            {
                entity.BeginUpdate();
            }
            foreach(Entity entity in tempEntities)
            {
                entity.Update();
            }
            foreach(Entity entity in tempEntities)
            {
                entity.EndUpdate();
                entity.life++;
            }
        }

        public static void DrawEntities()
        {
            foreach(Entity entity in tempEntities)
            {
                entity.BeginDraw();
            }
            foreach(Entity entity in tempEntities)
            {
                entity.Draw();
            }
            foreach(Entity entity in tempEntities)
            {
                entity.EndDraw();
            }
        }

        public static Entity AddEntity<T>(Vector2 position, bool skipInit = false) where T : Entity
        {
            Entity newEntity = Activator.CreateInstance<T>();
            if(!skipInit)
            {
                newEntity.Init();
            }
            newEntity.position = position;
            entities.Add(newEntity);
            return newEntity;
        }

        public static Entity AddEntity(Type type, Vector2 position, bool skipInit = false)
        {
            Entity newEntity = (Entity)Activator.CreateInstance(type);
            if(!skipInit)
            {
                newEntity.Init();
            }
            newEntity.position = position;
            entities.Add(newEntity);
            return newEntity;
        }
    }
}