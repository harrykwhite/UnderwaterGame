namespace UnderwaterGame.Utilities
{
    using Microsoft.Xna.Framework.Audio;
    using UnderwaterGame.Options;
    
    public static class SoundUtilities
    {
        public static SoundEffectInstance PlaySound(SoundEffect soundEffect, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Play();
            soundEffectInstance.Volume = volume * Option.soundVolume.value;
            soundEffectInstance.Pitch = pitch;
            soundEffectInstance.Pan = pan;
            return soundEffectInstance;
        }

        public static SoundEffectInstance PlaySoundRange(SoundEffect soundEffect, float volumeMin = 1f, float volumeMax = 1f, float pitchMin = 0f, float pitchMax = 0f, float panMin = 0f, float panMax = 0f)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Play();
            soundEffectInstance.Volume = RandomUtilities.Range(volumeMin, volumeMax) * Option.soundVolume.value;
            soundEffectInstance.Pitch = RandomUtilities.Range(pitchMin, pitchMax);
            soundEffectInstance.Pan = RandomUtilities.Range(panMin, panMax);
            return soundEffectInstance;
        }
    }
}
