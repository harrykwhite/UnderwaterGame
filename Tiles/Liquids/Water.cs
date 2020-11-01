using Microsoft.Xna.Framework;

namespace UnderwaterGame.Tiles.Liquids
{
    public class Water : LiquidTile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_LIQUIDS_WATER.asset;
            textureBorder = new Color(16, 103, 153);
            alpha = 0.2f;
        }
    }
}