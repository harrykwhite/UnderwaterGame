using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Characters;
using UnderwaterGame.Input;
using UnderwaterGame.Utilities;

namespace UnderwaterGame
{
    public static class Camera
    {
        private static float look = 40f;
        private static float speed = 0.125f;

        private static Vector2 shake;
        private static float shakeTime;
        private static float shakeSpeed = (MathHelper.Pi * 2f) / 9f;

        public static Vector2 position;
        public static Vector2 positionTo;

        public static int Width => Main.BufferWidth / (int)Scale;
        public static int Height => Main.BufferHeight / (int)Scale;
        public static float Scale => 4f;

        public static Shape Shape => new Shape(Shape.Fill.Rectangle, Width, Height)
        {
            position = position - (new Vector2(Width, Height) / 2f)
        };

        public static void Init()
        {
            position = new Vector2(Width / 2f, Height / 2f);
        }

        public static void Update()
        {
            int mapWidth = Width;
            int mapHeight = Height;

            if (Main.World != null)
            {
                mapWidth = Main.World.RealWidth;
                mapHeight = Main.World.RealHeight;
            }

            if (!Main.IsPaused)
            {
                if (EntityManager.GetEntityExists<PlayerCharacter>())
                {
                    positionTo = Main.World.player.position + (Vector2.Normalize(InputManager.GetMousePositionWorld() - Main.World.player.position) * look);
                }
            }

            positionTo.X = MathUtilities.Clamp(positionTo.X, Width / 2f, mapWidth - (Width / 2f));
            positionTo.Y = MathUtilities.Clamp(positionTo.Y, Height / 2f, mapHeight - (Height / 2f));

            position.X += (float)(positionTo.X - position.X) * speed;
            position.Y += (float)(positionTo.Y - position.Y) * speed;

            position += shake * (float)Math.Sin(shakeTime);
            shake *= 0.9f;

            shakeTime += shakeSpeed;
            shakeTime %= MathHelper.Pi * 2f;

            position.X = MathUtilities.Clamp(position.X, Width / 2f, mapWidth - (Width / 2f));
            position.Y = MathUtilities.Clamp(position.Y, Height / 2f, mapHeight - (Height / 2f));
        }

        public static void Shake(float amount, float direction)
        {
            shake += amount * new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
        }

        public static void Shake(float amount, Vector2 at)
        {
            float direction = MathUtilities.PointDirection(position, at);
            shake += amount * new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
        }
    }
}