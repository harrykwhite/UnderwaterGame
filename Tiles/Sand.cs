namespace UnderwaterGame.Tiles
{
    using Microsoft.Xna.Framework;

    public class Sand : Tile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_SAND.asset;
            textureBorder = new Color(142, 89, 89);
        }
    }
}