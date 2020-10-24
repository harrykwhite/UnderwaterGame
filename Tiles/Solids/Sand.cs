using Microsoft.Xna.Framework;

namespace UnderwaterGame.Tiles.Solids
{
    public class Sand : SolidTile
    {
        protected override void Init()
        {
            Texture = Main.TextureLibrary.TILES_SOLIDS_SAND.Asset;
            TextureBorder = new Color(142, 89, 89);
        }
    }
}