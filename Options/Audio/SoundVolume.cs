namespace UnderwaterGame.Options.Audio
{
    public class SoundVolume : AudioOption
    {
        protected override void Init()
        {
            name = "Sound Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}