using Microsoft.Xna.Framework;

namespace UnderwaterGame.Tiles.Solids
{
    public class Stone : SolidTile
    {
        protected override void Init()
        {
            Texture = Main.TextureLibrary.TILES_SOLIDS_STONE.Asset;
            TextureBorder = new Color(83, 86, 117);
        }
    }
}