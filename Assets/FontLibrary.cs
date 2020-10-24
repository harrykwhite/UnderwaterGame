using Microsoft.Xna.Framework.Graphics;

namespace UnderwaterGame.Assets
{
    public class FontLibrary : Library<SpriteFont>
    {
        public readonly LibraryAsset ARIALVERYSMALL = new LibraryAsset("Fonts/ArialVerySmall");
        public readonly LibraryAsset ARIALSMALL = new LibraryAsset("Fonts/ArialSmall");
        public readonly LibraryAsset ARIALMEDIUM = new LibraryAsset("Fonts/ArialMedium");
        public readonly LibraryAsset ARIALLARGE = new LibraryAsset("Fonts/ArialLarge");
        public readonly LibraryAsset ARIALVERYLARGE = new LibraryAsset("Fonts/ArialVeryLarge");
    }
}