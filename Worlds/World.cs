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
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds.Generation;

    public static class World
    {
        public enum Tilemap
        {
            Solids,

            Walls,

            Liquids
        }

        public static PlayerCharacter player;

        public static int playerSpawnTime;

        public static int playerSpawnTimeMax = 60;

        public static Vector2 playerSpawnPosition;

        public static WorldTile[][,] tilemaps;

        public static List<WorldEnvironmental> environmentals;

        public static List<WorldGeneration> generations;

        public static List<Hotspot> hotspots;

        public static int width = 1200;

        public static int height = 400;

        public static float gravityAcc = 0.15f;

        public static float gravityMax = 8f;

        public static int bubbleTime;

        public static int bubbleTimeMax = 60;

        public static int fishTime;

        public static int fishTimeMax = 300;

        public static Hotspot hotspotCurrent;

        public static Hotspot hotspotPrevious;

        public static void Init()
        {
            player = null;
            playerSpawnTime = 0;
            tilemaps = new WorldTile[Enum.GetNames(typeof(Tilemap)).Length][,];
            tilemaps[(byte)Tilemap.Solids] = new WorldTile[width, height];
            tilemaps[(byte)Tilemap.Walls] = new WorldTile[width, height];
            tilemaps[(byte)Tilemap.Liquids] = new WorldTile[width, height];
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
            generations = new List<WorldGeneration> {
                new SurfaceTerrainGeneration(),
                new SurfaceSpawnGeneration(),
                new SurfaceTowerGeneration(),
                new SurfaceEnvironmentalGeneration(),
                new CleaningGeneration(),
                new HotspotGeneration()
            };
            hotspots = new List<Hotspot>();
            bubbleTime = 0;
            fishTime = 0;
            hotspotCurrent = null;
            hotspotPrevious = null;
        }

        public static void Update()
        {
            if(UiManager.menuCurrent != null)
            {
                return;
            }
            hotspotPrevious = hotspotCurrent;
            hotspotCurrent = null;
            if(player?.GetExists() ?? false)
            {
                foreach(Hotspot hotspot in hotspots)
                {
                    if(hotspot.count > 0)
                    {
                        if(Vector2.Distance(player.position, hotspot.position) <= Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f)
                        {
                            hotspotCurrent = hotspot;
                        }
                    }
                }
            }
            else
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
                }
            }
            foreach(Hotspot hotspot in hotspots)
            {
                hotspot.Update();
            }
            if(bubbleTime < bubbleTimeMax)
            {
                bubbleTime++;
            }
            else
            {
                int trials = 100;
                Bubble bubble = (Bubble)EntityManager.AddEntity<Bubble>(Vector2.Zero);
                do
                {
                    bubble.position = new Vector2(RandomUtilities.Range(Camera.position.X - (Camera.GetWidth() / 2f), Camera.position.X + (Camera.GetWidth() / 2f)), RandomUtilities.Range(Camera.position.Y - (Camera.GetHeight() / 2f), Camera.position.Y + (Camera.GetHeight() / 2f)));
                    trials--;
                } while(!bubble.TileTypeCollision(bubble.position, Tile.water, Tilemap.Liquids) && trials > 0);
                if(trials > 0)
                {
                    bubble.direction = -MathHelper.Pi / 2f;
                    bubble.alpha = 0f;
                    bubble.life = -30;
                }
                else
                {
                    bubble.Destroy();
                }
                bubbleTime = 0;
            }
            if(fishTime < fishTimeMax)
            {
                fishTime++;
            }
            else
            {
                List<Fish> fishies = new List<Fish>();
                bool fishGroupLeft = Main.random.Next(2) == 0;
                Vector2 fishGroupPosition = Camera.position + new Vector2(Camera.GetWidth() * 0.5f * (fishGroupLeft ? 1f : -1f), RandomUtilities.Range(-Camera.GetHeight() / 2f, Camera.GetHeight() / 2f));
                int fishCount = (Main.random.Next(5) == 0 ? 5 : 0) + 1;
                float fishOffset = 8f;
                float fishDirectionOffset = MathHelper.ToRadians(Main.random.Next(360));
                for(int i = 0; i < fishCount; i++)
                {
                    Fish fish = (Fish)EntityManager.AddEntity<Fish>(fishGroupPosition + (i > 0 ? MathUtilities.LengthDirection(fishOffset, (((MathHelper.Pi * 2f) / (fishCount - 1f)) * i) + fishDirectionOffset) : Vector2.Zero));
                    fishies.Add(fish);
                    if(fish.TileTypeCollision(fish.position, Tile.water, Tilemap.Liquids))
                    {
                        fish.direction = fishGroupLeft ? MathHelper.Pi : 0f;
                        fish.alpha = 0f;
                        fish.flipHor = fishGroupLeft;
                    }
                    else
                    {
                        foreach(Fish fishy in fishies)
                        {
                            fishy.Destroy();
                        }
                    }
                }
                fishTime = 0;
            }
        }

        public static void Draw()
        {
            Shape cameraShape = Camera.GetShape();
            int xStart = (int)Math.Max(0f, cameraShape.position.X / Tile.size);
            int yStart = (int)Math.Max(0f, cameraShape.position.Y / Tile.size);
            int xEnd = (int)Math.Min(width - 1f, Math.Ceiling((cameraShape.position.X + Camera.GetWidth()) / Tile.size));
            int yEnd = (int)Math.Min(height - 1f, Math.Ceiling((cameraShape.position.Y + Camera.GetHeight()) / Tile.size));
            for(byte m = 0; m < tilemaps.Length; m++)
            {
                for(int y = yStart; y <= yEnd; y++)
                {
                    for(int x = xStart; x <= xEnd; x++)
                    {
                        WorldTile worldTile = tilemaps[m][x, y];
                        Color color = Color.White;
                        float depth = 0.65f;
                        Vector2 offset = Vector2.Zero;
                        switch((Tilemap)m)
                        {
                            case Tilemap.Walls:
                                color = new Color(80, 80, 80);
                                depth = 0.25f;
                                break;

                            case Tilemap.Liquids:
                                depth = 0.75f;
                                break;
                        }
                        if(worldTile == null)
                        {
                            continue;
                        }
                        Main.spriteBatch.Draw(Tile.GetTileById(worldTile.id).textures[worldTile.texture], (new Vector2(x, y) * Tile.size) + offset, null, color * Tile.GetTileById(worldTile.id).alpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, depth);
                    }
                }
            }
            foreach(Hotspot hotspot in hotspots)
            {
                Main.spriteBatch.Draw(Main.textureLibrary.OTHER_HOTSPOT.asset, hotspot.position, null, Color.White * hotspot.alpha, 0f, new Vector2(Main.textureLibrary.OTHER_HOTSPOT.asset.Width, Main.textureLibrary.OTHER_HOTSPOT.asset.Height) / 2f, 1f, SpriteEffects.None, 0.9f);
            }
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
            WorldTile worldTile = new WorldTile() { id = tile.id };
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
                worldTileData = new WorldTileData(x, y, worldTile, tilemap);
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
            int ew = environmental.sprite.textures[0].Width / Tile.size;
            int eh = environmental.sprite.textures[0].Height / Tile.size;
            if(GetEnvironmentalAt(x, y) != null)
            {
                return false;
            }
            for(int ey = 0; ey <= eh; ey++)
            {
                for(int ex = 0; ex < ew; ex++)
                {
                    int tx = x + ex - (int)Math.Ceiling(ew / 2f);
                    int ty = y + ey - eh;
                    if(!(GetTileAt(tx, ty, Tilemap.Solids) == null ^ ey == eh))
                    {
                        return false;
                    }
                }
            }
            environmentals.Add(new WorldEnvironmental(environmental.id, x, y));
            return true;
        }

        public static void RemoveEnvironmentalAt(int x, int y)
        {
            List<Entity> environmentalEntities = EntityManager.entities.FindAll((Entity entity) => entity is EnvironmentalEntity);
            foreach(Entity environmentalEntity in environmentalEntities)
            {
                EnvironmentalEntity environmental = (EnvironmentalEntity)environmentalEntity;
                if(environmental.worldEnvironmental.x == x && environmental.worldEnvironmental.y == y)
                {
                    environmentals.Remove(environmental.worldEnvironmental);
                    environmental.Destroy();
                }
            }
        }

        public static WorldEnvironmental GetEnvironmentalAt(int x, int y)
        {
            foreach(WorldEnvironmental worldEnvironmental in environmentals)
            {
                if(worldEnvironmental.x == x && worldEnvironmental.y == y)
                {
                    return worldEnvironmental;
                }
            }
            return null;
        }

        public static Vector2 GetEnvironmentalWorldPosition(int x, int y, Environmental environmental)
        {
            return (new Vector2(x, y) * Tile.size) - new Vector2(0f, environmental.sprite.origin.Y - environmental.sprite.bound.Y);
        }

        public static void AddHotspotAt(Vector2 position, Hotspot.Spawn[] spawns, int spawnMax, int spawnTimeMax, int count)
        {
            hotspots.Add(new Hotspot(position, spawns, spawnMax, spawnTimeMax, count));
        }

        public static void Generate()
        {
            Main.loading = new Thread(delegate ()
            {
                try
                {
                    GenerateWorld();
                }
                catch
                {
                    Main.save = null;
                    GenerateWorld();
                }
                Main.loading = null;
            });
            Main.loading.Start();
        }

        private static void GenerateWorld()
        {
            while((UiManager.fadeElements[2]?.alpha ?? 0f) < (UiManager.fadeElements[2]?.alphaMax ?? 0f))
            {
                continue;
            }
            if(Main.save != null)
            {
                for(byte m = 0; m < tilemaps.Length; m++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            if(Main.save.tilemaps[m][x, y] != null)
                            {
                                AddTileAt(x, y, (Tilemap)m, Tile.GetTileById(Main.save.tilemaps[m][x, y].id));
                                tilemaps[m][x, y].texture = Main.save.tilemaps[m][x, y].texture;
                            }
                        }
                    }
                }
                foreach(WorldEnvironmental environmental in Main.save.environmentals)
                {
                    environmentals.Add(environmental);
                }
                foreach(WorldHotspot hotspot in Main.save.hotspots)
                {
                    hotspots.Add(new Hotspot(new Vector2(hotspot.x, hotspot.y), hotspot.spawns, hotspot.spawnMax, hotspot.spawnTimeMax, hotspot.count));
                }
                playerSpawnPosition = new Vector2(Main.save.playerSpawnX, Main.save.playerSpawnY);
            }
            else
            {
                foreach(WorldGeneration generation in generations)
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
            }
            Camera.positionTo = playerSpawnPosition;
            Camera.position = Camera.positionTo;
            foreach(WorldEnvironmental worldEnvironmental in environmentals)
            {
                EnvironmentalEntity environmentalEntity = (EnvironmentalEntity)EntityManager.AddEntity<EnvironmentalEntity>(GetEnvironmentalWorldPosition(worldEnvironmental.x, worldEnvironmental.y, Environmental.GetEnvironmentalById(worldEnvironmental.id)));
                environmentalEntity.SetEnvironmental(Environmental.GetEnvironmentalById(worldEnvironmental.id), worldEnvironmental);
            }
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
            switch(tilemap)
            {
                case Tilemap.Liquids:
                    if(topEmpty)
                    {
                        worldTile.texture = 1;
                    }
                    break;

                default:
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
                    break;
            }
        }
    }
}