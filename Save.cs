namespace UnderwaterGame
{
    using System;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Worlds;

    [Serializable]
    public class Save
    {
        public WorldTile[][,] tilemaps;

        public WorldEnvironmental[] environmentals;

        public byte gameCursorDragItemId;

        public int gameCursorDragQuantity;

        public Save()
        {
            tilemaps = new WorldTile[World.tilemaps.Length][,];
            for(byte m = 0; m < World.tilemaps.Length; m++)
            {
                tilemaps[m] = new WorldTile[World.width, World.height];
                for(int y = 0; y < World.height; y++)
                {
                    for(int x = 0; x < World.width; x++)
                    {
                        tilemaps[m][x, y] = World.tilemaps[m][x, y];
                    }
                }
            }
            environmentals = World.environmentals.ToArray();
            GameCursorElement gameCursor = (GameCursorElement)UiManager.GetElement<GameCursorElement>();
            gameCursorDragItemId = gameCursor.dragItem?.id ?? 0;
            gameCursorDragQuantity = gameCursor.dragQuantity;
        }
    }
}