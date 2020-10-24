using Microsoft.Xna.Framework;
using System;

namespace UnderwaterGame.Utilities
{
    public static class GeneralUtilities
    {
        public static T[] AsOneDimensional<T>(T[,] oldArray)
        {
            int width = oldArray.GetLength(0);
            int height = oldArray.GetLength(1);

            T[] newArray = new T[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newArray[(y * width) + x] = oldArray[x, y];
                }
            }

            return newArray;
        }

        public static T[,] AsTwoDimensional<T>(T[] oldArray, int width, int height)
        {
            T[,] newArray = new T[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newArray[x, y] = oldArray[(y * width) + x];
                }
            }

            return newArray;
        }

        public static T[,] ResizeTwoDimensional<T>(T[,] oldArray, int width, int height)
        {
            T[,] array = new T[width, height];

            int minWidth = Math.Min(width, oldArray.GetLength(0));
            int minHeight = Math.Min(height, oldArray.GetLength(1));

            for (int y = 0; y < minHeight; y++)
            {
                for (int x = 0; x < minWidth; x++)
                {
                    array[x, y] = oldArray[x, y];
                }
            }

            return array;
        }

        public static Color MergeColor(Color a, Color b, float amount)
        {
            byte rMin = Math.Min(a.R, b.R);
            byte gMin = Math.Min(a.G, b.G);
            byte bMin = Math.Min(a.B, b.B);
            byte aMin = Math.Min(a.A, b.A);

            byte rMax = Math.Max(a.R, b.R);
            byte gMax = Math.Max(a.G, b.G);
            byte bMax = Math.Max(a.B, b.B);
            byte aMax = Math.Max(a.A, b.A);

            return new Color(
                (byte)MathUtilities.Merge(rMin, rMax, amount),
                (byte)MathUtilities.Merge(gMin, gMax, amount),
                (byte)MathUtilities.Merge(bMin, bMax, amount),
                (byte)MathUtilities.Merge(aMin, aMax, amount));
        }
    }
}