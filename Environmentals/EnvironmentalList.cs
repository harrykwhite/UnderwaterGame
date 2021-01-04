namespace UnderwaterGame.Environmentals
{
    using System.Collections.Generic;
    using UnderwaterGame.Environmentals.Rocks;
    using UnderwaterGame.Environmentals.Statues;

    public abstract partial class Environmental
    {
        public static List<Environmental> environmentals = new List<Environmental>();
        
        public static Seaweed.Seaweed seaweed;
        
        public static Rock rock;

        public static Statue statue;

        public static void LoadAll()
        {
            seaweed = Load<Seaweed.Seaweed>(1);
            rock = Load<Rock>(2);
            statue = Load<Statue>(3);
        }
    }
}