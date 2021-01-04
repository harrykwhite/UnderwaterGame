namespace UnderwaterGame.Options
{
    public class MasterVolume : Option
    {
        protected override void Init()
        {
            name = "Master Volume";
            value = 1f;
            valueMax = 1f;
        }
    }
}