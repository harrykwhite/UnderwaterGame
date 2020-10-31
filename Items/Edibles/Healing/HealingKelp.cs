namespace UnderwaterGame.Items.Edibles.Healing
{
    using UnderwaterGame.Sprites;

    public class HealingKelp : HealingEdible
    {
        protected override void Init()
        {
            name = "Healing Kelp";
            sprite = Sprite.healingKelp;
            stack = true;
            useTime = 10;
            useAngleUpdate = false;
            usePress = true;
            useHide = true;
            healAmount = 5f;
        }
    }
}