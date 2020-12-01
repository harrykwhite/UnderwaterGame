namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public class HotspotCountElement : UiElement
    {
        public override void Draw()
        {
            foreach(Hotspot hotspot in World.hotspots)
            {
                DrawUtilities.DrawStringExt(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(hotspot.count.ToString() + " enemies"), UiManager.WorldToUi(hotspot.position), Color.White * hotspot.alpha, 0f, hotspot.countScale, DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
            }
        }

        public override void Init()
        {
        }

        public override void Update()
        {
        }
    }
}