namespace UnderwaterGame.Generation
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Items;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Worlds;

    public class EnvironmentalGeneration : Generation
    {
        public override void Generate()
        {
            int intervalMax = 8;
            int interval = intervalMax / 2;
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
            SpawnGeneration spawnGeneration = (SpawnGeneration)World.generations.Find((Generation generation) => generation is SpawnGeneration);
            int spawnStatueX, spawnStatueY;
            do
            {
                spawnStatueX = ((World.width - spawnGeneration.width) / 2) + Main.random.Next(spawnGeneration.width);
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
            ItemDropEntity itemDrop = (ItemDropEntity)EntityManager.AddEntity<ItemDropEntity>(new Vector2(spawnStatueX * Tile.size, (spawnStatueY - 8f) * Tile.size));
            itemDrop.SetItem(Item.woodenTrident, 1);
            World.playerSpawnPosition = new Vector2(spawnStatueX, spawnStatueY - 1.5f) * Tile.size;
        }
    }
}