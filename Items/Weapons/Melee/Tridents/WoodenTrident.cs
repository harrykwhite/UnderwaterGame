﻿namespace UnderwaterGame.Items.Weapons.Melee.Tridents
{
    using UnderwaterGame.Sprites;

    public class WoodenTrident : TridentMelee
    {
        protected override void Init()
        {
            name = "Wooden Trident";
            sprite = Sprite.woodenTrident;
            useTime = 30;
            useStrength = 2f;
            damage = 2;
            strength = 4f;
            swingSpeed = 2f;
            swingLength = 6f;
            hitboxOffset = 34f;
            swingSprite = Sprite.longSwing;
        }
    }
}