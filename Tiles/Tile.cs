namespace UnderwaterGame.Tiles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract partial class Tile
    {
        public byte id;

        public Texture2D texture;

        public Color textureBorder;

        public Texture2D[] textures;

        public static int size = 8;

        public static int check = 4;

        public Tile()
        {
            tiles.Add(this);
            Init();
            textures = new Texture2D[20];
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            for(int i = 0; i < textures.Length; i++)
            {
                Texture2D newTexture = new Texture2D(Main.graphicsDevice, texture.Width, texture.Height);
                Color[] newTextureData = new Color[texture.Width * texture.Height];
                bool left = false;
                bool right = false;
                bool top = false;
                bool bottom = false;
                bool topLeftSlope = false;
                bool topRightSlope = false;
                bool bottomLeftSlope = false;
                bool bottomRightSlope = false;
                switch(i)
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
                for(int y = 0; y < texture.Height; y++)
                {
                    for(int x = 0; x < texture.Width; x++)
                    {
                        int index = (y * texture.Width) + x;
                        bool border = false;
                        bool cut = false;
                        border |= left && x == 0;
                        border |= right && x == texture.Width - 1;
                        border |= top && y == 0;
                        border |= bottom && y == texture.Height - 1;
                        cut |= topLeftSlope && (texture.Width - 1 - x) > y;
                        cut |= topRightSlope && x > y;
                        cut |= bottomLeftSlope && x < y;
                        cut |= bottomRightSlope && (texture.Width - 1 - x) < y;
                        border |= (topLeftSlope || bottomRightSlope) && (texture.Width - 1 - x) == y;
                        border |= (topRightSlope || bottomLeftSlope) && x == y;
                        newTextureData[index] = border ? textureBorder : (cut ? Color.Transparent : textureData[index]);
                    }
                }
                newTexture.SetData(newTextureData);
                Main.unloadTextures.Add(newTexture);
                textures[i] = newTexture;
            }
        }

        private static T Load<T>(byte id) where T : Tile
        {
            T tile = Activator.CreateInstance<T>();
            tile.id = id;
            return tile;
        }

        public static Tile GetTileById(byte id)
        {
            return tiles.Find((Tile tile) => tile.id == id);
        }

        protected abstract void Init();
    }
}