using System;
using UnderwaterGame.Tiles;

namespace UnderwaterGame.Worlds
{
    [Serializable]
    public class WorldTile
    {
        public byte id;
        public byte texture;
        public byte lighting;

        public Tile TileType => Tile.GetTileByID(id);
    }
}