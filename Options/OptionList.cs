namespace UnderwaterGame.Options
{
    using System.Collections.Generic;
    using UnderwaterGame.Options.Audio;

    public abstract partial class Option
    {
        public static List<Option> options = new List<Option>();

        public static SoundVolume soundVolume;

        public static MusicVolume musicVolume;

        public static MasterVolume masterVolume;

        public static void LoadAll()
        {
            soundVolume = Load<SoundVolume>(1);
            musicVolume = Load<MusicVolume>(2);
            masterVolume = Load<MasterVolume>(3);
        }
    }
}