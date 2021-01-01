namespace UnderwaterGame.Tiles
{
    using Microsoft.Xna.Framework;

    public class Brick : Tile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_BRICK.asset;
            textureBorder = new Color(126, 110, 79);
        }
    }
}