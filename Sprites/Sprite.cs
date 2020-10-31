namespace UnderwaterGame.Sprites
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using UnderwaterGame.Assets;
    using UnderwaterGame.Utilities;

    public partial class Sprite
    {
        private TextureLibrary.LibraryAsset libraryAsset;

        public Texture2D[] textures = new Texture2D[1];

        public Texture2D[] texturesFilled = new Texture2D[1];

        public Texture2D[] texturesOutlined = new Texture2D[1];

        public Rectangle bound;

        public Vector2 origin;

        public Shape shape;

        public Sprite()
        {
            sprites.Add(this);
        }

        private void SetTexture(Texture2D texture, int index = 0)
        {
            textures[index] = texture;
            texturesFilled[index] = TextureUtilities.CreateFilled(texture, Color.White, true);
            texturesOutlined[index] = TextureUtilities.CreateOutlined(texture, Color.White, true);
        }

        private void SetTextureSheet(Texture2D textureSheet, int frameWidth)
        {
            textures = TextureUtilities.CreateSplit(textureSheet, frameWidth, textureSheet.Height);
            texturesFilled = new Texture2D[textures.Length];
            texturesOutlined = new Texture2D[textures.Length];
            for(int i = 0; i < textures.Length; i++)
            {
                texturesFilled[i] = TextureUtilities.CreateFilled(textures[i], Color.White, true);
                texturesOutlined[i] = TextureUtilities.CreateOutlined(textures[i], Color.White, true);
            }
        }
    }
}