using Microsoft.Xna.Framework;

namespace UnderwaterGame.Tiles.Solids
{
    public class Brick : SolidTile
    {
        protected override void Init()
        {
            Texture = Main.TextureLibrary.TILES_SOLIDS_BRICK.Asset;
            TextureBorder = new Color(72, 96, 122);
        }
    }
}