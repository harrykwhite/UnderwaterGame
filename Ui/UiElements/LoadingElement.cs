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
            DrawUtilities.DrawString(Main.fontLibrary.ARIALSMALL.asset, new DrawUtilities.Text(text), UiManager.GetSize() / 2f, Color.White * UiManager.fadeElements[2].alpha, DrawUtilities.HorizontalAlign.Middle, DrawUtilities.VerticalAlign.Middle);
        }

        public override void Init()
        {
        }

        public override void Update()
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