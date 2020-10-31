namespace UnderwaterGame.Options.Display
{
    public class Fullscreen : DisplayOption
    {
        protected override void Init()
        {
            name = "Fullscreen";
            valueMax = 1f;
            valueRounded = true;
            valueFormat = Format.Toggle;
        }
    }
}