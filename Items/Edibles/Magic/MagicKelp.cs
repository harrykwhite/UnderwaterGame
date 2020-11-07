namespace UnderwaterGame.Items.Edibles.Magic
{
    using UnderwaterGame.Sprites;

    public class MagicKelp : MagicEdible
    {
        protected override void Init()
        {
            name = "Magic Kelp";
            sprite = Sprite.magicKelp;
            stack = true;
            useTime = 10;
            useAngleUpdate = false;
            usePress = true;
            useHide = true;
            magicAmount = 10f;
        }
    }
}