namespace UnderwaterGame.Sound
{
    using Microsoft.Xna.Framework.Audio;
    using System;

    public class SoundLayer
    {
        public Func<int> getSoundEffect;

        public int soundEffect;

        public int soundEffectTo;

        public SoundEffect[] soundEffects;

        public SoundEffectInstance[] soundEffectInstances;

        public float[] soundEffectVolumes;

        public SoundLayer(int count)
        {
            if(!SoundManager.soundLayers.Contains(this))
            {
                SoundManager.soundLayers.Add(this);
            }
            soundEffects = new SoundEffect[count];
            soundEffectInstances = new SoundEffectInstance[count];
            soundEffectVolumes = new float[count];
        }

        public void Update()
        {
            soundEffectTo = getSoundEffect();
            for(int i = 0; i < soundEffects.Length; i++)
            {
                if(soundEffectInstances[i] == null)
                {
                    soundEffectInstances[i] = soundEffects[i].CreateInstance();
                    soundEffectInstances[i].IsLooped = true;
                    soundEffectInstances[i].Volume = 0f;
                    soundEffectInstances[i].Play();
                }
                float destination = 0f;
                if((i == soundEffect && soundEffect == soundEffectTo) || i == soundEffectTo)
                {
                    destination = 1f;
                }
                if(soundEffectVolumes[i] < destination)
                {
                    soundEffectVolumes[i] += Math.Min(0.05f, destination - soundEffectVolumes[i]);
                }
                else if(soundEffectVolumes[i] > destination)
                {
                    soundEffectVolumes[i] -= Math.Min(0.05f, soundEffectVolumes[i] - destination);
                }
                soundEffectInstances[i].Volume = SoundManager.FilterVolume(soundEffectVolumes[i], SoundManager.Category.Music);
            }
            if(soundEffect != soundEffectTo)
            {
                if(soundEffectVolumes[soundEffect] == 0f)
                {
                    soundEffect = soundEffectTo;
                }
            }
        }

        public void Stop()
        {
            for(int i = 0; i < soundEffects.Length; i++)
            {
                if(soundEffectVolumes[i] > 0f)
                {
                    soundEffectVolumes[i] -= Math.Min(0.05f, soundEffectVolumes[i]);
                }
                soundEffectInstances[i].Volume = SoundManager.FilterVolume(soundEffectVolumes[i], SoundManager.Category.Music);
            }
        }
    }
}