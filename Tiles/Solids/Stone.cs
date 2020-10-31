namespace UnderwaterGame.Tiles.Solids
{
    using Microsoft.Xna.Framework;

    public class Stone : SolidTile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_SOLIDS_STONE.asset;
            textureBorder = new Color(83, 86, 117);
        }
    }
}