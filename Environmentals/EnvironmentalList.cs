namespace UnderwaterGame.Environmentals
{
    using System.Collections.Generic;
    using UnderwaterGame.Environmentals.Seaweed;

    public abstract partial class Environmental
    {
        public static List<Environmental> environmentals = new List<Environmental>();

        public static BigSeaweed bigSeaweed;

        public static SmallSeaweed smallSeaweed;

        public static void LoadAll()
        {
            bigSeaweed = Load<BigSeaweed>(1);
            smallSeaweed = Load<SmallSeaweed>(2);
        }
    }
}