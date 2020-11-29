namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
    using UnderwaterGame.Sprites;

    public class WoodenSword : SwordMelee
    {
        protected override void Init()
        {
            name = "Wooden Sword";
            sprite = Sprite.woodenSword;
            useTime = 24;
            useStrength = 2f;
            damage = 8f;
            strength = 4f;
            hitboxOffset = 24f;
            swingSprite = Sprite.wideSwing;
        }
    }
}