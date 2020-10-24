using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Armours.Heads
{
    public class WoodenHelmet : HeadArmour
    {
        protected override void Init()
        {
            Name = "Wooden Helmet";
            Sprite = Sprite.WoodenHelmet;

            WearSprite = Sprite.WoodenHelmetWear;
            WearDefense = 4f;
        }
    }
}