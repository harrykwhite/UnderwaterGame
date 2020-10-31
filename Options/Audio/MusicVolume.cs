namespace UnderwaterGame.Options.Display
{
    public class MusicVolume : DisplayOption
    {
        protected override void Init()
        {
            name = "Music Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}