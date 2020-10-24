using System;
using UnderwaterGame.UI;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.Worlds;

namespace UnderwaterGame
{
    [Serializable]
    public class Save
    {
        public WorldTile[][,] tilemaps;
        public WorldEnvironmental[] environmentals;

        public byte gameCursorDragItemID;
        public int gameCursorDragQuantity;

        public Save()
        {
            tilemaps = new WorldTile[Main.World.Tilemaps.Length][,];

            for (byte m = 0; m < Main.World.Tilemaps.Length; m++)
            {
                tilemaps[m] = new WorldTile[Main.World.Width, Main.World.Height];

                for (int y = 0; y < Main.World.Height; y++)
                {
                    for (int x = 0; x < Main.World.Width; x++)
                    {
                        tilemaps[m][x, y] = Main.World.Tilemaps[m][x, y];
                    }
                }
            }

            environmentals = Main.World.Environmentals.ToArray();

            GameCursorElement gameCursor = (GameCursorElement)UIManager.GetElement<GameCursorElement>();
            gameCursorDragItemID = gameCursor.dragItem?.id ?? 0;
            gameCursorDragQuantity = gameCursor.dragQuantity;
        }
    }
}