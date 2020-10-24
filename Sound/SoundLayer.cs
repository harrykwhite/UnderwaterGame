using Microsoft.Xna.Framework.Audio;
using System;

namespace UnderwaterGame.Sound
{
    public class SoundLayer
    {
        public Func<int> getSoundEffect;

        public int SoundEffect { get; private set; }
        public int SoundEffectTo { get; private set; }

        public SoundEffect[] SoundEffects { get; private set; }
        public SoundEffectInstance[] SoundEffectInstances { get; private set; }
        public float[] SoundEffectVolumes { get; private set; }

        public SoundLayer(int count)
        {
            if (!SoundManager.SoundLayers.Contains(this))
            {
                SoundManager.SoundLayers.Add(this);
            }

            SoundEffects = new SoundEffect[count];
            SoundEffectInstances = new SoundEffectInstance[count];
            SoundEffectVolumes = new float[count];
        }

        public void Update()
        {
            SoundEffectTo = getSoundEffect.Invoke();

            for (int i = 0; i < SoundEffects.Length; i++)
            {
                if (SoundEffectInstances[i] == null)
                {
                    SoundEffectInstances[i] = SoundEffects[i].CreateInstance();

                    SoundEffectInstances[i].IsLooped = true;
                    SoundEffectInstances[i].Volume = 0f;

                    SoundEffectInstances[i].Play();
                }

                float destination = 0f;

                if ((i == SoundEffect && SoundEffect == SoundEffectTo) || i == SoundEffectTo)
                {
                    destination = 1f;
                }

                if (SoundEffectVolumes[i] < destination)
                {
                    SoundEffectVolumes[i] += Math.Min(0.05f, destination - SoundEffectVolumes[i]);
                }
                else if (SoundEffectVolumes[i] > destination)
                {
                    SoundEffectVolumes[i] -= Math.Min(0.05f, SoundEffectVolumes[i] - destination);
                }

                SoundEffectInstances[i].Volume = SoundManager.FilterVolume(SoundEffectVolumes[i], SoundManager.Category.Music);
            }

            if (SoundEffect != SoundEffectTo)
            {
                if (SoundEffectVolumes[SoundEffect] == 0f)
                {
                    SoundEffect = SoundEffectTo;
                }
            }
        }

        public void Stop()
        {
            for (int i = 0; i < SoundEffects.Length; i++)
            {
                if (SoundEffectVolumes[i] > 0f)
                {
                    SoundEffectVolumes[i] -= Math.Min(0.05f, SoundEffectVolumes[i]);
                }

                SoundEffectInstances[i].Volume = SoundManager.FilterVolume(SoundEffectVolumes[i], SoundManager.Category.Music);
            }
        }
    }
}