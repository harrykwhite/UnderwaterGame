using Microsoft.Xna.Framework.Audio;
using System;
using UnderwaterGame.Options;
using UnderwaterGame.Worlds;

namespace UnderwaterGame
{
    public static class Music
    {
        public static bool layer;

        public static int layerLoop;

        public static float layerLoopVolume;

        public static float layerLoopPreviousVolume;
        
        public static float layerLoopVolumeAcc = 0.1f;
        
        public static SoundEffectInstance layerLoopInstance;
        
        public static SoundEffectInstance layerLoopInstancePrevious;

        public static bool combat;

        public static float combatVolume;

        public static float combatVolumeAcc = 0.01f;
        
        public static int combatLoop;

        public static int combatLoopMax = 3;

        public static int combatLoopTime;

        public static int combatLoopTimeMax = 16320;

        public static SoundEffectInstance combatLoopInstance;

        public static SoundEffectInstance combatLoopInstancePrevious;

        public static SoundEffectInstance combatStingerInstance;

        public static void Init()
        {
        }

        public static void Update()
        {
            if(layer)
            {
                if(Main.loading == null)
                {
                    int layerLoopPrevious = layerLoop;
                    layerLoop = 0;
                    if(World.player != null)
                    {
                        if(World.player.inWater)
                        {
                            layerLoop = 1;
                        }
                    }
                    if(layerLoopInstance == null || layerLoopPrevious != layerLoop)
                    {
                        SoundEffect soundEffect = layerLoop switch
                        {
                            1 => Main.soundLibrary.MUSIC_LAYER0_LOOP1.asset,
                            2 => Main.soundLibrary.MUSIC_LAYER0_LOOP2.asset,
                            3 => Main.soundLibrary.MUSIC_LAYER0_LOOP3.asset,
                            4 => Main.soundLibrary.MUSIC_LAYER0_LOOP4.asset,
                            5 => Main.soundLibrary.MUSIC_LAYER0_LOOP5.asset,
                            _ => Main.soundLibrary.MUSIC_LAYER0_LOOP0.asset,
                        };
                        layerLoopInstancePrevious = layerLoopInstance;
                        layerLoopInstance = soundEffect.CreateInstance();
                        layerLoopInstance.IsLooped = true;
                        layerLoopInstance.Play();
                        layerLoopPreviousVolume = layerLoopVolume;
                        layerLoopVolume = 0f;
                    }
                    if(layerLoopVolume < 1f)
                    {
                        layerLoopVolume += Math.Min(layerLoopVolumeAcc, 1f - layerLoopVolume);
                    }
                    if(layerLoopPreviousVolume > 0f)
                    {
                        layerLoopPreviousVolume -= Math.Min(layerLoopVolumeAcc, layerLoopPreviousVolume);
                    }
                }
                else
                {
                    layer = false;
                    if(layerLoopVolume > 0f)
                    {
                        layerLoopVolume -= Math.Min(layerLoopVolumeAcc, layerLoopVolume);
                        layer = true;
                    }
                    if(layerLoopPreviousVolume > 0f)
                    {
                        layerLoopPreviousVolume -= Math.Min(layerLoopVolumeAcc, layerLoopPreviousVolume);
                        layer = true;
                    }
                }
            }
            else
            {
                layerLoopInstance = null;
                layerLoopInstancePrevious = null;
                if(Main.loading == null)
                {
                    layer = true;
                }
            }
            if(combat)
            {
                int combatLoopPrevious = combatLoop;
                if(combatLoopTime < combatLoopTimeMax)
                {
                    combatLoopTime += Main.elapsedTime;
                }
                else
                {
                    if(combatLoop < combatLoopMax)
                    {
                        combatLoop++;
                    }
                    else
                    {
                        combatLoop = 0;
                    }
                    combatLoopTime = 0;
                }
                if(combatLoopInstance == null || combatLoop != combatLoopPrevious)
                {
                    SoundEffect soundEffect = combatLoop switch
                    {
                        1 => Main.soundLibrary.MUSIC_COMBAT_LOOP1.asset,
                        2 => Main.soundLibrary.MUSIC_COMBAT_LOOP2.asset,
                        3 => Main.soundLibrary.MUSIC_COMBAT_LOOP3.asset,
                        _ => Main.soundLibrary.MUSIC_COMBAT_LOOP0.asset,
                    };
                    combatLoopInstancePrevious = combatLoopInstance;
                    combatLoopInstance = soundEffect.CreateInstance();
                    combatLoopInstance.Play();
                }
                if(Main.loading == null && World.hotspotCurrent != null)
                {
                    if(combatVolume < 1f)
                    {
                        combatVolume += Math.Min(combatVolumeAcc, 1f - combatVolume);
                    }
                }
                else
                {
                    if(combatVolume > 0f)
                    {
                        combatVolume -= Math.Min(combatVolumeAcc, combatVolume);
                    }
                    else
                    {
                        combat = false;
                    }
                }
            }
            else
            {
                combatLoop = 0;
                combatLoopTime = 0;
                combatLoopInstance = null;
                combatLoopInstancePrevious = null;
                if(Main.loading == null && World.hotspotCurrent != null)
                {
                    combat = true;
                    SoundEffect soundEffect = Main.random.Next(3) switch
                    {
                        1 => Main.soundLibrary.MUSIC_COMBAT_STINGER1.asset,
                        2 => Main.soundLibrary.MUSIC_COMBAT_STINGER2.asset,
                        _ => Main.soundLibrary.MUSIC_COMBAT_STINGER0.asset,
                    };
                    combatStingerInstance = soundEffect.CreateInstance();
                    combatStingerInstance.Play();
                }
            }
            if(layerLoopInstance != null)
            {
                layerLoopInstance.Volume = layerLoopVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
            if(layerLoopInstancePrevious != null)
            {
                layerLoopInstancePrevious.Volume = layerLoopPreviousVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
            if(combatLoopInstance != null)
            {
                combatLoopInstance.Volume = combatVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
            if(combatLoopInstancePrevious != null)
            {
                combatLoopInstancePrevious.Volume = combatVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
            if(combatStingerInstance != null)
            {
                combatStingerInstance.Volume = combatVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
        }
    }
}