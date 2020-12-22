namespace UnderwaterGame.Items.Edibles.Health
{
    using UnderwaterGame.Sprites;

    public class Jelly : HealthEdible
    {
        protected override void Init()
        {
            name = "Jelly";
            sprite = Sprite.jelly;
            stack = true;
            useTime = 10;
            useAngleUpdate = false;
            usePress = true;
            useHide = true;
            healthAmount = 2f;
        }
    }
}