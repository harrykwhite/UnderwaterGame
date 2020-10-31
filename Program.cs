namespace UnderwaterGame
{
    using System;

    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using(Main main = new Main())
            {
                main.Run();
            }
        }
    }
}