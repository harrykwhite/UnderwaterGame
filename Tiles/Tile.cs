using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UnderwaterGame.Tiles.Liquids;
using UnderwaterGame.Tiles.Solids;
using UnderwaterGame.Tiles.Walls;
using UnderwaterGame.Worlds;

namespace UnderwaterGame.Tiles
{
    public abstract partial class Tile
    {
        public byte id;

        public Texture2D Texture { get; protected set; }
        public Color TextureBorder { get; protected set; }

        public Texture2D[] Textures { get; protected set; }

        public float Alpha { get; protected set; } = 1f;

        public static int Size => 8;
        public static int Check => 4;

        public bool IsSolid => this is SolidTile;
        public bool IsWall => this is WallTile;
        public bool IsLiquid => this is LiquidTile;

        public Tile()
        {
            Tiles.Add(this);

            Init();
            SetTextures();
        }

        private static T Load<T>(byte id) where T : Tile
        {
            T tile = Activator.CreateInstance<T>();
            tile.id = id;

            return tile;
        }

        public static Tile GetTileByID(byte id)
        {
            return Tiles.Find((Tile tile) => tile.id == id);
        }

        protected abstract void Init();

        public virtual void SetTextures()
        {
            World.TilemapType tilemapType = GetTilemapType();
            int tilemapCount = GetTilemapCount();

            Textures = new Texture2D[tilemapCount];

            Color[] textureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(textureData);

            for (int i = 0; i < tilemapCount; i++)
            {
                Texture2D newTexture = new Texture2D(Main.GraphicsDeviceCurrent, Texture.Width, Texture.Height);
                Color[] newTextureData = new Color[Texture.Width * Texture.Height];

                bool left = false;
                bool right = false;
                bool top = false;
                bool bottom = false;

                bool topLeftSlope = false;
                bool topRightSlope = false;
                bool bottomLeftSlope = false;
                bool bottomRightSlope = false;

                switch (tilemapType)
                {
                    default:
                        switch (i)
                        {
                            case 1:
                                left = true;
                                right = true;
                                top = true;
                                bottom = true;
                                break;

                            case 2:
                                left = true;
                                right = true;
                                top = true;
                                break;

                            case 3:
                                left = true;
                                right = true;
                                break;

                            case 4:
                                left = true;
                                right = true;
                                bottom = true;
                                break;

                            case 5:
                                left = true;
                                top = true;
                                bottom = true;
                                break;

                            case 6:
                                top = true;
                                bottom = true;
                                break;

                            case 7:
                                right = true;
                                top = true;
                                bottom = true;
                                break;

                            case 8:
                                left = true;
                                top = true;
                                break;

                            case 9:
                                top = true;
                                break;

                            case 10:
                                right = true;
                                top = true;
                                break;

                            case 11:
                                left = true;
                                break;

                            case 12:
                                right = true;
                                break;

                            case 13:
                                left = true;
                                bottom = true;
                                break;

                            case 14:
                                bottom = true;
                                break;

                            case 15:
                                right = true;
                                bottom = true;
                                break;

                            case 16:
                                topLeftSlope = true;
                                break;

                            case 17:
                                topRightSlope = true;
                                break;

                            case 18:
                                bottomLeftSlope = true;
                                break;

                            case 19:
                                bottomRightSlope = true;
                                break;
                        }
                        break;
                }

                for (int y = 0; y < Texture.Height; y++)
                {
                    for (int x = 0; x < Texture.Width; x++)
                    {
                        int index = (y * Texture.Width) + x;

                        bool border = false;
                        bool cut = false;

                        border |= left && x == 0;
                        border |= right && x == Texture.Width - 1;
                        border |= top && y == 0;
                        border |= bottom && y == Texture.Height - 1;

                        cut |= topLeftSlope && (Texture.Width - 1 - x) > y;
                        cut |= topRightSlope && x > y;
                        cut |= bottomLeftSlope && x < y;
                        cut |= bottomRightSlope && (Texture.Width - 1 - x) < y;

                        border |= (topLeftSlope || bottomRightSlope) && (Texture.Width - 1 - x) == y;
                        border |= (topRightSlope || bottomLeftSlope) && x == y;

                        newTextureData[index] = border ? TextureBorder : (cut ? Color.Transparent : textureData[index]);
                    }
                }

                newTexture.SetData(newTextureData);
                Textures[i] = newTexture;
            }
        }

        public virtual float GetTilemapDepth()
        {
            World.TilemapType tilemapType = GetTilemapType();

            switch (tilemapType)
            {
                case World.TilemapType.Walls:
                    return 0.25f;

                case World.TilemapType.Liquids:
                    return 0.75f;

                default:
                    return 0.65f;
            }
        }

        public virtual Color GetTilemapColor()
        {
            World.TilemapType tilemapType = GetTilemapType();

            switch (tilemapType)
            {
                case World.TilemapType.Walls:
                    return new Color(80, 80, 80);

                default:
                    return Color.White;
            }
        }

        public virtual int GetTilemapCount()
        {
            World.TilemapType tilemapType = GetTilemapType();

            switch (tilemapType)
            {
                case World.TilemapType.Walls:
                    return 0;

                case World.TilemapType.Liquids:
                    return 1;

                default:
                    return 20;
            }
        }

        public virtual World.TilemapType GetTilemapType()
        {
            if (IsWall)
            {
                return World.TilemapType.Walls;
            }

            if (IsLiquid)
            {
                return World.TilemapType.Liquids;
            }

            return World.TilemapType.Solids;
        }
    }
}