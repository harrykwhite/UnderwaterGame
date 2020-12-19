namespace UnderwaterGame.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Entities.Particles;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public abstract class Entity
    {
        public float angle;

        public float angleOffset;

        public Color blend = Color.White;

        public float depth = 0.5f;

        public float alpha = 1f;

        public long life;

        public bool flipHor;

        public bool flipVer;

        public float gravity;

        public int gravityWaterTime;

        public int gravityWaterTimeMax = 5;

        public Vector2 position;

        public Vector2 velocity;

        public Vector2 scale = Vector2.One;

        public Sprite sprite;

        public Animator animator;

        public Collider collider;

        public bool inWater;

        public bool inWaterPrevious;

        public Entity()
        {
        }

        public abstract void Init();

        public abstract void Update();

        public abstract void Draw();

        public virtual void BeginUpdate()
        {
        }

        public virtual void EndUpdate()
        {
        }

        public virtual void BeginDraw()
        {
        }

        public virtual void EndDraw()
        {
        }

        public virtual void Destroy()
        {
            if(!EntityManager.entities.Contains(this))
            {
                return;
            }
            EntityManager.entities.Remove(this);
        }

        public virtual void DrawSelf(Texture2D texture = null, Vector2? position = null, Rectangle? sourceRectangle = null, Color? color = null, float? rotation = null, Vector2? origin = null, Vector2? scale = null, SpriteEffects? flip = null, float? depth = null)
        {
            if(texture == null)
            {
                if(animator != null)
                {
                    texture = animator.sprite.textures[(int)animator.index];
                }
                else
                {
                    texture = sprite.textures[0];
                }
            }
            if(position == null)
            {
                position = this.position;
            }
            if(color == null)
            {
                color = blend * alpha;
            }
            if(rotation == null)
            {
                rotation = angle;
            }
            if(origin == null)
            {
                origin = sprite.origin;
            }
            if(scale == null)
            {
                scale = this.scale;
            }
            if(flip == null)
            {
                flip = GetFlip();
            }
            if(depth == null)
            {
                depth = this.depth;
            }
            Main.spriteBatch.Draw(texture, position.Value, sourceRectangle, color.Value, rotation.Value, origin.Value, scale.Value, flip.Value, depth.Value);
        }

        public void SetSprite(Sprite sprite, bool collider)
        {
            this.sprite = sprite;
            if(collider)
            {
                this.collider = new Collider(sprite.shape, this);
            }
        }

        public bool GetExists()
        {
            return EntityManager.entities.Contains(this);
        }

        public Point GetTilePosition(Vector2 offset)
        {
            Point tilePosition = ((position + offset) / Tile.size).ToPoint();
            tilePosition.X = (int)MathUtilities.Clamp(tilePosition.X, 0f, World.width - 1f);
            tilePosition.Y = (int)MathUtilities.Clamp(tilePosition.Y, 0f, World.height - 1f);
            return tilePosition;
        }

        protected void UpdateGravity(bool endless = false)
        {
            bool fall = true;
            float acc = World.gravityAcc;
            if(TileIn(Tile.water, World.Tilemap.Liquids))
            {
                if(gravityWaterTime > 0)
                {
                    gravityWaterTime--;
                }
                else
                {
                    fall = false;
                    acc *= 2f;
                }
            }
            else
            {
                gravityWaterTime = gravityWaterTimeMax;
            }
            if(fall)
            {
                gravity += acc;
            }
            else
            {
                if(!endless)
                {
                    if(gravity < 0f)
                    {
                        gravity += Math.Min(acc, -gravity);
                    }
                    else if(gravity > 0f)
                    {
                        gravity -= Math.Min(acc, gravity);
                    }
                }
            }
        }

        protected void UpdateWater()
        {
            inWaterPrevious = inWater;
            inWater = TileIn(Tile.water, World.Tilemap.Liquids);
            if(life > 0)
            {
                if(inWater ^ inWaterPrevious)
                {
                    int liquidParticleCount = 3;
                    for(int i = 0; i < liquidParticleCount; i++)
                    {
                        Liquid liquid = (Liquid)EntityManager.AddEntity<Liquid>(position);
                        liquid.position = position;
                        liquid.direction = (-MathHelper.Pi / 2f) + ((MathHelper.Pi / 18f) * (i - ((liquidParticleCount - 1f) / 2f)));
                        liquid.blend = new Color(16, 103, 153);
                    }
                }
            }
        }

        public bool InWorld()
        {
            return position.X >= 0f && position.Y >= 0f && position.X <= World.width * Tile.size && position.Y <= World.height * Tile.size;
        }

        protected void LockInWorld()
        {
            position.X = MathUtilities.Clamp(position.X, 0f, World.width * Tile.size);
            position.Y = MathUtilities.Clamp(position.Y, 0f, World.height * Tile.size);
        }

        public bool TileIn(Tile tileType, World.Tilemap tilemap)
        {
            Point tilePosition = GetTilePosition(Vector2.Zero);
            return World.GetTileDataAt(tilePosition.X, tilePosition.Y, tilemap, (WorldTileData tileData) => Tile.GetTileById(tileData.worldTile.id) == tileType) != null;
        }

        public bool TileCollision(Vector2 at, World.Tilemap tilemap, Predicate<WorldTileData> predicate = null)
        {
            Point tileAt = (at / Tile.size).ToPoint();
            List<WorldTileData> worldTileData = World.GetTileDataRange(tileAt.X, tileAt.Y, tilemap, Tile.check, predicate);
            foreach(WorldTileData data in worldTileData)
            {
                if(collider.GetRelative(at).Intersects(data.shape))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TileTypeCollision(Vector2 at, Tile tileType, World.Tilemap tilemap)
        {
            Point tileAt = (at / Tile.size).ToPoint();
            List<WorldTileData> worldTileData = World.GetTileDataRange(tileAt.X, tileAt.Y, tilemap, Tile.check, (WorldTileData tileData) => Tile.GetTileById(tileData.worldTile.id) == tileType);
            foreach(WorldTileData data in worldTileData)
            {
                if(collider.GetRelative(at).Intersects(data.shape))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TileCollisionLine(Vector2 a, Vector2 b, World.Tilemap tilemap)
        {
            float direction = MathUtilities.PointDirection(a, b);
            Vector2 at = a;
            while(true)
            {
                Point tileAt = (at / Tile.size).ToPoint();
                List<WorldTileData> worldTileData = World.GetTileDataRange(tileAt.X, tileAt.Y, tilemap, Tile.check);
                foreach(WorldTileData data in worldTileData)
                {
                    if(collider.GetRelative(at).Intersects(data.shape))
                    {
                        return true;
                    }
                }
                if(Vector2.Distance(at, b) < 1f)
                {
                    break;
                }
                at += MathUtilities.LengthDirection(Math.Min(1f, Vector2.Distance(at, b)), direction);
            }
            return false;
        }

        public bool TileTypeCollisionLine(Vector2 a, Vector2 b, Tile tileType, World.Tilemap tilemap)
        {
            float direction = MathUtilities.PointDirection(a, b);
            Vector2 at = a;
            while(true)
            {
                Point tileAt = (at / Tile.size).ToPoint();
                List<WorldTileData> worldTileData = World.GetTileDataRange(tileAt.X, tileAt.Y, tilemap, Tile.check, (WorldTileData tileData) => Tile.GetTileById(tileData.worldTile.id) == tileType);
                foreach(WorldTileData data in worldTileData)
                {
                    if(collider.GetRelative(at).Intersects(data.shape))
                    {
                        return true;
                    }
                }
                if(at == b)
                {
                    break;
                }
                at += MathUtilities.LengthDirection(Math.Min(1f, Vector2.Distance(at, b)), direction);
            }
            return false;
        }

        protected void TileCollisions()
        {
            Point tilePosition = GetTilePosition(Vector2.Zero);
            List<WorldTileData> worldTileData = World.GetTileDataRange(tilePosition.X, tilePosition.Y, World.Tilemap.Solids, Math.Max(Tile.check, (int)velocity.Length() / Tile.size));
            foreach(WorldTileData data in worldTileData)
            {
                if(collider.GetRelative(position + new Vector2(velocity.X, 0f)).Intersects(data.shape))
                {
                    TileCollisionsTowards(new Vector2(velocity.X >= 0f ? 1f : -1f, 0f), Math.Abs(velocity.X));
                    velocity.X = 0f;
                    break;
                }
            }
            foreach(WorldTileData data in worldTileData)
            {
                if(collider.GetRelative(position + new Vector2(0f, velocity.Y)).Intersects(data.shape))
                {
                    TileCollisionsTowards(new Vector2(0f, velocity.Y >= 0f ? 1f : -1f), Math.Abs(velocity.Y));
                    velocity.Y = 0f;
                    break;
                }
            }
            foreach(WorldTileData data in worldTileData)
            {
                if(collider.GetRelative(position + velocity).Intersects(data.shape))
                {
                    velocity.X = 0f;
                    break;
                }
            }
        }

        private void TileCollisionsTowards(Vector2 normal, float distance)
        {
            float length = 0f;
            float lengthAcc = 0.1f;
            while(!TileCollision() && length < distance)
            {
                position += normal * lengthAcc;
                length += lengthAcc;
            }
            bool TileCollision()
            {
                Point tilePosition = GetTilePosition(Vector2.Zero);
                List<WorldTileData> worldTileData = World.GetTileDataRange(tilePosition.X, tilePosition.Y, World.Tilemap.Solids, Math.Max(Tile.check, (int)velocity.Length() / Tile.size));
                foreach(WorldTileData data in worldTileData)
                {
                    if(collider.GetRelative(position + (normal * lengthAcc)).Intersects(data.shape))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public SpriteEffects GetFlip()
        {
            return (flipHor ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (flipVer ? SpriteEffects.FlipVertically : SpriteEffects.None);
        }
    }
}