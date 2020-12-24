namespace UnderwaterGame.Items.Edibles.Health
{
    using UnderwaterGame.Sprites;

    public class PinkJelly : HealthEdible
    {
        protected override void Init()
        {
            name = "Pink Jelly";
            sprite = Sprite.pinkJelly;
            stack = true;
            useTime = 10;
            useAngleUpdate = false;
            usePress = true;
            useHide = true;
            healthAmount = 1;
        }
    }
}