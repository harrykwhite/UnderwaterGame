namespace UnderwaterGame.Tiles.Solids
{
    using Microsoft.Xna.Framework;

    public class Brick : SolidTile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_SOLIDS_BRICK.asset;
            textureBorder = new Color(126, 110, 79);
        }
    }
}