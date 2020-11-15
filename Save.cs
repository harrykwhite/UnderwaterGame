namespace UnderwaterGame
{
    using System;
    using UnderwaterGame.Worlds;

    [Serializable]
    public class Save
    {
        public WorldTile[][,] tilemaps;

        public WorldEnvironmental[] environmentals;

        public WorldHotspot[] hotspots;

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
            hotspots = new WorldHotspot[World.hotspots.Count];
            for(int i = 0; i < hotspots.Length; i++)
            {
                hotspots[i] = new WorldHotspot(World.hotspots[i].position.X, World.hotspots[i].position.Y, World.hotspots[i].spawns, World.hotspots[i].count);
            }
        }
    }
}