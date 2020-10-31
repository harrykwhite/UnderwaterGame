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
            damage = 6f;
            swingSpeed = 1.5f;
            swingLength = 6f;
            hitboxOffset = 34f;
            swingSprite = Sprite.longSwing;
        }
    }
}