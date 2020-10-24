using System.Collections.Generic;
using UnderwaterGame.Options.Display;

namespace UnderwaterGame.Options
{
    public abstract partial class Option
    {
        public static List<Option> Options { get; private set; } = new List<Option>();

        public static Fullscreen Fullscreen { get; private set; }
        public static SoundVolume SoundVolume { get; private set; }
        public static MusicVolume MusicVolume { get; private set; }
        public static MasterVolume MasterVolume { get; private set; }

        public static void LoadAll()
        {
            Fullscreen = Load<Fullscreen>(1);
            SoundVolume = Load<SoundVolume>(2);
            MusicVolume = Load<MusicVolume>(3);
            MasterVolume = Load<MasterVolume>(4);
        }
    }
}