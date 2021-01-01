namespace UnderwaterGame.Tiles
{
    using Microsoft.Xna.Framework;

    public class Stone : Tile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_STONE.asset;
            textureBorder = new Color(83, 86, 117);
        }
    }
}