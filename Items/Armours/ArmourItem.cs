using UnderwaterGame.Sprites;

namespace UnderwaterGame.Items.Armours
{
    public abstract class ArmourItem : Item
    {
        public Sprite WearSprite { get; protected set; }
        public float WearDefense { get; protected set; }
    }
}