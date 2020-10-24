using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Armours.Legs
{
    public class WoodenLeggings : LegArmour
    {
        protected override void Init()
        {
            Name = "Wooden Leggings";
            Sprite = Sprite.WoodenLeggings;

            WearSprite = Sprite.WoodenLeggingsWear;
            WearDefense = 3f;
        }
    }
}