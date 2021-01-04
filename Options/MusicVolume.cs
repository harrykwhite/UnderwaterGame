namespace UnderwaterGame.Options
{
    public class MusicVolume : Option
    {
        protected override void Init()
        {
            name = "Music Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}