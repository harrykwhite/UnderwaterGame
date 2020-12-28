namespace UnderwaterGame
{
    using Microsoft.Xna.Framework.Audio;
    using System;
    using UnderwaterGame.Options;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Worlds;

    public static class Music
    {
        public static int layerLoop;

        public static float layerLoopVolume;

        public static float layerLoopPreviousVolume;

        public static float layerLoopVolumeAcc = 0.1f;

        public static SoundEffectInstance layerLoopInstance;

        public static SoundEffectInstance layerLoopInstancePrevious;

        public static bool combat;

        public static int combatLoop;

        public static int combatLoopMax = 3;

        public static int combatLoopTime;

        public static int combatLoopTimeMax = 16320;

        public static float combatLoopVolume;

        public static float combatLoopVolumeInAcc = 0.01f;

        public static float combatLoopVolumeOutAcc = 0.005f;

        public static int combatLoopIntroStingerTime;

        public static int combatLoopIntroStingerTimeMax;

        public static SoundEffectInstance combatLoopInstance;

        public static SoundEffectInstance combatLoopInstancePrevious;

        public static SoundEffectInstance combatIntroStingerInstance;

        public static SoundEffectInstance combatOutroStingerInstance;

        public static void Init()
        {
        }

        public static void Update()
        {
            bool loading = Main.loading != null && (UiManager.fadeElements[3]?.alpha ?? 0f) >= (UiManager.fadeElements[3]?.alphaMax ?? 0f);
            if(!loading)
            {
                int layerLoopPrevious = layerLoop;
                layerLoop = 1;
                if(World.player != null)
                {
                    if(!World.player.inWater && World.player.life > 0)
                    {
                        layerLoop = 0;
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
                        _ => Main.soundLibrary.MUSIC_LAYER0_LOOP0.asset
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
            if(combat)
            {
                if(combatLoopVolume < 1f)
                {
                    combatLoopVolume += Math.Min(combatLoopVolumeInAcc, 1f - combatLoopVolume);
                }
                if(!loading && World.hotspotCurrent == null)
                {
                    if(World.hotspotPrevious != null)
                    {
                        /*if(World.hotspotPrevious.count <= 0)
                        {
                            SoundEffect soundEffect = Main.random.Next(3) switch
                            {
                                1 => Main.soundLibrary.MUSIC_COMBAT_OUTROSTINGER1.asset,
                                2 => Main.soundLibrary.MUSIC_COMBAT_OUTROSTINGER2.asset,
                                _ => Main.soundLibrary.MUSIC_COMBAT_OUTROSTINGER0.asset
                            };
                            combatOutroStingerInstance = soundEffect.CreateInstance();
                            combatOutroStingerInstance.Play();
                        }*/
                    }
                    combat = false;
                }
            }
            else
            {
                if(combatLoopVolume > 0f)
                {
                    combatLoopVolume -= Math.Min(combatLoopVolumeOutAcc, combatLoopVolume);
                }
                if(!loading && World.hotspotCurrent != null)
                {
                    if(combatLoopVolume == 0f && (combatIntroStingerInstance == null || combatLoopIntroStingerTime == combatLoopIntroStingerTimeMax))
                    {
                        SoundEffect soundEffect;
                        switch(Main.random.Next(3))
                        {
                            case 1:
                                soundEffect = Main.soundLibrary.MUSIC_COMBAT_INTROSTINGER1.asset;
                                combatLoopIntroStingerTimeMax = 2032;
                                break;

                            case 2:
                                soundEffect = Main.soundLibrary.MUSIC_COMBAT_INTROSTINGER2.asset;
                                combatLoopIntroStingerTimeMax = 1024;
                                break;

                            default:
                                soundEffect = Main.soundLibrary.MUSIC_COMBAT_INTROSTINGER0.asset;
                                combatLoopIntroStingerTimeMax = 1040;
                                break;
                        }
                        combatIntroStingerInstance = soundEffect.CreateInstance();
                        combatIntroStingerInstance.Play();
                        combatLoop = 0;
                        combatLoopTime = 0;
                        combatLoopIntroStingerTime = 0;
                        combatLoopInstance = null;
                        combatLoopInstancePrevious = null;
                    }
                    combat = true;
                }
            }
            if(combatLoopIntroStingerTime < combatLoopIntroStingerTimeMax)
            {
                if(combatIntroStingerInstance != null)
                {
                    combatLoopIntroStingerTime += Main.elapsedTime;
                }
            }
            else
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
                        _ => Main.soundLibrary.MUSIC_COMBAT_LOOP0.asset
                    };
                    combatLoopInstancePrevious = combatLoopInstance;
                    combatLoopInstance = soundEffect.CreateInstance();
                    combatLoopInstance.Play();
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
                combatLoopInstance.Volume = combatLoopVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
            if(combatLoopInstancePrevious != null)
            {
                combatLoopInstancePrevious.Volume = combatLoopVolume * Option.musicVolume.value * Option.masterVolume.value;
            }
            if(combatIntroStingerInstance != null)
            {
                combatIntroStingerInstance.Volume = Option.musicVolume.value * Option.masterVolume.value;
            }
            if(combatOutroStingerInstance != null)
            {
                combatOutroStingerInstance.Volume = Option.musicVolume.value * Option.masterVolume.value;
            }
        }
    }
}