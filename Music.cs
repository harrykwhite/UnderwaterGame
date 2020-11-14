using Microsoft.Xna.Framework.Audio;
using System;
using UnderwaterGame.Worlds;

namespace UnderwaterGame
{
    public static class Music
    {
        public static int layerLoop;
        
        public static SoundEffectInstance layerLoopInstance;
        
        public static SoundEffectInstance layerLoopInstancePrevious;
        
        public static int combatLoop;

        public static int combatLoopMax = 3;
        
        public static int combatLoopTime;

        public static int combatLoopTimeMax = 1020;

        public static SoundEffectInstance combatLoopInstance;

        public static SoundEffectInstance combatIntroStingerInstance;

        public static SoundEffectInstance combatOutroStingerInstance;
        
        public static void Init()
        {
            combatLoop = Main.random.Next(combatLoopMax + 1);
        }

        public static void Update()
        {
            float layerVolumeAcc = 0.1f;
            float combatVolumeAcc = 0.01f;
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
                layerLoopInstance.Volume = 0f;
                layerLoopInstance.Play();
            }
            if(layerLoopInstance.Volume < 1f)
            {
                layerLoopInstance.Volume += Math.Min(layerVolumeAcc, 1f - layerLoopInstance.Volume);
            }
            if(layerLoopInstancePrevious != null)
            {
                if(layerLoopInstancePrevious.Volume > 0f)
                {
                    layerLoopInstancePrevious.Volume -= Math.Min(layerVolumeAcc, layerLoopInstancePrevious.Volume);
                }
                else
                {
                    layerLoopInstancePrevious.Stop();
                    layerLoopInstancePrevious = null;
                }
            }
            if(World.hotspotCurrent != null)
            {
                int combatLoopPrevious = combatLoop;
                combatOutroStingerInstance = null;
                if(combatIntroStingerInstance == null)
                {
                    SoundEffect soundEffect = Main.random.Next(3) switch
                    {
                        1 => Main.soundLibrary.MUSIC_COMBAT_INTROSTINGER1.asset,
                        2 => Main.soundLibrary.MUSIC_COMBAT_INTROSTINGER2.asset,
                        _ => Main.soundLibrary.MUSIC_COMBAT_INTROSTINGER0.asset,
                    };
                    combatIntroStingerInstance = soundEffect.CreateInstance();
                    combatIntroStingerInstance.Play();
                }
                if(combatLoopTime < combatLoopTimeMax)
                {
                    combatLoopTime++;
                }
                else
                {
                    do
                    {
                        combatLoop = Main.random.Next(combatLoopMax + 1);
                    } while(combatLoop == combatLoopPrevious);
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
                    combatLoopInstance = soundEffect.CreateInstance();
                    combatLoopInstance.Volume = 0f;
                    combatLoopInstance.Play();
                }
                if(combatLoopInstance.Volume < 1f)
                {
                    combatLoopInstance.Volume += Math.Min(combatVolumeAcc, 1f - combatLoopInstance.Volume);
                }
            }
            else
            {
                combatIntroStingerInstance = null;
                if(combatOutroStingerInstance == null)
                {
                    SoundEffect soundEffect = Main.random.Next(3) switch
                    {
                        1 => Main.soundLibrary.MUSIC_COMBAT_OUTROSTINGER1.asset,
                        2 => Main.soundLibrary.MUSIC_COMBAT_OUTROSTINGER2.asset,
                        _ => Main.soundLibrary.MUSIC_COMBAT_OUTROSTINGER0.asset,
                    };
                    combatOutroStingerInstance = soundEffect.CreateInstance();
                    combatOutroStingerInstance.Play();
                }
                if(combatLoopInstance != null)
                {
                    if(combatLoopInstance.Volume > 0f)
                    {
                        combatLoopInstance.Volume -= Math.Min(combatVolumeAcc, combatLoopInstance.Volume);
                    }
                    else
                    {
                        combatLoopInstance.Stop();
                        combatLoopInstance = null;
                        combatLoop = 0;
                        combatLoopTime = 0;
                    }
                }
            }
        }
    }
}