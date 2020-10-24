using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Weapons.Melee.Tridents
{
    public class WoodenTrident : TridentMelee
    {
        protected override void Init()
        {
            Name = "Wooden Trident";
            Sprite = Sprite.WoodenTrident;

            UseTime = 28;

            Damage = 6f;

            swingSpeed = 1.5f;
            swingLength = 6f;

            hitboxOffset = 34f;
            swingSprite = Sprite.LongSwing;
        }
    }
}