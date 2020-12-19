namespace UnderwaterGame.Worlds.Generation
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Tiles;

    public class SurfaceEnvironmentalGeneration : WorldGeneration
    {
        public override void Generate()
        {
            int interval = 0;
            int intervalMax = 8;
            int intervalOffset = 2;
            for(int x = 0; x < World.width; x++)
            {
                if(interval > 0)
                {
                    interval--;
                }
                else
                {
                    for(int y = 0; y < World.height; y++)
                    {
                        WorldTile worldTile = World.GetTileAt(x, y, World.Tilemap.Solids);
                        Tile tile = Tile.GetTileById(worldTile?.id ?? 0);
                        if(tile == Tile.sand)
                        {
                            Environmental environmental = Environmental.seaweed;
                            if(World.AddEnvironmentalAt(x, y, environmental))
                            {
                                x += (environmental.sprite.textures[0].Width / Tile.size) - 1;
                            }
                            break;
                        }
                    }
                    interval = intervalMax + Main.random.Next(-intervalOffset, intervalOffset);
                }
            }
            SurfaceSpawnGeneration surfaceSpawnGeneration = (SurfaceSpawnGeneration)World.generations.Find((WorldGeneration worldGeneration) => worldGeneration is SurfaceSpawnGeneration);
            int spawnStatueX, spawnStatueY;
            do
            {
                spawnStatueX = ((World.width - surfaceSpawnGeneration.width) / 2) + Main.random.Next(surfaceSpawnGeneration.width);
                spawnStatueY = 0;
                for(int y = 0; y < World.height; y++)
                {
                    if(World.GetTileAt(spawnStatueX, y, World.Tilemap.Solids) != null)
                    {
                        spawnStatueY = y;
                        break;
                    }
                }
            } while(!World.AddEnvironmentalAt(spawnStatueX, spawnStatueY, Environmental.spawnStatue));
            World.playerSpawnPosition = new Vector2(spawnStatueX, spawnStatueY - 1.5f) * Tile.size;
        }
    }
}