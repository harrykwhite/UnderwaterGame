namespace UnderwaterGame.Options
{
    using System.Collections.Generic;
    
    public abstract partial class Option
    {
        public static List<Option> options = new List<Option>();

        public static Windowed windowed;
        
        public static SoundVolume soundVolume;

        public static MusicVolume musicVolume;

        public static MasterVolume masterVolume;

        public static void LoadAll()
        {
            windowed = Load<Windowed>(1);
            soundVolume = Load<SoundVolume>(2);
            musicVolume = Load<MusicVolume>(3);
            masterVolume = Load<MasterVolume>(4);
        }
    }
}