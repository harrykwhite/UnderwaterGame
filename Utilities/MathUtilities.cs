namespace UnderwaterGame.Utilities
{
    using Microsoft.Xna.Framework;
    using System;

    public static class MathUtilities
    {
        public static int Clamp(int value, int min, int max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        public static float Merge(float min, float max, float amount)
        {
            return ((max - min) * amount) + min;
        }

        public static float Mean(params float[] values)
        {
            int count = values.Length;
            float sum = 0f;
            for(int i = 0; i < count; i++)
            {
                sum += values[i];
            }
            return sum / count;
        }

        public static float PointDirection(Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        public static Vector2 LengthDirection(float length, float direction)
        {
            return new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction)) * length;
        }

        public static float AngleNormal(float angle)
        {
            while(angle > MathHelper.Pi)
            {
                angle -= MathHelper.Pi * 2f;
            } while(angle < -MathHelper.Pi)
            {
                angle += MathHelper.Pi * 2f;
            }
            return angle;
        }

        public static float AngleDifference(float source, float destination)
        {
            return AngleNormal(destination - source);
        }

        public static bool AngleLeftHalf(float angle)
        {
            angle = AngleNormal(angle);
            return angle < -MathHelper.Pi / 2f || angle > MathHelper.Pi / 2f;
        }

        public static bool AngleRightHalf(float angle)
        {
            angle = AngleNormal(angle);
            return angle >= -MathHelper.Pi / 2f && angle <= MathHelper.Pi / 2f;
        }
    }
}