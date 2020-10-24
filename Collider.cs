using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Entities;
using UnderwaterGame.Utilities;

namespace UnderwaterGame
{
    public class Collider
    {
        public Shape shape;
        public Entity entity;

        public Collider(Shape shape, Entity entity)
        {
            this.shape = shape;
            this.entity = entity;
        }

        public Shape GetRelative(Vector2? at = null, float? angle = null)
        {
            Shape real = new Shape(Shape.Fill.Rectangle, shape.width, shape.height)
            {
                position = (at ?? entity.position) - (new Vector2(shape.width, shape.height) / 2f)
            };

            if (shape.width != shape.height)
            {
                int width = (int)Math.Abs(MathUtilities.Merge(shape.width, shape.height, 1f - (float)Math.Abs(Math.Cos(angle ?? (entity.angle + entity.angleOffset)))));
                int height = (int)Math.Abs(MathUtilities.Merge(shape.width, shape.height, 1f - (float)Math.Abs(Math.Sin(angle ?? (entity.angle + entity.angleOffset)))));

                real.position.X += (shape.width - width) / 2f;
                real.position.Y += (shape.height - height) / 2f;

                real.width = width;
                real.height = height;

                real.Clear();
            }

            return real;
        }

        public bool IsTouching(Vector2 at, Collider other)
        {
            return GetRelative(at).Intersects(other.GetRelative());
        }

        public Entity GetTouchingEntity(Vector2 at, Predicate<Entity> predicate = null)
        {
            Shape real = GetRelative(at);

            foreach (Entity entity in EntityManager.Entities)
            {
                if (entity.Collider == this)
                {
                    continue;
                }

                if (predicate != null)
                {
                    if (!predicate.Invoke(entity))
                    {
                        continue;
                    }
                }

                if (real.Intersects(entity.Collider.GetRelative()))
                {
                    return entity;
                }
            }

            return null;
        }
    }
}