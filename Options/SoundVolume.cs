namespace UnderwaterGame.Options
{
    public class SoundVolume : Option
    {
        protected override void Init()
        {
            name = "Sound Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}