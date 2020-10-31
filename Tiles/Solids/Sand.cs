namespace UnderwaterGame.Tiles.Solids
{
    using Microsoft.Xna.Framework;

    public class Sand : SolidTile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_SOLIDS_SAND.asset;
            textureBorder = new Color(142, 89, 89);
        }
    }
}