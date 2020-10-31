namespace UnderwaterGame.Sound
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using System;

    public class SoundEmitter
    {
        private float volume;

        public Vector2 position;

        public SoundEffect soundEffect;

        public SoundEffectInstance soundEffectInstance;

        public SoundEmitter(SoundEffect type)
        {
            if(!SoundManager.soundEmitters.Contains(this))
            {
                SoundManager.soundEmitters.Add(this);
            }
            soundEffect = type;
            soundEffectInstance = soundEffect.CreateInstance();
        }

        public void Update()
        {
            float distance = Vector2.Distance(position, Camera.position);
            volume = 1f - (distance / 100f);
            volume = MathHelper.Clamp(volume, 0f, 1f);
            if(soundEffectInstance == null)
            {
                soundEffectInstance = soundEffect.CreateInstance();
                soundEffectInstance.Play();
            }
            if(soundEffectInstance.State != SoundState.Playing)
            {
                soundEffectInstance.Play();
            }
            soundEffectInstance.Volume = volume;
        }

        public void Stop()
        {
            if(volume > 0f)
            {
                volume -= Math.Min(0.05f, volume);
            }
            else
            {
                soundEffectInstance?.Stop();
            }
        }
    }
}