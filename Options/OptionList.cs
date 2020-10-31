namespace UnderwaterGame.Options
{
    using System.Collections.Generic;
    using UnderwaterGame.Options.Display;

    public abstract partial class Option
    {
        public static List<Option> options = new List<Option>();

        public static Fullscreen fullscreen;

        public static SoundVolume soundVolume;

        public static MusicVolume musicVolume;

        public static MasterVolume masterVolume;

        public static void LoadAll()
        {
            fullscreen = Load<Fullscreen>(1);
            soundVolume = Load<SoundVolume>(2);
            musicVolume = Load<MusicVolume>(3);
            masterVolume = Load<MasterVolume>(4);
        }
    }
}