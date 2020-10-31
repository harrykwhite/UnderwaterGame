namespace UnderwaterGame.Items.Armours.Heads
{
    using UnderwaterGame.Sprites;

    public class WoodenHelmet : HeadArmour
    {
        protected override void Init()
        {
            name = "Wooden Helmet";
            sprite = Sprite.woodenHelmet;
            wearSprite = Sprite.woodenHelmetWear;
            wearDefense = 3f;
        }
    }
}