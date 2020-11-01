namespace UnderwaterGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using UnderwaterGame.Assets;
    using UnderwaterGame.Entities;
    using UnderwaterGame.Environmentals;
    using UnderwaterGame.Items;
    using UnderwaterGame.Options;
    using UnderwaterGame.Sound;
    using UnderwaterGame.Sprites;
    using UnderwaterGame.Tiles;
    using UnderwaterGame.Ui;
    using UnderwaterGame.Ui.UiElements;
    using UnderwaterGame.Worlds;

    public class Main : Game
    {
        public static Thread loading;

        public static Config config;

        public static bool configCheck;

        public static Save save;

        public static bool saveCheck;

        public static TextureLibrary textureLibrary;

        public static FontLibrary fontLibrary;

        public static SoundLibrary soundLibrary;

        public static GraphicsDevice graphicsDeviceCurrent;

        public static GraphicsDeviceManager graphicsDeviceManagerCurrent;

        public static SpriteBatch spriteBatch;

        public static Point windowStartingPosition;

        public static Random random;

        public static int resolutionWidth = 1600;

        public static int resolutionHeight = 900;

        public Main()
        {
            graphicsDeviceManagerCurrent = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphicsDeviceCurrent = GraphicsDevice;
            graphicsDeviceManagerCurrent.PreferredBackBufferWidth = resolutionWidth;
            graphicsDeviceManagerCurrent.PreferredBackBufferHeight = resolutionHeight;
            graphicsDeviceManagerCurrent.ApplyChanges();
            Control.Refresh();
            Window.Title = "Underwater Game";
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            textureLibrary = new TextureLibrary();
            fontLibrary = new FontLibrary();
            soundLibrary = new SoundLibrary();
            windowStartingPosition = Window.Position;
            random = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureLibrary.LoadAll(Content, typeof(TextureLibrary));
            fontLibrary.LoadAll(Content, typeof(FontLibrary));
            soundLibrary.LoadAll(Content, typeof(SoundLibrary));
            Sprite.LoadAll();
            Item.LoadAll();
            Tile.LoadAll();
            Environmental.LoadAll();
            Option.LoadAll();
            UiManager.Init();
            SoundManager.Init();
            Camera.Init();
            Lighting.Init();
            World.Init();
            Read();
        }

        protected override void UnloadContent()
        {
            if(loading == null)
            {
                WriteSave();
                WriteConfig();
            }
            Content.Unload();
            foreach(Sprite sprite in Sprite.sprites)
            {
                foreach(Texture2D texture in sprite.textures)
                {
                    texture.Dispose();
                }
                foreach(Texture2D texture in sprite.texturesFilled)
                {
                    texture.Dispose();
                }
                foreach(Texture2D texture in sprite.texturesOutlined)
                {
                    texture.Dispose();
                }
            }
            Tile[] tiles = Tile.tiles.ToArray();
            for(int i = 0; i < tiles.Length; i++)
            {
                for(int t = 0; t < tiles[i].textures.Length; t++)
                {
                    tiles[i].textures[t].Dispose();
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if(loading == null)
            {
                if(!configCheck)
                {
                    if(config != null)
                    {
                        for(int i = 0; i < Option.options.Count; i++)
                        {
                            Option.GetOptionById((byte)(i + 1)).value = config.values[i];
                        }
                    }
                    configCheck = true;
                }
                if(!saveCheck)
                {
                    World.Generate();
                    if(save != null)
                    {
                        GameCursorElement gameCursor = (GameCursorElement)UiManager.GetElement<GameCursorElement>();
                        gameCursor.dragItem = Item.GetItemById(save.gameCursorDragItemId);
                        gameCursor.dragQuantity = save.gameCursorDragQuantity;
                    }
                    saveCheck = true;
                }
            }
            int idealWidth = Option.fullscreen.GetToggle() ? GraphicsDevice.DisplayMode.Width : resolutionWidth;
            int idealHeight = Option.fullscreen.GetToggle() ? GraphicsDevice.DisplayMode.Height : resolutionHeight;
            if(GetBufferWidth() != idealWidth || GetBufferHeight() != idealHeight)
            {
                graphicsDeviceManagerCurrent.PreferredBackBufferWidth = idealWidth;
                graphicsDeviceManagerCurrent.PreferredBackBufferHeight = idealHeight;
                graphicsDeviceManagerCurrent.ApplyChanges();
                Window.Position = Option.fullscreen.GetToggle() ? Point.Zero : windowStartingPosition;
            }
            Control.Refresh();
            Control.Update();
            if(loading == null || UiManager.fadeElements[2].alpha != UiManager.fadeElements[2].alphaMax)
            {
                EntityManager.Update();
                Camera.Update();
                World.Update();
            }
            UiManager.Update();
            SoundManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(112, 200, 255));
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Matrix.CreateTranslation(-Camera.position.X + (Camera.GetWidth() / 2f), -Camera.position.Y + (Camera.GetHeight() / 2f), 0f) * Matrix.CreateScale(Camera.scale, Camera.scale, 0f));
            if(loading == null || UiManager.fadeElements[2].alpha != UiManager.fadeElements[2].alphaMax)
            {
                EntityManager.Draw();
                Lighting.Draw();
                World.Draw();
            }
            spriteBatch.End();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Matrix.CreateScale(UiManager.scale, UiManager.scale, 0f));
            UiManager.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void Restart()
        {
            loading = new Thread(delegate ()
            {
                RestartGame();
                loading = null;
            });
            loading.Start();
        }

        public static void Read()
        {
            loading = new Thread(delegate ()
            {
                if(File.Exists(GetGameDirectory() + "Config.dat"))
                {
                    ReadConfig();
                }
                if(File.Exists(GetGameDirectory() + "Save.dat"))
                {
                    ReadSave();
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
                WriteSave();
                loading = null;
            });
            loading.Start();
        }

        private static void RestartGame()
        {
            while(UiManager.fadeElements[2].alpha < UiManager.fadeElements[2].alphaMax)
            {
                continue;
            }
            WriteConfig();
            WriteSave();
            EntityManager.entities.Clear();
            UiManager.Init();
            SoundManager.Init();
            Lighting.Init();
            Camera.positionTo = new Vector2(Camera.GetWidth(), Camera.GetHeight()) / 2f;
            Camera.position = Camera.positionTo;
        }

        private static void ReadConfig()
        {
            Directory.CreateDirectory(GetGameDirectory());
            FileStream stream = new FileStream(GetGameDirectory() + "Config.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                config = (Config)formatter.Deserialize(stream);
            }
            catch(SerializationException e) { Console.WriteLine(e.Message); }
            finally { stream.Close(); }
        }

        private static void WriteConfig()
        {
            Directory.CreateDirectory(GetGameDirectory());
            FileStream stream = new FileStream(GetGameDirectory() + "Config.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            config = new Config();
            try
            {
                formatter.Serialize(stream, config);
            }
            catch(SerializationException e) { Console.WriteLine(e.Message); }
            finally { stream.Close(); }
        }

        private static void ReadSave()
        {
            Directory.CreateDirectory(GetGameDirectory());
            FileStream stream = new FileStream(GetGameDirectory() + "Save.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                save = (Save)formatter.Deserialize(stream);
            }
            catch(SerializationException e) { Console.WriteLine(e.Message); }
            finally { stream.Close(); }
        }

        private static void WriteSave()
        {
            Directory.CreateDirectory(GetGameDirectory());
            FileStream stream = new FileStream(GetGameDirectory() + "Save.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            save = new Save();
            try
            {
                formatter.Serialize(stream, save);
            }
            catch(SerializationException e) { Console.WriteLine(e.Message); }
            finally { stream.Close(); }
        }

        public static int GetBufferWidth()
        {
            return graphicsDeviceManagerCurrent.PreferredBackBufferWidth;
        }

        public static int GetBufferHeight()
        {
            return graphicsDeviceManagerCurrent.PreferredBackBufferHeight;
        }

        public static string GetGameDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\UnderwaterGame\";
        }
    }
}