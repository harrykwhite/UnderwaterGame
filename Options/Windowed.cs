namespace UnderwaterGame.Options
{
    public class Windowed : Option
    {
        protected override void Init()
        {
            name = "Windowed";
            value = 1f;
            valueMax = 1f;
            valueRounded = true;
            valueFormat = Format.Toggle;
        }
    }
}