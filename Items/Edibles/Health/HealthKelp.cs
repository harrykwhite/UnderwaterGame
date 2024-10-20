﻿namespace UnderwaterGame.Items.Edibles.Health
{
    using UnderwaterGame.Sprites;

    public class HealthKelp : HealthEdible
    {
        protected override void Init()
        {
            name = "Health Kelp";
            sprite = Sprite.healthKelp;
            stack = true;
            useTime = 5;
            useAngleUpdate = false;
            usePress = true;
            useHide = true;
            healthAmount = 2;
        }
    }
}