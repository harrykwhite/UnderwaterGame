using Microsoft.Xna.Framework;
using System;
using UnderwaterGame.Entities;
using UnderwaterGame.Environmentals;
using UnderwaterGame.Items;
using UnderwaterGame.Tiles;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Generation
{
    public class SpawnGeneration : Generation
    {
        public int width = 32;

        public int height = 12;

        public override void Generate()
        {
            int xStart = (World.width - width) / 2;
            int xEnd = (World.width + width) / 2;
            for(int x = xStart; x <= xEnd; x++)
            {
                int yStart = 0;
                while(World.GetTileAt(x, yStart, World.Tilemap.Solids) == null)
                {
                    yStart++;
                }
                int yEnd = yStart + Math.Min(Math.Min(x - xStart, xEnd - x), height) - 1;
                for(int y = yStart; y <= yEnd; y++)
                {
                    World.RemoveTileAt(x, y, World.Tilemap.Solids);
                }
            }
            SpawnGeneration spawnGeneration = (SpawnGeneration)World.generations.Find((Generation generation) => generation is SpawnGeneration);
            int statueX, statueY;
            do
            {
                statueX = ((World.width - spawnGeneration.width) / 2) + Main.random.Next(spawnGeneration.width);
                statueY = 0;
                for(int y = 0; y < World.height; y++)
                {
                    if(World.GetTileAt(statueX, y, World.Tilemap.Solids) != null)
                    {
                        statueY = y;
                        break;
                    }
                }
            } while(!World.AddEnvironmentalAt(statueX, statueY, Environmental.statue));
            ItemDropEntity itemDrop = (ItemDropEntity)EntityManager.AddEntity<ItemDropEntity>(new Vector2(statueX + (Environmental.statue.sprite.textures[0].Width / (Tile.size * 2f)), statueY - 8f) * Tile.size);
            itemDrop.SetItem(Item.woodenTrident, 1);
            World.playerSpawnPosition = new Vector2(statueX + (Environmental.statue.sprite.textures[0].Width / (Tile.size * 2f)), statueY - 1.5f) * Tile.size;
        }
    }
}