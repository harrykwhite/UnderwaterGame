namespace UnderwaterGame.Options.Display
{
    public class Fullscreen : DisplayOption
    {
        protected override void Init()
        {
            Name = "Fullscreen";

            ValueMax = 1f;
            ValueRounded = true;
            ValueFormat = Format.Toggle;
        }
    }
}