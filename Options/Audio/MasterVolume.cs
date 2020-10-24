namespace UnderwaterGame.Options.Display
{
    public class MasterVolume : DisplayOption
    {
        protected override void Init()
        {
            Name = "Master Volume";

            value = 1f;
            ValueMax = 1f;
        }
    }
}