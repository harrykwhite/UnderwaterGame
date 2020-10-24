using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using UnderwaterGame.Options;
using UnderwaterGame.Utilities;

namespace UnderwaterGame.Sound
{
    public static class SoundManager
    {
        public enum Category
        {
            Sound,
            Music
        }

        public static List<SoundEmitter> SoundEmitters { get; private set; } = new List<SoundEmitter>();
        public static List<SoundLayer> SoundLayers { get; private set; } = new List<SoundLayer>();

        public static List<SoundEmitter> StopSoundEmitters { get; private set; } = new List<SoundEmitter>();
        public static List<SoundLayer> StopSoundLayers { get; private set; } = new List<SoundLayer>();

        public static void Init()
        {
            StopSoundEmitters.Clear();
            StopSoundLayers.Clear();

            StopSoundEmitters.AddRange(SoundEmitters);
            StopSoundLayers.AddRange(SoundLayers);

            SoundEmitters.Clear();
            SoundLayers.Clear();

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
            SoundLayers.Add(new SoundLayer(6));

            SoundLayers[0].SoundEffects[0] = Main.SoundLibrary.MUSIC_LAYER0_TRACK0.Asset;
            SoundLayers[0].SoundEffects[1] = Main.SoundLibrary.MUSIC_LAYER0_TRACK1.Asset;
            SoundLayers[0].SoundEffects[2] = Main.SoundLibrary.MUSIC_LAYER0_TRACK2.Asset;
            SoundLayers[0].SoundEffects[3] = Main.SoundLibrary.MUSIC_LAYER0_TRACK3.Asset;
            SoundLayers[0].SoundEffects[4] = Main.SoundLibrary.MUSIC_LAYER0_TRACK4.Asset;
            SoundLayers[0].SoundEffects[5] = Main.SoundLibrary.MUSIC_LAYER0_TRACK5.Asset;

            SoundLayers[0].getSoundEffect = delegate ()
            {
                if (Main.World?.player != null)
                {
                    if (!Main.World.player.InWater)
                    {
                        return 0;
                    }
                }

                return 1;
            };
        }

        public static void UpdateEmitters()
        {
            SoundEmitter[] soundEmittersTemp = SoundEmitters.ToArray();

            foreach (SoundEmitter soundEmitter in soundEmittersTemp)
            {
                soundEmitter.Update();
            }
        }

        public static void UpdateLayers()
        {
            SoundLayer[] soundLayersTemp = SoundLayers.ToArray();

            foreach (SoundLayer soundLayer in soundLayersTemp)
            {
                soundLayer.Update();
            }
        }

        public static void StopEmitters()
        {
            SoundEmitter[] stopSoundEmittersTemp = StopSoundEmitters.ToArray();

            foreach (SoundEmitter stopSoundEmitter in stopSoundEmittersTemp)
            {
                stopSoundEmitter.Stop();
            }
        }

        public static void StopLayers()
        {
            SoundLayer[] stopSoundLayersTemp = StopSoundLayers.ToArray();

            foreach (SoundLayer stopSoundLayer in stopSoundLayersTemp)
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
            volume *= Option.MasterVolume.value;

            switch (category)
            {
                case Category.Music:
                    volume *= Option.MusicVolume.value;
                    break;

                case Category.Sound:
                    volume *= Option.SoundVolume.value;
                    break;
            }

            return volume;
        }
    }
}