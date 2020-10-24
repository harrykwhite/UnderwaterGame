using System.Collections.Generic;
using UnderwaterGame.Tiles.Liquids;
using UnderwaterGame.Tiles.Solids;
using UnderwaterGame.Tiles.Walls;

namespace UnderwaterGame.Tiles
{
    public abstract partial class Tile
    {
        public static List<Tile> Tiles { get; private set; } = new List<Tile>();

        public static Stone Stone { get; private set; }
        public static Water Water { get; private set; }
        public static Sand Sand { get; private set; }
        public static SandWall SandWall { get; private set; }
        public static StoneWall StoneWall { get; private set; }
        public static Brick Brick { get; private set; }
        public static BrickWall BrickWall { get; private set; }

        public static void LoadAll()
        {
            Stone = Load<Stone>(1);
            Water = Load<Water>(2);
            Sand = Load<Sand>(3);
            SandWall = Load<SandWall>(4);
            StoneWall = Load<StoneWall>(5);
            Brick = Load<Brick>(6);
            BrickWall = Load<BrickWall>(7);
        }
    }
}