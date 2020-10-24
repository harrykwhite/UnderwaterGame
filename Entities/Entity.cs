﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UnderwaterGame.Entities.Particles;
using UnderwaterGame.Sprites;
using UnderwaterGame.Tiles;
using UnderwaterGame.Utilities;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Entities
{
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
        public int gravityWaterTimeMax = 4;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 scale = Vector2.One;

        public Sprite Sprite { get; private set; }
        public Animator Animator { get; protected set; }
        public Collider Collider { get; protected set; }

        public bool InWater { get; private set; }
        public bool InWaterPrevious { get; private set; }

        public SpriteEffects Flip => (flipHor ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (flipVer ? SpriteEffects.FlipVertically : SpriteEffects.None);

        public Entity()
        {
            Collider = new Collider(new Shape(Shape.Fill.Rectangle, 16, 16), this);
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
            if (!EntityManager.Entities.Contains(this))
            {
                return;
            }

            EntityManager.Entities.Remove(this);
        }

        public virtual void DrawSelf(Texture2D texture = null, Vector2? position = null, Rectangle? sourceRectangle = null, Color? color = null, float? rotation = null, Vector2? origin = null, Vector2? scale = null, SpriteEffects? flip = null, float? depth = null)
        {
            if (texture == null)
            {
                if (Animator != null)
                {
                    texture = Animator.sprite.Textures[(int)Animator.index];
                }
                else
                {
                    texture = Sprite.Textures[0];
                }
            }

            if (position == null)
            {
                position = this.position;
            }

            if (color == null)
            {
                color = blend * alpha;
            }

            if (rotation == null)
            {
                rotation = angle;
            }

            if (origin == null)
            {
                origin = Sprite.Origin;
            }

            if (scale == null)
            {
                scale = this.scale;
            }

            if (flip == null)
            {
                flip = Flip;
            }

            if (depth == null)
            {
                depth = this.depth;
            }

            Main.SpriteBatch.Draw(texture, position.Value, sourceRectangle, color.Value, rotation.Value, origin.Value, scale.Value, flip.Value, depth.Value);
        }

        public void SetSprite(Sprite sprite)
        {
            Sprite = sprite;
            Collider.shape = sprite.Shape;
        }

        public bool GetExists()
        {
            return EntityManager.Entities.Contains(this);
        }

        public Point GetTilePosition(Vector2 offset)
        {
            Point tilePosition = ((position + offset) / Tile.Size).ToPoint();

            tilePosition.X = (int)MathUtilities.Clamp(tilePosition.X, 0f, Main.World.Width - 1f);
            tilePosition.Y = (int)MathUtilities.Clamp(tilePosition.Y, 0f, Main.World.Height - 1f);

            return tilePosition;
        }

        protected void UpdateGravity(bool endless = false)
        {
            bool fall = true;
            float acc = Main.World.BaseGravityAcc;

            if (TileIn(position, Tile.Water, World.TilemapType.Liquids))
            {
                if (gravityWaterTime > 0)
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

            if (fall)
            {
                gravity += acc;
            }
            else
            {
                if (!endless)
                {
                    if (gravity < 0f)
                    {
                        gravity += Math.Min(acc, -gravity);
                    }
                    else if (gravity > 0f)
                    {
                        gravity -= Math.Min(acc, gravity);
                    }
                }
            }
        }

        protected void UpdateWater()
        {
            InWaterPrevious = InWater;
            InWater = TileIn(position, Tile.Water, World.TilemapType.Liquids);

            if (life > 0)
            {
                if (InWater ^ InWaterPrevious)
                {
                    int liquidParticleCount = 3;

                    for (int i = 0; i < liquidParticleCount; i++)
                    {
                        Liquid liquid = (Liquid)EntityManager.AddEntity<Liquid>(position);
                        liquid.position = position;
                        liquid.direction = (-MathHelper.Pi / 2f) + ((MathHelper.Pi / 18f) * (i - ((liquidParticleCount - 1f) / 2f)));
                        liquid.blend = new Color(46, 130, 178);
                    }
                }
            }
        }

        public bool InWorld(Vector2 at, bool hor = true, bool ver = true)
        {
            bool inHor = at.X >= 0f && at.X <= Main.World.RealWidth;
            bool inVer = at.Y >= 0f && at.Y <= Main.World.RealHeight;

            return hor ^ ver ? (hor && inHor) || (ver && inVer) : inHor && inVer;
        }

        protected void LockInWorld()
        {
            position.X = MathUtilities.Clamp(position.X, 0f, Main.World.RealWidth);
            position.Y = MathUtilities.Clamp(position.Y, 0f, Main.World.RealHeight);
        }

        public bool TileIn(Vector2 at, Tile tileType, World.TilemapType tilemapType, Predicate<WorldTileData> predicate = null)
        {
            Point tilePosition = GetTilePosition(Vector2.Zero);
            return Main.World.GetTileDataAt(tilePosition.X, tilePosition.Y, tilemapType, (WorldTileData tileData) => tileData.WorldTile.TileType == tileType) != null;
        }

        public bool TileCollision(Vector2 at, World.TilemapType tilemapType, Predicate<WorldTileData> predicate = null)
        {
            Point tileAt = (at / Tile.Size).ToPoint();
            List<WorldTileData> worldTileData = Main.World.GetTileDataRange(tileAt.X, tileAt.Y, tilemapType, Tile.Check, predicate);

            foreach (WorldTileData data in worldTileData)
            {
                if (Collider.GetRelative(at).Intersects(data.Shape))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TileTypeCollision(Vector2 at, Tile tileType, World.TilemapType tilemapType, Predicate<WorldTileData> predicate = null)
        {
            Point tileAt = (at / Tile.Size).ToPoint();
            List<WorldTileData> worldTileData = Main.World.GetTileDataRange(tileAt.X, tileAt.Y, tilemapType, Tile.Check, (WorldTileData tileData) => tileData.WorldTile.TileType == tileType && predicate.Invoke(tileData));

            foreach (WorldTileData data in worldTileData)
            {
                if (Collider.GetRelative(at).Intersects(data.Shape))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TileCollisionLine(Vector2 a, Vector2 b, World.TilemapType tilemapType, Predicate<WorldTileData> predicate = null)
        {
            float direction = MathUtilities.PointDirection(a, b);
            Vector2 at = a;

            while (true)
            {
                Point tileAt = (at / Tile.Size).ToPoint();
                List<WorldTileData> worldTileData = Main.World.GetTileDataRange(tileAt.X, tileAt.Y, tilemapType, Tile.Check, predicate);

                foreach (WorldTileData data in worldTileData)
                {
                    if (Collider.GetRelative(at).Intersects(data.Shape))
                    {
                        return true;
                    }
                }

                if (Vector2.Distance(at, b) < 1f)
                {
                    break;
                }

                at += MathUtilities.LengthDirection(Math.Min(1f, Vector2.Distance(at, b)), direction);
            }

            return false;
        }

        public bool TileTypeCollisionLine(Vector2 a, Vector2 b, Tile tileType, World.TilemapType tilemapType, Predicate<WorldTileData> predicate = null)
        {
            float direction = MathUtilities.PointDirection(a, b);
            Vector2 at = a;

            while (true)
            {
                Point tileAt = (at / Tile.Size).ToPoint();
                List<WorldTileData> worldTileData = Main.World.GetTileDataRange(tileAt.X, tileAt.Y, tilemapType, Tile.Check, (WorldTileData tileData) => tileData.WorldTile.TileType == tileType && predicate.Invoke(tileData));

                foreach (WorldTileData data in worldTileData)
                {
                    if (Collider.GetRelative(at).Intersects(data.Shape))
                    {
                        return true;
                    }
                }

                if (at == b)
                {
                    break;
                }

                at += MathUtilities.LengthDirection(Math.Min(1f, Vector2.Distance(at, b)), direction);
            }

            return false;
        }

        protected void TileCollisions(Vector2 offset)
        {
            Point tilePosition = GetTilePosition(Vector2.Zero);
            List<WorldTileData> worldTileData = Main.World.GetTileDataRange(tilePosition.X, tilePosition.Y, World.TilemapType.Solids, Math.Max(Tile.Check, (int)velocity.Length() / Tile.Size));

            foreach (WorldTileData data in worldTileData)
            {
                if (Collider.GetRelative(position + new Vector2(velocity.X, 0f)).Intersects(data.Shape))
                {
                    TileCollisionsTowards(new Vector2(velocity.X >= 0f ? 1f : -1f, 0f), Math.Abs(velocity.X));
                    velocity.X = 0f;
                    break;
                }
            }

            foreach (WorldTileData data in worldTileData)
            {
                if (Collider.GetRelative(position + new Vector2(0f, velocity.Y)).Intersects(data.Shape))
                {
                    TileCollisionsTowards(new Vector2(0f, velocity.Y >= 0f ? 1f : -1f), Math.Abs(velocity.Y));
                    velocity.Y = 0f;
                    break;
                }
            }

            foreach (WorldTileData data in worldTileData)
            {
                if (Collider.GetRelative(position + velocity).Intersects(data.Shape))
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

            while (!TileCollision() && length < distance)
            {
                position += normal * lengthAcc;
                length += lengthAcc;
            }

            bool TileCollision()
            {
                Point tilePosition = GetTilePosition(Vector2.Zero);
                List<WorldTileData> worldTileData = Main.World.GetTileDataRange(tilePosition.X, tilePosition.Y, World.TilemapType.Solids, Math.Max(Tile.Check, (int)velocity.Length() / Tile.Size));

                foreach (WorldTileData data in worldTileData)
                {
                    if (Collider.GetRelative(position + (normal * lengthAcc)).Intersects(data.Shape))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}