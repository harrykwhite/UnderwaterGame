using System;

namespace UnderwaterGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Main main = new Main())
            {
                main.Run();
            }
        }
    }
}
