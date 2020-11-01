namespace UnderwaterGame.Utilities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class TextureUtilities
    {
        public static Vector2 TextureSize(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }

        public static Texture2D CreateFilled(Texture2D texture, Color color, bool alphaMatch = false)
        {
            Texture2D newTexture = new Texture2D(Main.graphicsDevice, texture.Width, texture.Height);
            Color[] newTextureData = new Color[texture.Width * texture.Height];
            texture.GetData(newTextureData);
            for(int i = 0; i < newTextureData.Length; i++)
            {
                byte alpha = newTextureData[i].A;
                if(alpha != 0)
                {
                    newTextureData[i] = color;
                    if(alphaMatch)
                    {
                        newTextureData[i].A = alpha;
                    }
                }
            }
            newTexture.SetData(newTextureData);
            return newTexture;
        }

        public static Texture2D CreateOutlined(Texture2D texture, Color color, bool resize = true)
        {
            if(resize)
            {
                texture = CreateResized(texture, texture.Width + 2, texture.Height + 2, true, true);
            }
            Texture2D outlinedTexture = new Texture2D(Main.graphicsDevice, texture.Width, texture.Height);
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            Color[,] outlinedTextureDataTwoDimensional = new Color[texture.Width, texture.Height];
            Color[,] textureDataTwoDimensional = GeneralUtilities.AsTwoDimensional(textureData, texture.Width, texture.Height);
            for(int y = 0; y < texture.Height; y++)
            {
                for(int x = 0; x < texture.Width; x++)
                {
                    bool left = false;
                    bool top = false;
                    bool right = false;
                    bool bottom = false;
                    if(x > 0)
                    {
                        left = textureDataTwoDimensional[x - 1, y].A > 0;
                    }
                    if(y > 0)
                    {
                        top = textureDataTwoDimensional[x, y - 1].A > 0;
                    }
                    if(x < texture.Width - 1)
                    {
                        right = textureDataTwoDimensional[x + 1, y].A > 0;
                    }
                    if(y < texture.Height - 1)
                    {
                        bottom = textureDataTwoDimensional[x, y + 1].A > 0;
                    }
                    if(left || top || right || bottom)
                    {
                        outlinedTextureDataTwoDimensional[x, y] = color;
                    }
                }
            }
            for(int y = 0; y < texture.Height; y++)
            {
                for(int x = 0; x < texture.Width; x++)
                {
                    if(textureDataTwoDimensional[x, y].A > 0)
                    {
                        outlinedTextureDataTwoDimensional[x, y] = textureDataTwoDimensional[x, y];
                    }
                }
            }
            outlinedTexture.SetData(GeneralUtilities.AsOneDimensional(outlinedTextureDataTwoDimensional));
            return outlinedTexture;
        }

        public static Texture2D CreateResized(Texture2D texture, int width, int height, bool left = false, bool top = false)
        {
            int xStart = 0, yStart = 0;
            if(left)
            {
                xStart += (width - texture.Width) / 2;
            }
            if(top)
            {
                yStart += (height - texture.Height) / 2;
            }
            Texture2D resizedTexture = new Texture2D(Main.graphicsDevice, width, height);
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            Color[,] resizedTextureDataTwoDimensional = new Color[width, height];
            Color[,] textureDataTwoDimensional = GeneralUtilities.AsTwoDimensional(textureData, texture.Width, texture.Height);
            for(int y = 0; y < height; y++)
            {
                int ry = y - yStart;
                if(ry < 0 || ry >= texture.Height)
                {
                    continue;
                }
                for(int x = 0; x < width; x++)
                {
                    int rx = x - xStart;
                    if(rx < 0 || rx >= texture.Width)
                    {
                        continue;
                    }
                    resizedTextureDataTwoDimensional[x, y] = textureDataTwoDimensional[rx, ry];
                }
            }
            resizedTexture.SetData(GeneralUtilities.AsOneDimensional(resizedTextureDataTwoDimensional));
            return resizedTexture;
        }

        public static Texture2D CreateResized(Texture2D texture, int xStart, int yStart, int width, int height)
        {
            Texture2D resizedTexture = new Texture2D(Main.graphicsDevice, width, height);
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            Color[,] resizedTextureDataTwoDimensional = new Color[width, height];
            Color[,] textureDataTwoDimensional = GeneralUtilities.AsTwoDimensional(textureData, texture.Width, texture.Height);
            for(int y = 0; y < height; y++)
            {
                int ry = y + yStart;
                if(ry < 0 || ry >= texture.Height)
                {
                    continue;
                }
                for(int x = 0; x < width; x++)
                {
                    int rx = x + xStart;
                    if(rx < 0 || rx >= texture.Width)
                    {
                        continue;
                    }
                    resizedTextureDataTwoDimensional[x, y] = textureDataTwoDimensional[rx, ry];
                }
            }
            resizedTexture.SetData(GeneralUtilities.AsOneDimensional(resizedTextureDataTwoDimensional));
            return resizedTexture;
        }

        public static Texture2D[] CreateSplit(Texture2D sheet, int frameWidth, int frameHeight)
        {
            int frameCount = sheet.Width / frameWidth;
            Texture2D[] frames = new Texture2D[frameCount];
            for(int i = 0; i < frameCount; i++)
            {
                int x = i * frameWidth;
                int y = 0;
                frames[i] = TextureUtilities.CreateResized(sheet, x, y, frameWidth, frameHeight);
            }
            return frames;
        }
    }
}