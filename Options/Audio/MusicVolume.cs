namespace UnderwaterGame.Options.Display
{
    public class MusicVolume : DisplayOption
    {
        protected override void Init()
        {
            Name = "Music Volume";

            value = 1f;
            ValueMax = 1f;
        }
    }
}