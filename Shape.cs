﻿using Microsoft.Xna.Framework;
using System;

namespace UnderwaterGame
{
    public class Shape
    {
        public enum Fill
        {
            Rectangle,
            Circle,
            TopLeftSlope,
            TopRightSlope,
            BottomLeftSlope,
            BottomRightSlope
        }

        public Vector2 position;
        public Fill fill;

        public int width;
        public int height;

        public bool[,] Data { get; private set; }

        public Shape(Fill fill, int width, int height)
        {
            this.fill = fill;

            this.width = width;
            this.height = height;

            Clear();
        }

        public void Clear()
        {
            Data = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    switch (fill)
                    {
                        case Fill.Rectangle:
                            Data[x, y] = true;
                            break;

                        case Fill.Circle:
                            Data[x, y] = Vector2.Distance(new Vector2(x, y), new Vector2(width, height) / 2f) <= Math.Min(width, height) / 2f;
                            break;

                        case Fill.TopLeftSlope:
                            Data[x, y] = (width - 1 - x) <= y;
                            break;

                        case Fill.TopRightSlope:
                            Data[x, y] = x <= y;
                            break;

                        case Fill.BottomLeftSlope:
                            Data[x, y] = x >= y;
                            break;

                        case Fill.BottomRightSlope:
                            Data[x, y] = (width - 1 - x) >= y;
                            break;
                    }
                }
            }
        }

        public bool Contains(Vector2 position)
        {
            int px = (int)Math.Round(position.X - this.position.X);
            int py = (int)Math.Round(position.Y - this.position.Y);

            if (px < 0 || py < 0 || px >= width || py >= height)
            {
                return false;
            }

            return Data[px, py];
        }

        public bool Intersects(Shape shape)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int sx = (int)Math.Round(x + position.X - shape.position.X);
                    int sy = (int)Math.Round(y + position.Y - shape.position.Y);

                    if (sx < 0 || sy < 0 || sx >= shape.width || sy >= shape.height)
                    {
                        continue;
                    }

                    if (Data[x, y] && shape.Data[sx, sy])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}