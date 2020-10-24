namespace UnderwaterGame.Options.Display
{
    public class SoundVolume : DisplayOption
    {
        protected override void Init()
        {
            Name = "Sound Volume";

            value = 1f;
            ValueMax = 1f;
        }
    }
}