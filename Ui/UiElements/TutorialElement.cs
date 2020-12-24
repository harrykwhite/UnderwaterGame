namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class TutorialElement : UiElement
    {
        public override void Draw()
        {
            DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text("Use the WASD keys to move the player\nUse the F key to change the hotbar\nUse the ESCAPE key to open the inventory"), UiManager.WorldToUi(World.playerSpawnPosition + new Vector2(0f, 6.5f * Tile.size)), Color.White, DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
        }

        public override void Init()
        {
        }

        public override void Update()
        {
        }
    }
}