namespace UnderwaterGame.Items.Armours.Chests
{
    using UnderwaterGame.Sprites;

    public class WoodenChestplate : ChestArmour
    {
        protected override void Init()
        {
            name = "Wooden Chestplate";
            sprite = Sprite.woodenChestplate;
            wearSprite = Sprite.woodenChestplateWear;
            wearDefense = 3f;
        }
    }
}