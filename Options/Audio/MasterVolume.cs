namespace UnderwaterGame.Options.Display
{
    public class MasterVolume : DisplayOption
    {
        protected override void Init()
        {
            name = "Master Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}