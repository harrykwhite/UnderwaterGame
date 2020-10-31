namespace UnderwaterGame.Options.Display
{
    public class SoundVolume : DisplayOption
    {
        protected override void Init()
        {
            name = "Sound Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}