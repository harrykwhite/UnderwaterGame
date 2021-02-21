namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using System;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public static class Camera
    {
        private static float look = 40f;

        private static float speed = 0.125f;

        private static Vector2 shake;

        private static float shakeTime;

        private static float shakeSpeed = MathHelper.Pi / 6f;

        public static Vector2 position;

        public static Vector2 positionTo;

        public static int scale = 3;

        public static void Init()
        {
            positionTo = new Vector2(GetWidth(), GetHeight()) / 2f;
            position = positionTo;
            shake = Vector2.Zero;
        }

        public static void Update()
        {
            if(UiManager.menuCurrent == null)
            {
                if(World.player?.GetExists() ?? false)
                {
                    positionTo = World.player.position + (Vector2.Normalize(Control.GetMousePositionWorld() - World.player.position) * look);
                }
                positionTo.X = MathUtilities.Clamp(positionTo.X, (float)Math.Ceiling(GetWidth() / 2f), (World.width * Tile.size) - (float)Math.Ceiling(GetWidth() / 2f));
                positionTo.Y = MathUtilities.Clamp(positionTo.Y, (float)Math.Ceiling(GetHeight() / 2f), (World.height * Tile.size) - (float)Math.Ceiling(GetHeight() / 2f));
                position.X += (float)(positionTo.X - position.X) * speed;
                position.Y += (float)(positionTo.Y - position.Y) * speed;
                shake *= 0.9f;
                shakeTime += shakeSpeed;
                shakeTime %= MathHelper.Pi * 2f;
                position += shake * (float)Math.Sin(shakeTime);
                position.X = MathUtilities.Clamp(position.X, (float)Math.Ceiling(GetWidth() / 2f), (World.width * Tile.size) - (float)Math.Ceiling(GetWidth() / 2f));
                position.Y = MathUtilities.Clamp(position.Y, (float)Math.Ceiling(GetHeight() / 2f), (World.height * Tile.size) - (float)Math.Ceiling(GetHeight() / 2f));
            }
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

        public static int GetWidth()
        {
            return Main.GetBufferWidth() / scale;
        }

        public static int GetHeight()
        {
            return Main.GetBufferHeight() / scale;
        }
    }
}