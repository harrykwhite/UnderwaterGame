namespace UnderwaterGame.Sound
{
    using Microsoft.Xna.Framework.Audio;
    using System.Collections.Generic;
    using UnderwaterGame.Options;
    using UnderwaterGame.Utilities;
    using UnderwaterGame.Worlds;

    public static class SoundManager
    {
        public enum Category
        {
            Sound,
            Music
        }

        public static List<SoundEmitter> soundEmitters = new List<SoundEmitter>();

        public static List<SoundLayer> soundLayers = new List<SoundLayer>();

        public static List<SoundEmitter> stopSoundEmitters = new List<SoundEmitter>();

        public static List<SoundLayer> stopSoundLayers = new List<SoundLayer>();

        public static void Init()
        {
            stopSoundEmitters.Clear();
            stopSoundLayers.Clear();
            stopSoundEmitters.AddRange(soundEmitters);
            stopSoundLayers.AddRange(soundLayers);
            soundEmitters.Clear();
            soundLayers.Clear();
            LoadLayers();
        }

        public static void Update()
        {
            UpdateEmitters();
            UpdateLayers();
            StopEmitters();
            StopLayers();
        }

        public static void LoadLayers()
        {
            soundLayers.Add(new SoundLayer(6));
            soundLayers[0].soundEffects[0] = Main.soundLibrary.MUSIC_LAYER0_TRACK0.asset;
            soundLayers[0].soundEffects[1] = Main.soundLibrary.MUSIC_LAYER0_TRACK1.asset;
            soundLayers[0].soundEffects[2] = Main.soundLibrary.MUSIC_LAYER0_TRACK2.asset;
            soundLayers[0].soundEffects[3] = Main.soundLibrary.MUSIC_LAYER0_TRACK3.asset;
            soundLayers[0].soundEffects[4] = Main.soundLibrary.MUSIC_LAYER0_TRACK4.asset;
            soundLayers[0].soundEffects[5] = Main.soundLibrary.MUSIC_LAYER0_TRACK5.asset;
            soundLayers[0].getSoundEffect = delegate ()
            {
                if(World.player != null)
                {
                    if(!World.player.inWater)
                    {
                        return 0;
                    }
                }
                return 1;
            };
        }

        public static void UpdateEmitters()
        {
            SoundEmitter[] soundEmittersTemp = soundEmitters.ToArray();
            foreach(SoundEmitter soundEmitter in soundEmittersTemp)
            {
                soundEmitter.Update();
            }
        }

        public static void UpdateLayers()
        {
            SoundLayer[] soundLayersTemp = soundLayers.ToArray();
            foreach(SoundLayer soundLayer in soundLayersTemp)
            {
                soundLayer.Update();
            }
        }

        public static void StopEmitters()
        {
            SoundEmitter[] stopSoundEmittersTemp = stopSoundEmitters.ToArray();
            foreach(SoundEmitter stopSoundEmitter in stopSoundEmittersTemp)
            {
                stopSoundEmitter.Stop();
            }
        }

        public static void StopLayers()
        {
            SoundLayer[] stopSoundLayersTemp = stopSoundLayers.ToArray();
            foreach(SoundLayer stopSoundLayer in stopSoundLayersTemp)
            {
                stopSoundLayer.Stop();
            }
        }

        public static SoundEffectInstance PlaySound(SoundEffect soundEffect, Category category, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Play();
            soundEffectInstance.Volume = FilterVolume(volume, category);
            soundEffectInstance.Pitch = pitch;
            soundEffectInstance.Pan = pan;
            return soundEffectInstance;
        }

        public static SoundEffectInstance PlaySoundRange(SoundEffect soundEffect, Category category, float volumeMin = 1f, float volumeMax = 1f, float pitchMin = 0f, float pitchMax = 0f, float panMin = 0f, float panMax = 0f)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Play();
            soundEffectInstance.Volume = FilterVolume(RandomUtilities.Range(volumeMin, volumeMax), category);
            soundEffectInstance.Pitch = RandomUtilities.Range(pitchMin, pitchMax);
            soundEffectInstance.Pan = RandomUtilities.Range(panMin, panMax);
            return soundEffectInstance;
        }

        public static float FilterVolume(float volume, Category? category = null)
        {
            volume *= Option.masterVolume.value;
            switch(category)
            {
                case Category.Music:
                    volume *= Option.musicVolume.value;
                    break;

                case Category.Sound:
                    volume *= Option.soundVolume.value;
                    break;
            }
            return volume;
        }
    }
}