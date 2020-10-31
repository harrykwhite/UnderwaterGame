namespace UnderwaterGame.Tiles.Liquids
{
    public class Water : LiquidTile
    {
        protected override void Init()
        {
            texture = Main.textureLibrary.TILES_LIQUIDS_WATER.asset;
            alpha = 0.3f;
        }
    }
}