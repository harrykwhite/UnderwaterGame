namespace UnderwaterGame.Options.Audio
{
    public class MasterVolume : AudioOption
    {
        protected override void Init()
        {
            name = "Master Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}