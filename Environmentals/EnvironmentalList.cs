namespace UnderwaterGame.Environmentals
{
    using System.Collections.Generic;
    using UnderwaterGame.Environmentals.Statues;

    public abstract partial class Environmental
    {
        public static List<Environmental> environmentals = new List<Environmental>();
        
        public static Seaweed.Seaweed seaweed;
        
        public static SpawnStatue spawnStatue;

        public static void LoadAll()
        {
            seaweed = Load<Seaweed.Seaweed>(1);
            spawnStatue = Load<SpawnStatue>(2);
        }
    }
}