namespace UnderwaterGame.Options.Audio
{
    public class MusicVolume : AudioOption
    {
        protected override void Init()
        {
            name = "Music Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}