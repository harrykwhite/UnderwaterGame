﻿namespace UnderwaterGame.Worlds
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Entities.Characters;
    using UnderwaterGame.Entities.Characters.Enemies;
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
            FirstWalls,
            SecondWalls,
            Liquids
        }

        public static PlayerCharacter player;

        public static WorldTile[][,] tilemaps;

        public static List<WorldEnvironmental> environmentals;

        public static List<WorldHotspot> hotspots;

        public static List<WorldGeneration> generations;

        public static int width = 600;

        public static int height = 400;

        public static float gravityAcc = 0.15f;

        public static float gravityMax = 8f;

        public static int spawnTime;

        public static int spawnTimeMax = 300;

        public static int spawnMax = 8;
        
        public static int bubbleTime;

        public static int bubbleTimeMax = 60;

        public static WorldHotspot hotspotCurrent;

        public static void Init()
        {
            tilemaps = new WorldTile[Enum.GetNames(typeof(Tilemap)).Length][,];
            tilemaps[(byte)Tilemap.Solids] = new WorldTile[width, height];
            tilemaps[(byte)Tilemap.FirstWalls] = new WorldTile[width, height];
            tilemaps[(byte)Tilemap.SecondWalls] = new WorldTile[width, height];
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
            hotspots = new List<WorldHotspot>();
            generations = new List<WorldGeneration> {
                new SurfaceTerrainGeneration(),
                new SurfaceMountainGeneration(),
                new SurfaceTowerGeneration(),
                new SurfaceEnvironmentalGeneration(),
                new HotspotGeneration(),
                new CleaningGeneration()
            };
            spawnTime = 0;
            bubbleTime = 0;
            hotspotCurrent = null;
        }

        public static void Update()
        {
            if(UiManager.menuCurrent != null)
            {
                return;
            }
            hotspotCurrent = null;
            if(player?.GetExists() ?? false)
            {
                foreach(WorldHotspot hotspot in hotspots)
                {
                    Vector2 particlePosition;
                    do
                    {
                        particlePosition = new Vector2(hotspot.x + Main.random.Next(-Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2, Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2), hotspot.y + Main.random.Next(-Main.textureLibrary.OTHER_HOTSPOT.asset.Height / 2, Main.textureLibrary.OTHER_HOTSPOT.asset.Height / 2));
                    } while(Vector2.Distance(particlePosition, new Vector2(hotspot.x, hotspot.y)) > Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f);
                    EntityManager.AddEntity<Hotspot>(particlePosition);
                    if(Vector2.Distance(player.position, new Vector2(hotspot.x, hotspot.y)) <= Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f)
                    {
                        hotspotCurrent = hotspot;
                    }
                }
            }
            if(spawnTime < spawnTimeMax)
            {
                spawnTime++;
            }
            else
            {
                if(hotspotCurrent != null)
                {
                    if(EntityManager.GetEntityCount<EnemyCharacter>() < spawnMax && hotspotCurrent.count > 0)
                    {
                        int trials = 100;
                        Type enemyType = null;
                        do
                        {
                            for(int i = 0; i < hotspotCurrent.spawns.Length; i++)
                            {
                                if(Main.random.Next(100) <= (hotspotCurrent.spawns[i].chance * 100f))
                                {
                                    enemyType = Type.GetType(hotspotCurrent.spawns[i].type);
                                }
                            }
                        } while(enemyType == null);
                        EnemyCharacter enemy = (EnemyCharacter)EntityManager.AddEntity(enemyType, Vector2.Zero);
                        do
                        {
                            enemy.position = new Vector2(hotspotCurrent.x, hotspotCurrent.y) + MathUtilities.LengthDirection(RandomUtilities.Range(0f, Main.textureLibrary.OTHER_HOTSPOT.asset.Width / 2f), MathHelper.ToRadians(Main.random.Next(360)));
                            trials--;
                        } while((enemy.TileCollision(enemy.position, Tilemap.Solids) || !enemy.TileCollision(enemy.position, Tilemap.Liquids)) && trials > 0);
                        if(trials > 0)
                        {
                            int smokeCount = 6;
                            for(int i = 0; i < smokeCount; i++)
                            {
                                Smoke smoke = (Smoke)EntityManager.AddEntity<Smoke>(enemy.position);
                                smoke.direction = ((MathHelper.Pi * 2f) / smokeCount) * i;
                            }
                            enemy.hotspot = hotspotCurrent;
                        }
                        else
                        {
                            enemy.Destroy();
                        }
                    }
                }
                spawnTime = 0;
            }
            if(bubbleTime < bubbleTimeMax)
            {
                bubbleTime++;
            }
            else
            {
                int bubbleCount = 2 + Main.random.Next(2);
                for(int i = 0; i < bubbleCount; i++)
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
                        break;
                    }
                }
                bubbleTime = 0;
            }
        }

        public static void Draw()
        {
            Shape cameraShape = Camera.GetShape();
            int xStart = (int)Math.Max(0f, cameraShape.position.X / Tile.size);
            int yStart = (int)Math.Max(0f, cameraShape.position.Y / Tile.size);
            int xEnd = (int)Math.Min(width - 1f, (cameraShape.position.X + Camera.GetWidth()) / Tile.size);
            int yEnd = (int)Math.Min(height - 1f, (cameraShape.position.Y + Camera.GetHeight()) / Tile.size);
            for(byte m = 0; m < tilemaps.Length; m++)
            {
                for(int y = yStart; y <= yEnd; y++)
                {
                    for(int x = xStart; x <= xEnd; x++)
                    {
                        WorldTile worldTile = tilemaps[m][x, y];
                        Color color = Color.White;
                        float depth = 0.65f;
                        switch((Tilemap)m)
                        {
                            case Tilemap.FirstWalls:
                                color = new Color(80, 80, 80);
                                depth = 0.25f;
                                break;

                            case Tilemap.SecondWalls:
                                color = new Color(40, 40, 40);
                                depth = 0.15f;
                                break;

                            case Tilemap.Liquids:
                                depth = 0.75f;
                                break;
                        }
                        if(worldTile == null)
                        {
                            continue;
                        }
                        Main.spriteBatch.Draw(Tile.GetTileById(worldTile.id).textures[worldTile.texture], new Vector2(x, y) * Tile.size, null, color * Tile.GetTileById(worldTile.id).alpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, depth);
                    }
                }
            }
            for(int i = 0; i < (width * Tile.size) / Main.textureLibrary.OTHER_CLOUD.asset.Width; i++)
            {
                Main.spriteBatch.Draw(Main.textureLibrary.OTHER_CLOUD.asset, new Vector2(i * Main.textureLibrary.OTHER_CLOUD.asset.Width, 0f), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            foreach(WorldHotspot hotspot in hotspots)
            {
                Main.spriteBatch.Draw(Main.textureLibrary.OTHER_HOTSPOT.asset, new Vector2(hotspot.x, hotspot.y), null, Color.White, 0f, new Vector2(Main.textureLibrary.OTHER_HOTSPOT.asset.Width, Main.textureLibrary.OTHER_HOTSPOT.asset.Height) / 2f, 1f, SpriteEffects.None, 1f);
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
                    int tx = x + ex - (ew / 2);
                    int ty = y + ey - eh;
                    WorldTile worldTile = GetTileAt(tx, ty, Tilemap.Solids);
                    WorldTileData worldTileData = GetTileDataAt(tx, ty, Tilemap.Solids);
                    if(ey == eh)
                    {
                        if(worldTile == null)
                        {
                            return false;
                        }
                        if(worldTileData.shape.fill != Shape.Fill.Rectangle)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if(worldTile != null)
                        {
                            return false;
                        }
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

        public static void AddHotspotAt(float x, float y, WorldHotspot.Spawn[] spawns, int count)
        {
            hotspots.Add(new WorldHotspot(x, y, spawns, count));
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
            while(UiManager.fadeElements[2].alpha < UiManager.fadeElements[2].alphaMax)
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
                                tilemaps[m][x, y].lighting = Main.save.tilemaps[m][x, y].lighting;
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
                    hotspots.Add(hotspot);
                }
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
                                if((Tilemap)m == Tilemap.Solids)
                                {
                                    SetTileLighting(x, y, (Tilemap)m);
                                }
                            }
                        }
                    }
                }
            }
            player = (PlayerCharacter)EntityManager.AddEntity<PlayerCharacter>(new Vector2((width * Tile.size) / 2f, 0f));
            Camera.positionTo = player.position;
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

        private static void SetTileLighting(int x, int y, Tilemap tilemap)
        {
            WorldTile worldTile = tilemaps[(byte)tilemap][x, y];
            int range = 255 / Lighting.jump;
            for(int r = 1; r <= range; r++)
            {
                bool foundEmpty = false;
                for(int ty = y - r; ty <= y + r; ty++)
                {
                    for(int tx = x - r; tx <= x + r; tx++)
                    {
                        if(GetTileAt(tx, ty, Tilemap.Solids) == null)
                        {
                            foundEmpty = true;
                            break;
                        }
                    }
                    if(foundEmpty)
                    {
                        break;
                    }
                }
                if(foundEmpty)
                {
                    break;
                }
                worldTile.lighting = (byte)MathUtilities.Clamp(r * Lighting.jump, 0f, 255f);
            }
        }
    }
}