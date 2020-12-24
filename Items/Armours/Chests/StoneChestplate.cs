namespace UnderwaterGame.Items.Armours.Chests
{
    using UnderwaterGame.Sprites;

    public class StoneChestplate : ChestArmour
    {
        protected override void Init()
        {
            name = "Stone Chestplate";
            sprite = Sprite.stoneChestplate;
            wearSprite = Sprite.stoneChestplateWear;
            wearDefense = 1;
        }
    }
}