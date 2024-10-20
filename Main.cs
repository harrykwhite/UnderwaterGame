﻿namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using UnderwaterGame.Assets;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Items;
    using UnderwaterGame.Options;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Worlds;

    public class Main : Game
    {
        public static Thread loading;

        public static Config config;

        public static TextureLibrary textureLibrary;

        public static FontLibrary fontLibrary;

        public static SoundLibrary soundLibrary;

        public static GraphicsDevice graphicsDevice;

        public static GraphicsDeviceManager graphicsDeviceManager;

        public static SpriteBatch spriteBatch;

        public static Random random;

        public static List<Texture2D> unloadTextures = new List<Texture2D>();

        public static int resolutionWidth = 1600;

        public static int resolutionHeight = 900;

        public static int elapsedTime;

        public static bool restart;

        public static int restartTime;

        public Main()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Underwater Game";
            Window.AllowAltF4 = true;
            graphicsDevice = GraphicsDevice;
            graphicsDeviceManager.PreferredBackBufferWidth = resolutionWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = resolutionHeight;
            graphicsDeviceManager.ApplyChanges();
            Control.Refresh();
            textureLibrary = new TextureLibrary();
            fontLibrary = new FontLibrary();
            soundLibrary = new SoundLibrary();
            random = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureLibrary.LoadAll(Content);
            fontLibrary.LoadAll(Content);
            soundLibrary.LoadAll(Content);
            Sprite.LoadAll();
            Item.LoadAll();
            Tile.LoadAll();
            Environmental.LoadAll();
            Option.LoadAll();
            EntityManager.Init();
            UiManager.Init();
            Camera.Init();
            Lighting.Init();
            World.Init();
            Music.Init();
            Read();
        }

        protected override void UnloadContent()
        {
            if(loading == null)
            {
                WriteConfig();
            }
            Content.Unload();
            foreach(Texture2D unloadTexture in unloadTextures)
            {
                unloadTexture.Dispose();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            if(restart)
            {
                if(restartTime > 0)
                {
                    restartTime--;
                }
                else
                {
                    loading = new Thread(delegate ()
                    {
                        RestartGame();
                        loading = null;
                    });
                    loading.Start();
                    restart = false;
                }
            }
            if(loading == null)
            {
                if(!World.generated)
                {
                    World.Generate();
                }
            }
            Control.Refresh();
            Control.Update();
            if(loading == null || (UiManager.fadeElements[3]?.alpha ?? 0f) < (UiManager.fadeElements[3]?.alphaMax ?? 0f))
            {
                EntityManager.Update();
                World.Update();
                Camera.Update();
            }
            UiManager.Update();
            Music.Update();
            if(graphicsDeviceManager.IsFullScreen == Option.windowed.GetToggle())
            {
                graphicsDeviceManager.IsFullScreen = !graphicsDeviceManager.IsFullScreen;
                graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.IsFullScreen ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width : resolutionWidth;
                graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.IsFullScreen ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height : resolutionHeight;
                graphicsDeviceManager.ApplyChanges();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(107, 205, 255));
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Matrix.CreateTranslation(-Camera.position.X + (Camera.GetWidth() / 2f), -Camera.position.Y + (Camera.GetHeight() / 2f), 0f) * Matrix.CreateScale(Camera.scale, Camera.scale, 0f));
            if(loading == null || UiManager.fadeElements[3]?.alpha != UiManager.fadeElements[3]?.alphaMax)
            {
                EntityManager.Draw();
                Lighting.Draw();
                World.Draw();
            }
            spriteBatch.End();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            UiManager.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void Read()
        {
            loading = new Thread(delegate ()
            {
                if(File.Exists(GetGameDirectory() + "Config"))
                {
                    ReadConfig();
                }
                if(config != null)
                {
                    for(int i = 0; i < Option.options.Count; i++)
                    {
                        Option.GetOptionById((byte)(i + 1)).value = config.values[i];
                    }
                }
                loading = null;
            });
            loading.Start();
        }

        public static void Write()
        {
            loading = new Thread(delegate ()
            {
                WriteConfig();
                loading = null;
            });
            loading.Start();
        }

        private static void RestartGame()
        {
            while((UiManager.fadeElements[3]?.alpha ?? 0f) < (UiManager.fadeElements[3]?.alphaMax ?? 0f))
            {
                continue;
            }
            WriteConfig();
            EntityManager.Init();
            UiManager.Init();
            Camera.Init();
            Lighting.Init();
            World.Init();
            Music.Init();
            Camera.positionTo = new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f;
            Camera.position = Camera.positionTo;
        }

        private static void ReadConfig()
        {
            Directory.CreateDirectory(GetGameDirectory());
            FileStream fileStream = new FileStream(GetGameDirectory() + "Config", FileMode.Open);
            try
            {
                config = (Config)new BinaryFormatter().Deserialize(fileStream);
            }
            finally
            {
                fileStream.Close();
            }
        }

        private static void WriteConfig()
        {
            Directory.CreateDirectory(GetGameDirectory());
            FileStream fileStream = new FileStream(GetGameDirectory() + "Config", FileMode.Create);
            try
            {
                new BinaryFormatter().Serialize(fileStream, config = new Config());
            }
            finally
            {
                fileStream.Close();
            }
        }

        public static int GetBufferWidth()
        {
            return graphicsDeviceManager.PreferredBackBufferWidth;
        }

        public static int GetBufferHeight()
        {
            return graphicsDeviceManager.PreferredBackBufferHeight;
        }

        public static string GetGameDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\UnderwaterGame\";
        }
    }
}