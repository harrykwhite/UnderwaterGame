using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Weapons.Melee.Swords
{
    public class WoodenSword : SwordMelee
    {
        protected override void Init()
        {
            Name = "Wooden Sword";
            Sprite = Sprite.WoodenSword;

            UseTime = 24;

            Damage = 7f;

            hitboxOffset = 24f;
            swingSprite = Sprite.WideSwing;
        }
    }
}