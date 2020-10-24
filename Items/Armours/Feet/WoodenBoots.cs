using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Armours.Feet
{
    public class WoodenBoots : FeetArmour
    {
        protected override void Init()
        {
            Name = "Wooden Boots";
            Sprite = Sprite.WoodenBoots;

            WearSprite = Sprite.WoodenBootsWear;
            WearDefense = 2f;
        }
    }
}