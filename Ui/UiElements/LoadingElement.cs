namespace UnderwaterGame.Ui.UiElements
{
    using Microsoft.Xna.Framework;
    using UnderwaterGame.Utilities;

    public class LoadingElement : UiElement
    {
        public int ellipsisCount;

        public int ellipsisCountMax = 3;

        public int ellipsisTime;

        public int ellipsisTimeMax = 30;

        public override void Draw()
        {
            string text = "Loading";
            for(int i = 0; i < ellipsisCount; i++)
            {
                text += ".";
            }
            DrawUtilities.DrawString(Main.fontLibrary.ARIALMEDIUM.asset, new DrawUtilities.Text(text), new Vector2(Main.GetBufferWidth(), Main.GetBufferHeight()) / 2f, Color.White * (UiManager.fadeElements[3]?.alpha ?? 0f), DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            if(Main.loading == null && (UiManager.fadeElements[3]?.alpha ?? 0f) <= 0f)
            {
                ellipsisCount = 0;
                ellipsisTime = 0;
            }
            else
            {
                if(ellipsisTime < ellipsisTimeMax)
                {
                    ellipsisTime++;
                }
                else
                {
                    if(ellipsisCount < ellipsisCountMax)
                    {
                        ellipsisCount++;
                    }
                    else
                    {
                        ellipsisCount = 0;
                    }
                    ellipsisTime = 0;
                }
            }
        }
    }
}