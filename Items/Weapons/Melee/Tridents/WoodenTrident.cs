namespace UnderwaterGame.Items.Weapons.Melee.Tridents
{
    using UnderwaterGame.Sprites;

    public class WoodenTrident : TridentMelee
    {
        protected override void Init()
        {
            name = "Wooden Trident";
            sprite = Sprite.woodenTrident;
            useTime = 28;
            useStrength = 2f;
            damage = 6f;
            strength = 4f;
            swingSpeed = 1.5f;
            swingLength = 6f;
            hitboxOffset = 34f;
            swingSprite = Sprite.longSwing;
        }
    }
}