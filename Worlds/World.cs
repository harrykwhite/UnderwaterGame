namespace UnderwaterGame.Worlds
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Generation;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Utilities;

    public static class World
    {
        public enum Tilemap
        {
            Solids,

            Walls
        }

        public static PlayerCharacter player;

        public static int playerSpawnTime;

        public static int playerSpawnTimeMax = 60;

        public static bool playerSpawned;

        public static Vector2 playerSpawnPosition;

        public static WorldTile[][,] tilemaps;

        public static List<WorldEnvironmental> environmentals;

        public static int width = 1200;

        public static int height = 300;

        public static int bubbleSmallTime;

        public static int bubbleSmallTimeMax = 120;

        public static List<Hotspot> hotspots;
        
        public static Hotspot hotspotCurrent;

        public static Hotspot hotspotPrevious;

        public static List<Generation> generations;

        public static bool generated;

        public static void Init()
        {
            player = null;
            playerSpawnTime = 0;
            playerSpawned = false;
            tilemaps = new WorldTile[Enum.GetNames(typeof(Tilemap)).Length][,];
            tilemaps[(byte)Tilemap.Solids] = new WorldTile[width, height];
            tilemaps[(byte)Tilemap.Walls] = new WorldTile[width, height];
            for(byte m = 0; m < tilemaps.Length; m++)
            {
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        tilemaps[m][x, y] = null;
                    }
                }
            }
            environmentals = new List<WorldEnvironmental>();
            bubbleSmallTime = 0;
            hotspots = new List<Hotspot>();
            hotspotCurrent = null;
            hotspotPrevious = null;
            generated = false;
            generations = new List<Generation>() {
                new TerrainGeneration(),
                new SpawnGeneration(),
                new TowerGeneration(),
                new SeaweedGeneration(),
                new RockGeneration(),
                new HotspotGeneration()
            };
        }

        public static void Update()
        {
            if(UiManager.menuCurrent != null)
            {
                return;
            }
            if(player?.GetExists() ?? false)
            {
                hotspotPrevious = hotspotCurrent;
                hotspotCurrent = null;
                foreach(Hotspot hotspot in hotspots)
                {
                    if(Vector2.Distance(player.position, hotspot.position) <= Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f)
                    {
                        hotspotCurrent = hotspot;
                    }
                }
                foreach(Hotspot hotspot in hotspots)
                {
                    hotspot.Update();
                }
            }
            else
            {
                if(!playerSpawned)
                {
                    if(playerSpawnTime < playerSpawnTimeMax)
                    {
                        playerSpawnTime++;
                    }
                    else
                    {
                        player = (PlayerCharacter)EntityManager.AddEntity<PlayerCharacter>(playerSpawnPosition);
                        player.flashTime = player.flashTimeMax;
                        int particleCount = 6;
                        for(int i = 0; i < particleCount; i++)
                        {
                            Blood blood = (Blood)EntityManager.AddEntity<Blood>(player.position);
                            blood.direction = ((MathHelper.Pi * 2f) / particleCount) * i;
                        }
                        playerSpawned = true;
                    }
                }
            }
            if(bubbleSmallTime < bubbleSmallTimeMax)
            {
                bubbleSmallTime++;
            }
            else
            {
                int trials = 100;
                int particleCount = 3;
                Vector2[] bubbleSmallPositions = new Vector2[particleCount];
                for(int i = 0; i < particleCount; i++)
                {
                    BubbleSmall bubbleSmall = (BubbleSmall)EntityManager.AddEntity<BubbleSmall>(Vector2.Zero);
                    bool valid;
                    do
                    {
                        valid = true;
                        bubbleSmall.position = Camera.position + new Vector2(RandomUtilities.Range(-Camera.GetWidth() / 2f, Camera.GetWidth() / 2f), RandomUtilities.Range(-Camera.GetHeight() / 2f, Camera.GetHeight() / 2f));
                        for(int ii = i - 1; ii >= 0; ii--)
                        {
                            if(Vector2.Distance(bubbleSmall.position, bubbleSmallPositions[ii]) <= 128f)
                            {
                                valid = false;
                                break;
                            }
                        }
                        trials--;
                    } while(!valid && trials > 0);
                    if(trials > 0)
                    {
                        bubbleSmall.direction = -MathHelper.Pi / 2f;
                        bubbleSmall.alpha = 0f;
                        bubbleSmall.life = -30;
                        bubbleSmallPositions[i] = bubbleSmall.position;
                    }
                    else
                    {
                        bubbleSmall.Destroy();
                        break;
                    }
                }
                bubbleSmallTime = 0;
            }
        }

        public static void Draw()
        {
            int xStart = (int)Math.Max(0f, (Camera.position.X - (Camera.GetWidth() / 2f)) / Tile.size);
            int yStart = (int)Math.Max(0f, (Camera.position.Y - (Camera.GetHeight() / 2f)) / Tile.size);
            int xEnd = (int)Math.Min(width - 1f, Math.Ceiling((Camera.position.X + (Camera.GetWidth() / 2f)) / Tile.size));
            int yEnd = (int)Math.Min(height - 1f, Math.Ceiling((Camera.position.Y + (Camera.GetHeight() / 2f)) / Tile.size));
            for(byte m = 0; m < tilemaps.Length; m++)
            {
                for(int y = yStart; y <= yEnd; y++)
                {
                    for(int x = xStart; x <= xEnd; x++)
                    {
                        WorldTile worldTile = tilemaps[m][x, y];
                        if(worldTile == null)
                        {
                            continue;
                        }
                        Main.spriteBatch.Draw(Tile.GetTileById(worldTile.id).textures[worldTile.texture], new Vector2(x, y) * Tile.size, null, (Tilemap)m == Tilemap.Walls ? new Color(120, 120, 120) : Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (Tilemap)m == Tilemap.Walls ? 0.25f : 0.65f);
                    }
                }
            }
            foreach(WorldEnvironmental worldEnvironmental in environmentals)
            {
                Environmental environmental = Environmental.GetEnvironmentalById(worldEnvironmental.id);
                Main.spriteBatch.Draw(environmental.sprite.textures[0], (new Vector2(worldEnvironmental.x, worldEnvironmental.y) * Tile.size) - new Vector2(0f, environmental.sprite.textures[0].Height), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.35f);
            }
            foreach(Hotspot hotspot in hotspots)
            {
                Main.spriteBatch.Draw(Main.textureLibrary.OTHER_HOTSPOT.asset, hotspot.position, null, Color.White, 0f, new Vector2(Main.textureLibrary.OTHER_HOTSPOT.asset.Width, Main.textureLibrary.OTHER_HOTSPOT.asset.Height) / 2f, 1f, SpriteEffects.None, 0.9f);
            }
            Main.spriteBatch.Draw(Main.textureLibrary.OTHER_PIXEL.asset, Camera.position - (new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f), null, new Color(55, 154, 204) * 0.2f, 0f, Vector2.Zero, new Vector2(Camera.GetWidth(), Camera.GetHeight()), SpriteEffects.None, 1f);
        }

        public static bool AddTileAt(int x, int y, Tilemap tilemap, Tile tile)
        {
            if(x < 0 || y < 0 || x >= width || y >= height)
            {
                return false;
            }
            if(GetTileAt(x, y, tilemap) != null)
            {
                return false;
            }
            WorldTile worldTile = new WorldTile(tile.id);
            tilemaps[(byte)tilemap][x, y] = worldTile;
            return true;
        }

        public static void RemoveTileAt(int x, int y, Tilemap tilemap)
        {
            if(x < 0 || y < 0 || x >= width || y >= height)
            {
                return;
            }
            tilemaps[(byte)tilemap][x, y] = null;
        }

        public static WorldTile GetTileAt(int x, int y, Tilemap tilemap)
        {
            while(x < 0)
            {
                x++;
            } while(y < 0)
            {
                y++;
            } while(x >= width)
            {
                x--;
            } while(y >= height)
            {
                y--;
            }
            return tilemaps[(byte)tilemap][x, y];
        }

        public static WorldTileData GetTileDataAt(int x, int y, Tilemap tilemap, Predicate<WorldTileData> predicate = null)
        {
            WorldTile worldTile = GetTileAt(x, y, tilemap);
            WorldTileData worldTileData = null;
            if(worldTile != null)
            {
                worldTileData = new WorldTileData(worldTile, x, y, tilemap);
                if(predicate != null)
                {
                    if(!predicate(worldTileData))
                    {
                        worldTileData = null;
                    }
                }
            }
            return worldTileData;
        }

        public static List<WorldTileData> GetTileDataRange(int x, int y, Tilemap tilemap, int range, Predicate<WorldTileData> predicate = null)
        {
            List<WorldTileData> data = new List<WorldTileData>();
            for(int ty = y - range; ty <= y + range; ty++)
            {
                for(int tx = x - range; tx <= x + range; tx++)
                {
                    WorldTileData worldTileData = GetTileDataAt(tx, ty, tilemap, predicate);
                    if(worldTileData != null)
                    {
                        data.Add(worldTileData);
                    }
                }
            }
            return data;
        }

        public static bool AddEnvironmentalAt(int x, int y, Environmental environmental)
        {
            if(x < 0 || y < 0 || x >= width || y >= height)
            {
                return false;
            }
            int ew = environmental.sprite.textures[0].Width / Tile.size;
            int eh = environmental.sprite.textures[0].Height / Tile.size;
            for(int ey = 0; ey <= eh; ey++)
            {
                for(int ex = -1; ex <= ew; ex++)
                {
                    int tx = x + ex;
                    int ty = y + ey - eh;
                    if(ex >= 0 && ex < ew && ey < eh)
                    {
                        if(GetEnvironmentalAt(tx, ty) != null)
                        {
                            return false;
                        }
                    }
                    WorldTile worldTile = GetTileAt(tx, ty, Tilemap.Solids);
                    if(ey == eh)
                    {
                        if(worldTile == null)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if(ex >= 0 && ex < ew)
                        {
                            if(worldTile != null)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            environmentals.Add(new WorldEnvironmental(environmental.id, x, y));
            return true;
        }

        public static WorldEnvironmental GetEnvironmentalAt(int x, int y)
        {
            while(x < 0)
            {
                x++;
            } while(y < 0)
            {
                y++;
            } while(x >= width)
            {
                x--;
            } while(y >= height)
            {
                y--;
            }
            foreach(WorldEnvironmental worldEnvironmental in environmentals)
            {
                Environmental environmental = Environmental.GetEnvironmentalById(worldEnvironmental.id);
                if(worldEnvironmental.x >= x && worldEnvironmental.y >= y && worldEnvironmental.x < x + (environmental.sprite.textures[0].Width / Tile.size) && worldEnvironmental.y < y + (environmental.sprite.textures[0].Height / Tile.size))
                {
                    return worldEnvironmental;
                }
            }
            return null;
        }

        public static void Generate()
        {
            Main.loading = new Thread(delegate ()
            {
                GenerateWorld();
                Main.loading = null;
            });
            Main.loading.Start();
        }

        private static void GenerateWorld()
        {
            while((UiManager.fadeElements[3]?.alpha ?? 0f) < (UiManager.fadeElements[3]?.alphaMax ?? 0f))
            {
                continue;
            }
            foreach(Generation generation in generations)
            {
                generation.Generate();
                for(byte m = 0; m < tilemaps.Length; m++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            if(tilemaps[m][x, y] == null)
                            {
                                continue;
                            }
                            SetTileTexture(x, y, (Tilemap)m);
                        }
                    }
                }
            }
            Camera.positionTo = playerSpawnPosition;
            Camera.position = Camera.positionTo;
            generated = true;
        }

        private static void SetTileTexture(int x, int y, Tilemap tilemap)
        {
            WorldTile worldTile = tilemaps[(byte)tilemap][x, y];
            Tile tile = Tile.GetTileById(worldTile.id);
            bool left = Tile.GetTileById(GetTileAt(x - 1, y, tilemap)?.id ?? 0) == tile;
            bool right = Tile.GetTileById(GetTileAt(x + 1, y, tilemap)?.id ?? 0) == tile;
            bool top = Tile.GetTileById(GetTileAt(x, y - 1, tilemap)?.id ?? 0) == tile;
            bool bottom = Tile.GetTileById(GetTileAt(x, y + 1, tilemap)?.id ?? 0) == tile;
            bool leftEmpty = GetTileAt(x - 1, y, tilemap) == null;
            bool rightEmpty = GetTileAt(x + 1, y, tilemap) == null;
            bool topEmpty = GetTileAt(x, y - 1, tilemap) == null;
            bool bottomEmpty = GetTileAt(x, y + 1, tilemap) == null;
            if(!left && !right && !top && !bottom)
            {
                worldTile.texture = 1;
            }
            if(!left && !right && !top && bottom)
            {
                worldTile.texture = 2;
            }
            if(!left && !right && top && bottom)
            {
                worldTile.texture = 3;
            }
            if(!left && !right && top && !bottom)
            {
                worldTile.texture = 4;
            }
            if(!left && right && !top && !bottom)
            {
                worldTile.texture = 5;
            }
            if(left && right && !top && !bottom)
            {
                worldTile.texture = 6;
            }
            if(left && !right && !top && !bottom)
            {
                worldTile.texture = 7;
            }
            if(!left && right && !top && bottom)
            {
                worldTile.texture = 8;
            }
            if(left && right && !top && bottom)
            {
                worldTile.texture = 9;
            }
            if(left && !right && !top && bottom)
            {
                worldTile.texture = 10;
            }
            if(!left && right && top && bottom)
            {
                worldTile.texture = 11;
            }
            if(left && !right && top && bottom)
            {
                worldTile.texture = 12;
            }
            if(!left && right && top && !bottom)
            {
                worldTile.texture = 13;
            }
            if(left && right && top && !bottom)
            {
                worldTile.texture = 14;
            }
            if(left && !right && top && !bottom)
            {
                worldTile.texture = 15;
            }
            if(leftEmpty && !rightEmpty && topEmpty && !bottomEmpty)
            {
                worldTile.texture = 16;
            }
            if(!leftEmpty && rightEmpty && topEmpty && !bottomEmpty)
            {
                worldTile.texture = 17;
            }
            if(leftEmpty && !rightEmpty && !topEmpty && bottomEmpty)
            {
                worldTile.texture = 18;
            }
            if(!leftEmpty && rightEmpty && !topEmpty && bottomEmpty)
            {
                worldTile.texture = 19;
            }
        }
    }
}