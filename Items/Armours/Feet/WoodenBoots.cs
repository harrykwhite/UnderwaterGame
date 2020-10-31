namespace UnderwaterGame.Items.Armours.Feet
{
    using UnderwaterGame.Sprites;

    public class WoodenBoots : FeetArmour
    {
        protected override void Init()
        {
            name = "Wooden Boots";
            sprite = Sprite.woodenBoots;
            wearSprite = Sprite.woodenBootsWear;
            wearDefense = 1f;
        }
    }
}