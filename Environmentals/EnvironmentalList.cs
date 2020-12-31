namespace UnderwaterGame.Environmentals
{
    using System.Collections.Generic;
    using UnderwaterGame.Environmentals.Rocks;
    using UnderwaterGame.Environmentals.Statues;

    public abstract partial class Environmental
    {
        public static List<Environmental> environmentals = new List<Environmental>();
        
        public static Seaweed.Seaweed seaweed;
        
        public static RockSmall rockSmall;

        public static RockLarge rockLarge;

        public static SpawnStatue spawnStatue;

        public static void LoadAll()
        {
            seaweed = Load<Seaweed.Seaweed>(1);
            rockSmall = Load<RockSmall>(2);
            rockLarge = Load<RockLarge>(3);
            spawnStatue = Load<SpawnStatue>(4);
        }
    }
}