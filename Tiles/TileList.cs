namespace UnderwaterGame.Tiles
{
    using System.Collections.Generic;

    public abstract partial class Tile
    {
        public static List<Tile> tiles = new List<Tile>();

        public static Stone stone;

        public static Sand sand;

        public static Brick brick;

        public static void LoadAll()
        {
            stone = Load<Stone>(1);
            sand = Load<Sand>(2);
            brick = Load<Brick>(3);
        }
    }
}