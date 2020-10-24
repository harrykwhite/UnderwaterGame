using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using UnderwaterGame.Entities;
using UnderwaterGame.Entities.Characters;
using UnderwaterGame.Environmentals;
using UnderwaterGame.Items;
using UnderwaterGame.Tiles;
using UnderwaterGame.UI;
using UnderwaterGame.Utilities;
using UnderwaterGame.Worlds.Areas;
using UnderwaterGame.Worlds.Generation;

namespace UnderwaterGame.Worlds
{
    public class World
    {
        public enum TilemapType
        {
            Solids,
            Walls,
            Liquids
        }

        public PlayerCharacter player;
        public Vector2 playerSpawn;

        public WorldTile[][,] Tilemaps { get; private set; }
        public List<WorldEnvironmental> Environmentals { get; private set; }

        public List<WorldArea> Areas { get; private set; }
        public List<WorldGeneration> Generations { get; private set; }

        public int Width => 600;
        public int Height => 400;

        public int RealWidth => Width * Tile.Size;
        public int RealHeight => Height * Tile.Size;

        public float BaseGravityAcc => 0.15f;
        public float BaseGravityMax => 8f;

        public World()
        {
            Tilemaps = new WorldTile[Enum.GetNames(typeof(TilemapType)).Length][,];

            Tilemaps[(byte)TilemapType.Solids] = new WorldTile[Width, Height];
            Tilemaps[(byte)TilemapType.Walls] = new WorldTile[Width, Height];
            Tilemaps[(byte)TilemapType.Liquids] = new WorldTile[Width, Height];

            for (byte m = 0; m < Tilemaps.Length; m++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Tilemaps[m][x, y] = null;
                    }
                }
            }

            Environmentals = new List<WorldEnvironmental>();

            InitAreas();
            InitGenerations();
        }

        public void UpdateAreas()
        {
            if (Main.IsPaused)
            {
                return;
            }

            foreach (WorldArea area in Areas)
            {
                area.UpdateSpawn();
            }
        }

        public void DrawTilemaps()
        {
            int xStart = (int)Math.Max(0f, Camera.Shape.position.X / Tile.Size);
            int yStart = (int)Math.Max(0f, Camera.Shape.position.Y / Tile.Size);

            int xEnd = (int)Math.Min(Width - 1f, (Camera.Shape.position.X + Camera.Width) / Tile.Size);
            int yEnd = (int)Math.Min(Height - 1f, (Camera.Shape.position.Y + Camera.Height) / Tile.Size);

            for (byte m = 0; m < Tilemaps.Length; m++)
            {
                for (int y = yStart; y <= yEnd; y++)
                {
                    for (int x = xStart; x <= xEnd; x++)
                    {
                        WorldTile worldTile = Tilemaps[m][x, y];

                        if (worldTile == null)
                        {
                            continue;
                        }

                        Main.SpriteBatch.Draw(worldTile.TileType.Textures[worldTile.texture], new Vector2(x, y) * Tile.Size, null, worldTile.TileType.GetTilemapColor() * worldTile.TileType.Alpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, worldTile.TileType.GetTilemapDepth());
                    }
                }
            }
        }

        public bool AddTileAt(int x, int y, TilemapType tilemap, Tile tile)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return false;
            }

            if (GetTileAt(x, y, tilemap) != null)
            {
                return false;
            }

            WorldTile worldTile = new WorldTile()
            {
                id = tile.id
            };

            Tilemaps[(byte)tilemap][x, y] = worldTile;

            return true;
        }

        public void RemoveTileAt(int x, int y, TilemapType tilemap)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return;
            }

            Tilemaps[(byte)tilemap][x, y] = null;
        }

        public WorldTile GetTileAt(int x, int y, TilemapType tilemap)
        {
            while (x < 0)
            {
                x++;
            }

            while (y < 0)
            {
                y++;
            }

            while (x >= Width)
            {
                x--;
            }

            while (y >= Height)
            {
                y--;
            }

            return Tilemaps[(byte)tilemap][x, y];
        }

        public WorldTileData GetTileDataAt(int x, int y, TilemapType tilemap, Predicate<WorldTileData> predicate = null)
        {
            WorldTile worldTile = Main.World.GetTileAt(x, y, tilemap);
            WorldTileData worldTileData = null;

            if (worldTile != null)
            {
                worldTileData = new WorldTileData(x, y, worldTile, tilemap);

                if (predicate != null)
                {
                    if (!predicate.Invoke(worldTileData))
                    {
                        worldTileData = null;
                    }
                }
            }

            return worldTileData;
        }

        public List<WorldTileData> GetTileDataRange(int x, int y, TilemapType tilemap, int range, Predicate<WorldTileData> predicate = null)
        {
            List<WorldTileData> data = new List<WorldTileData>();

            for (int ty = y - range; ty <= y + range; ty++)
            {
                for (int tx = x - range; tx <= x + range; tx++)
                {
                    WorldTileData worldTileData = GetTileDataAt(tx, ty, tilemap, predicate);

                    if (worldTileData != null)
                    {
                        data.Add(worldTileData);
                    }
                }
            }

            return data;
        }

        public bool AddEnvironmentalAt(int x, int y, Environmental environmental)
        {
            int ew = environmental.Sprite.Width / Tile.Size;
            int eh = environmental.Sprite.Height / Tile.Size;

            if (GetEnvironmentalAt(x, y) != null)
            {
                return false;
            }

            for (int ey = 0; ey <= eh; ey++)
            {
                for (int ex = 0; ex < ew; ex++)
                {
                    int tx = x + ex - (ew / 2);
                    int ty = y + ey - eh;

                    WorldTile worldTile = GetTileAt(tx, ty, TilemapType.Solids);
                    WorldTileData worldTileData = GetTileDataAt(tx, ty, TilemapType.Solids);

                    if (ey == eh)
                    {
                        if (worldTile == null)
                        {
                            return false;
                        }

                        if (worldTileData.Shape.fill != Shape.Fill.Rectangle)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (worldTile != null)
                        {
                            return false;
                        }
                    }
                }
            }

            WorldEnvironmental worldEnvironmental = new WorldEnvironmental()
            {
                id = environmental.id,
                x = x,
                y = y
            };

            Environmentals.Add(worldEnvironmental);
            return true;
        }

        public void RemoveEnvironmentalAt(int x, int y)
        {
            List<Entity> environmentalEntities = EntityManager.Entities.FindAll((Entity entity) => entity is EnvironmentalEntity);

            foreach (Entity environmentalEntity in environmentalEntities)
            {
                EnvironmentalEntity environmental = (EnvironmentalEntity)environmentalEntity;

                if (environmental.WorldEnvironmental.x == x && environmental.WorldEnvironmental.y == y)
                {
                    Environmentals.Remove(environmental.WorldEnvironmental);
                    environmental.Destroy();
                }
            }
        }

        public WorldEnvironmental GetEnvironmentalAt(int x, int y)
        {
            foreach (WorldEnvironmental worldEnvironmental in Environmentals)
            {
                if (worldEnvironmental.x == x && worldEnvironmental.y == y)
                {
                    return worldEnvironmental;
                }
            }

            return null;
        }

        public Vector2 GetEnvironmentalWorldPosition(int x, int y, Environmental environmental)
        {
            return (new Vector2(x, y) * Tile.Size) - new Vector2(0f, environmental.Sprite.Origin.Y - environmental.Sprite.Bound.Y);
        }

        public void Generate()
        {
            Main.loading = new Thread(delegate ()
            {
                GenerateWorld();
                Main.loading = null;
            });

            Main.loading.Start();
        }

        private void GenerateWorld()
        {
            while (UIManager.FadeElements[2].Alpha < UIManager.FadeElements[2].alphaMax)
            {
                continue;
            }

            if (Main.Save != null)
            {
                for (byte m = 0; m < Tilemaps.Length; m++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            if (Main.Save.tilemaps[m][x, y] != null)
                            {
                                AddTileAt(x, y, (TilemapType)m, Main.Save.tilemaps[m][x, y].TileType);
                                Tilemaps[m][x, y].texture = Main.Save.tilemaps[m][x, y].texture;
                                Tilemaps[m][x, y].lighting = Main.Save.tilemaps[m][x, y].lighting;
                            }
                        }
                    }
                }

                foreach (WorldEnvironmental environmental in Main.Save.environmentals)
                {
                    Environmentals.Add(environmental);
                }
            }
            else
            {
                foreach (WorldGeneration generation in Generations)
                {
                    generation.Generate(this);

                    for (byte m = 0; m < Tilemaps.Length; m++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            for (int x = 0; x < Width; x++)
                            {
                                if (Tilemaps[m][x, y] == null)
                                {
                                    continue;
                                }

                                SetTileTexture(x, y, (TilemapType)m);

                                if ((TilemapType)m == TilemapType.Solids)
                                {
                                    SetTileLighting(x, y, (TilemapType)m);
                                }
                            }
                        }
                    }
                }
            }

            SpawnPlayer();
            SpawnEnvironmentals();
        }

        private void SetTileTexture(int x, int y, TilemapType tilemap)
        {
            WorldTile worldTile = Tilemaps[(byte)tilemap][x, y];
            Tile tile = worldTile.TileType;

            bool left = GetTileAt(x - 1, y, tilemap)?.TileType == tile;
            bool right = GetTileAt(x + 1, y, tilemap)?.TileType == tile;
            bool top = GetTileAt(x, y - 1, tilemap)?.TileType == tile;
            bool bottom = GetTileAt(x, y + 1, tilemap)?.TileType == tile;

            bool leftEmpty = GetTileAt(x - 1, y, tilemap) == null;
            bool rightEmpty = GetTileAt(x + 1, y, tilemap) == null;
            bool topEmpty = GetTileAt(x, y - 1, tilemap) == null;
            bool bottomEmpty = GetTileAt(x, y + 1, tilemap) == null;

            switch (tilemap)
            {
                case TilemapType.Solids:
                case TilemapType.Walls:
                    if (left && right && top && bottom)
                    {
                        worldTile.texture = 0;
                    }

                    if (!left && !right && !top && !bottom)
                    {
                        worldTile.texture = 1;
                    }

                    if (!left && !right && !top && bottom)
                    {
                        worldTile.texture = 2;
                    }

                    if (!left && !right && top && bottom)
                    {
                        worldTile.texture = 3;
                    }

                    if (!left && !right && top && !bottom)
                    {
                        worldTile.texture = 4;
                    }

                    if (!left && right && !top && !bottom)
                    {
                        worldTile.texture = 5;
                    }

                    if (left && right && !top && !bottom)
                    {
                        worldTile.texture = 6;
                    }

                    if (left && !right && !top && !bottom)
                    {
                        worldTile.texture = 7;
                    }

                    if (!left && right && !top && bottom)
                    {
                        worldTile.texture = 8;
                    }

                    if (left && right && !top && bottom)
                    {
                        worldTile.texture = 9;
                    }

                    if (left && !right && !top && bottom)
                    {
                        worldTile.texture = 10;
                    }

                    if (!left && right && top && bottom)
                    {
                        worldTile.texture = 11;
                    }

                    if (left && !right && top && bottom)
                    {
                        worldTile.texture = 12;
                    }

                    if (!left && right && top && !bottom)
                    {
                        worldTile.texture = 13;
                    }

                    if (left && right && top && !bottom)
                    {
                        worldTile.texture = 14;
                    }

                    if (left && !right && top && !bottom)
                    {
                        worldTile.texture = 15;
                    }

                    if (leftEmpty && !rightEmpty && topEmpty && !bottomEmpty)
                    {
                        worldTile.texture = 16;
                    }

                    if (!leftEmpty && rightEmpty && topEmpty && !bottomEmpty)
                    {
                        worldTile.texture = 17;
                    }

                    if (leftEmpty && !rightEmpty && !topEmpty && bottomEmpty)
                    {
                        worldTile.texture = 18;
                    }

                    if (!leftEmpty && rightEmpty && !topEmpty && bottomEmpty)
                    {
                        worldTile.texture = 19;
                    }
                    break;
            }
        }

        private void SetTileLighting(int x, int y, TilemapType tilemap)
        {
            WorldTile worldTile = Tilemaps[(byte)tilemap][x, y];
            WorldTileData worldTileData = GetTileDataAt(x, y, tilemap);

            int range = 255 / Lighting.Jump;

            for (int r = 1; r <= range; r++)
            {
                bool foundEmpty = false;

                for (int ty = y - r; ty <= y + r; ty++)
                {
                    for (int tx = x - r; tx <= x + r; tx++)
                    {
                        if (GetTileAt(tx, ty, TilemapType.Solids) == null)
                        {
                            foundEmpty = true;
                            break;
                        }
                    }

                    if (foundEmpty)
                    {
                        break;
                    }
                }

                if (foundEmpty)
                {
                    break;
                }

                worldTile.lighting = (byte)MathUtilities.Clamp(r * Lighting.Jump, 0f, 255f);
            }
        }

        private void InitAreas()
        {
            Areas = new List<WorldArea> {
                new SurfaceArea()
            };
        }

        private void InitGenerations()
        {
            Generations = new List<WorldGeneration> {
                new SurfaceTerrainGeneration(),
                new SurfaceTowerGeneration(),
                new SurfaceEnvironmentalGeneration(),
                new CleaningGeneration()
            };
        }

        private void SpawnPlayer()
        {
            playerSpawn = new Vector2(RealWidth / 2f, 200f);
            player = (PlayerCharacter)EntityManager.AddEntity<PlayerCharacter>(playerSpawn);

            Camera.positionTo = player.position;
            Camera.position = Camera.positionTo;

            Item[] items = Item.Items.ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                player.Inventory.AddItem(items[i], 99);
            }
        }

        private void SpawnEnvironmentals()
        {
            foreach (WorldEnvironmental worldEnvironmental in Environmentals)
            {
                EnvironmentalEntity environmentalEntity = (EnvironmentalEntity)EntityManager.AddEntity<EnvironmentalEntity>(GetEnvironmentalWorldPosition(worldEnvironmental.x, worldEnvironmental.y, worldEnvironmental.EnvironmentalType));
                environmentalEntity.SetEnvironmental(worldEnvironmental.EnvironmentalType, worldEnvironmental);
            }
        }
    }
}