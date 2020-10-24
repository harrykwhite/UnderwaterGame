using System.Collections.Generic;
using UnderwaterGame.Environmentals.Seaweed;

namespace UnderwaterGame.Environmentals
{
    public abstract partial class Environmental
    {
        public static List<Environmental> Environmentals { get; private set; } = new List<Environmental>();

        public static BigSeaweed BigSeaweed { get; private set; }
        public static SmallSeaweed SmallSeaweed { get; private set; }

        public static void LoadAll()
        {
            BigSeaweed = Load<BigSeaweed>(1);
            SmallSeaweed = Load<SmallSeaweed>(2);
        }
    }
}