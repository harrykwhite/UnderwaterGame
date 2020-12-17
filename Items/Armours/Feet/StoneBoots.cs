namespace UnderwaterGame.Items.Armours.Feet
{
    using UnderwaterGame.Sprites;

    public class StoneBoots : FeetArmour
    {
        protected override void Init()
        {
            name = "Stone Boots";
            sprite = Sprite.stoneBoots;
            wearSprite = Sprite.stoneBootsWear;
            wearDefense = 1f;
        }
    }
}