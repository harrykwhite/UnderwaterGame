using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Edibles.Healing
{
    public class HealingKelp : HealingEdible
    {
        protected override void Init()
        {
            Name = "Healing Kelp";
            Sprite = Sprite.HealingKelp;
            Stack = true;

            UseTime = 10;
            UseAngleUpdate = false;
            UsePress = true;
            UseHide = true;

            healAmount = 5f;
        }
    }
}