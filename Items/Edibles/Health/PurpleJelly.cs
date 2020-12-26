namespace UnderwaterGame.Items.Edibles.Health
{
    using UnderwaterGame.Sprites;

    public class PurpleJelly : HealthEdible
    {
        protected override void Init()
        {
            name = "Purple Jelly";
            sprite = Sprite.purpleJelly;
            stack = true;
            useTime = 5;
            useAngleUpdate = false;
            usePress = true;
            useHide = true;
            healthAmount = 1;
        }
    }
}