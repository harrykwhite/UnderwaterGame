namespace UnderwaterGame.Tiles
{
    using System.Collections.Generic;
    using UnderwaterGame.Tiles.Liquids;
    using UnderwaterGame.Tiles.Solids;
    using UnderwaterGame.Tiles.Walls;

    public abstract partial class Tile
    {
        public static List<Tile> tiles = new List<Tile>();

        public static Stone stone;

        public static Water water;

        public static Sand sand;

        public static SandWall sandWall;

        public static StoneWall stoneWall;

        public static Brick brick;

        public static BrickWall brickWall;

        public static Cloud cloud;

        public static void LoadAll()
        {
            stone = Load<Stone>(1);
            water = Load<Water>(2);
            sand = Load<Sand>(3);
            sandWall = Load<SandWall>(4);
            stoneWall = Load<StoneWall>(5);
            brick = Load<Brick>(6);
            brickWall = Load<BrickWall>(7);
            cloud = Load<Cloud>(8);
        }
    }
}