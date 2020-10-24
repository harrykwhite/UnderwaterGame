using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnderwaterGame.Assets;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Sprites
{
    public partial class Sprite
    {
        private TextureLibrary.LibraryAsset libraryAsset;

        public Texture2D[] Textures { get; private set; } = new Texture2D[1];
        public Texture2D[] TexturesFilled { get; private set; } = new Texture2D[1];
        public Texture2D[] TexturesOutlined { get; private set; } = new Texture2D[1];

        public Rectangle Bound { get; private set; }
        public Vector2 Origin { get; private set; }
        public Shape Shape { get; private set; }

        public int Width => Textures[0].Width;
        public int Height => Textures[0].Height;

        public Sprite()
        {
            Sprites.Add(this);
        }

        private void SetTexture(Texture2D texture, int index = 0)
        {
            Textures[index] = texture;
            TexturesFilled[index] = TextureUtilities.CreateFilled(texture, Color.White, true);
            TexturesOutlined[index] = TextureUtilities.CreateOutlined(texture, Color.White, true);
        }

        private void SetTextureSheet(Texture2D textureSheet, int frameWidth)
        {
            Textures = TextureUtilities.CreateSplit(textureSheet, frameWidth, textureSheet.Height);
            TexturesFilled = new Texture2D[Textures.Length];
            TexturesOutlined = new Texture2D[Textures.Length];

            for (int i = 0; i < Textures.Length; i++)
            {
                TexturesFilled[i] = TextureUtilities.CreateFilled(Textures[i], Color.White, true);
                TexturesOutlined[i] = TextureUtilities.CreateOutlined(Textures[i], Color.White, true);
            }
        }
    }
}