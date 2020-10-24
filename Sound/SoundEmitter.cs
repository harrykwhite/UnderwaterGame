using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace UnderwaterGame.Sound
{
    public class SoundEmitter
    {
        private float volume;
        public Vector2 position;

        public SoundEffect SoundEffect { get; private set; }
        public SoundEffectInstance SoundEffectInstance { get; private set; }

        public SoundEmitter(SoundEffect type)
        {
            if (!SoundManager.SoundEmitters.Contains(this))
            {
                SoundManager.SoundEmitters.Add(this);
            }

            SoundEffect = type;
            SoundEffectInstance = SoundEffect.CreateInstance();
        }

        public void Update()
        {
            float distance = Vector2.Distance(position, Camera.position);

            volume = 1f - (distance / 100f);
            volume = MathHelper.Clamp(volume, 0f, 1f);

            if (SoundEffectInstance == null)
            {
                SoundEffectInstance = SoundEffect.CreateInstance();
                SoundEffectInstance.Play();
            }

            if (SoundEffectInstance.State != SoundState.Playing)
            {
                SoundEffectInstance.Play();
            }

            SoundEffectInstance.Volume = volume;
        }

        public void Stop()
        {
            if (volume > 0f)
            {
                volume -= Math.Min(0.05f, volume);
            }
            else
            {
                SoundEffectInstance?.Stop();
            }
        }
    }
}