namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;

    public class Hit
    {
        public int damage;

        public float strength;

        public Vector2 at;

        public float? direction;

        public Hit(int damage, float strength, Vector2 at, float? direction)
        {
            this.damage = damage;
            this.strength = strength;
            this.at = at;
            this.direction = direction;
        }
    }
}