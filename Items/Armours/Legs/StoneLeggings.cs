namespace UnderwaterGame.Items.Armours.Legs
{
    using UnderwaterGame.Sprites;

    public class StoneLeggings : LegArmour
    {
        protected override void Init()
        {
            name = "Stone Leggings";
            sprite = Sprite.stoneLeggings;
            wearSprite = Sprite.stoneLeggingsWear;
            wearDefense = 2f;
        }
    }
}