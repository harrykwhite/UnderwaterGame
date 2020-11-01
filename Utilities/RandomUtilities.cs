namespace UnderwaterGame.Utilities
{
    public static class RandomUtilities
    {
        public static float Range(float min, float max)
        {
            return Main.random.Next((int)(min * 100f), (int)(max * 100f)) / 100f;
        }
    }
}