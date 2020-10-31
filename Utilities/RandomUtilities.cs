namespace UnderwaterGame.Utilities
{
    public static class RandomUtilities
    {
        public static T Choose<T>(params T[] items)
        {
            return items[Main.random.Next(items.Length)];
        }

        public static bool Chance(float chance)
        {
            int realChance = (int)(chance * 100f);
            return Main.random.Next(100) <= realChance;
        }

        public static float Range(float min, float max)
        {
            int mn = (int)(min * 10000f);
            int mx = (int)(max * 10000f);
            float rand = Main.random.Next(mn, mx);
            return rand / 10000f;
        }
    }
}