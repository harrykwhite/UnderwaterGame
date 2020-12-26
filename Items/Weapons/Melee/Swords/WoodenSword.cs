namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
    using UnderwaterGame.Sprites;

    public class WoodenSword : SwordMelee
    {
        protected override void Init()
        {
            name = "Wooden Sword";
            sprite = Sprite.woodenSword;
            useTime = 25;
            useStrength = 2f;
            damage = 2;
            strength = 4f;
            hitboxOffset = 24f;
            swingSprite = Sprite.wideSwing;
        }
    }
}