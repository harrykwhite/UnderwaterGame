namespace UnderwaterGame.Tiles.Liquids
{
    public class Water : LiquidTile
    {
        protected override void Init()
        {
            Texture = Main.TextureLibrary.TILES_LIQUIDS_WATER.Asset;
            Alpha = 0.4f;
        }
    }
}