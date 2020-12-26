namespace UnderwaterGame
{
    public class Spawn
    {
        public string type;

        public float chance;

        public Spawn(string type, float chance)
        {
            this.type = type;
            this.chance = chance;
        }
    }
}
