namespace UnderwaterGame.Items.Armours.Heads
{
    using UnderwaterGame.Sprites;

    public class StoneHelmet : HeadArmour
    {
        protected override void Init()
        {
            name = "Stone Helmet";
            sprite = Sprite.stoneHelmet;
            wearSprite = Sprite.stoneHelmetWear;
            wearDefense = 1;
        }
    }
}