using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Armours.Chests
{
    public class WoodenChestplate : ChestArmour
    {
        protected override void Init()
        {
            Name = "Wooden Chestplate";
            Sprite = Sprite.WoodenChestplate;

            WearSprite = Sprite.WoodenChestplateWear;
            WearDefense = 4f;
        }
    }
}