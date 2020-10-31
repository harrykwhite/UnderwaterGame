namespace UnderwaterGame.Items.Armours.Legs
{
    using UnderwaterGame.Sprites;

    public class WoodenLeggings : LegArmour
    {
        protected override void Init()
        {
            name = "Wooden Leggings";
            sprite = Sprite.woodenLeggings;
            wearSprite = Sprite.woodenLeggingsWear;
            wearDefense = 2f;
        }
    }
}