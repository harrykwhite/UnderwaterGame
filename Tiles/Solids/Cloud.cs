namespace UnderwaterGame.Tiles.Solids
{
    using Microsoft.Xna.Framework;

    public class Cloud : SolidTile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_SOLIDS_CLOUD.asset;
            textureBorder = new Color(209, 209, 209);
        }
    }
}