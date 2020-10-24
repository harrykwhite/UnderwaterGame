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
using UnderwaterGame.Input;
using UnderwaterGame.Items;
using UnderwaterGame.Options;
using UnderwaterGame.Sound;
using UnderwaterGame.Sprites;
using UnderwaterGame.Tiles;
using UnderwaterGame.UI;
using UnderwaterGame.UI.UIElements;
using UnderwaterGame.Worlds;

namespace UnderwaterGame
{
    public class Main : Game
    {
        public static Thread loading;

        public static World World { get; private set; }

        public static Config Config { get; private set; }
        public static bool ConfigCheck { get; private set; }

        public static Save Save { get; private set; }
        public static bool SaveCheck { get; private set; }

        public static TextureLibrary TextureLibrary { get; private set; }
        public static FontLibrary FontLibrary { get; private set; }
        public static SoundLibrary SoundLibrary { get; private set; }

        public static GraphicsDevice GraphicsDeviceCurrent { get; private set; }
        public static GraphicsDeviceManager GraphicsDeviceManagerCurrent { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static Point WindowStartingPosition { get; private set; }

        public static Random Random { get; private set; }

        public static int ResolutionWidth => 1600;
        public static int ResolutionHeight => 900;

        public static int BufferWidth => GraphicsDeviceManagerCurrent.PreferredBackBufferWidth;
        public static int BufferHeight => GraphicsDeviceManagerCurrent.PreferredBackBufferHeight;

        public static string GameDirectory => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\UnderwaterGame\";

        public static bool IsPaused => UIManager.InMenu;

        public Main()
        {
            GraphicsDeviceManagerCurrent = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GraphicsDeviceCurrent = GraphicsDevice;

            GraphicsDeviceManagerCurrent.PreferredBackBufferWidth = ResolutionWidth;
            GraphicsDeviceManagerCurrent.PreferredBackBufferHeight = ResolutionHeight;
            GraphicsDeviceManagerCurrent.ApplyChanges();

            InputManager.Refresh();

            Window.Title = "Underwater Game";
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;

            TextureLibrary = new TextureLibrary();
            FontLibrary = new FontLibrary();
            SoundLibrary = new SoundLibrary();

            WindowStartingPosition = Window.Position;
            Random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            TextureLibrary.LoadAll(Content, typeof(TextureLibrary));
            FontLibrary.LoadAll(Content, typeof(FontLibrary));
            SoundLibrary.LoadAll(Content, typeof(SoundLibrary));

            Sprite.LoadAll();
            Item.LoadAll();
            Tile.LoadAll();
            Environmental.LoadAll();
            Option.LoadAll();

            UIManager.Init();
            SoundManager.Init();

            Camera.Init();
            Lighting.Init();

            Read();
        }

        protected override void UnloadContent()
        {
            if (loading == null)
            {
                WriteSave();
                WriteConfig();
            }

            Content.Unload();

            foreach (Sprite sprite in Sprite.Sprites)
            {
                foreach (Texture2D texture in sprite.Textures)
                {
                    texture.Dispose();
                }

                foreach (Texture2D texture in sprite.TexturesFilled)
                {
                    texture.Dispose();
                }

                foreach (Texture2D texture in sprite.TexturesOutlined)
                {
                    texture.Dispose();
                }
            }

            Tile[] tiles = Tile.Tiles.ToArray();

            for (int i = 0; i < tiles.Length; i++)
            {
                for (int t = 0; t < tiles[i].Textures.Length; t++)
                {
                    tiles[i].Textures[t].Dispose();
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (loading == null)
            {
                if (!ConfigCheck)
                {
                    if (Config != null)
                    {
                        for (int i = 0; i < Option.Options.Count; i++)
                        {
                            Option.GetOptionByID((byte)(i + 1)).value = Config.values[i];
                        }
                    }

                    ConfigCheck = true;
                }

                if (!SaveCheck)
                {
                    World = new World();
                    World.Generate();

                    if (Save != null)
                    {
                        GameCursorElement gameCursor = (GameCursorElement)UIManager.GetElement<GameCursorElement>();
                        gameCursor.dragItem = Item.GetItemByID(Save.gameCursorDragItemID);
                        gameCursor.dragQuantity = Save.gameCursorDragQuantity;
                    }

                    SaveCheck = true;
                }
            }

            int displayWidth = GraphicsDeviceManagerCurrent.IsFullScreen ? GraphicsDevice.DisplayMode.Width : ResolutionWidth;
            int displayHeight = GraphicsDeviceManagerCurrent.IsFullScreen ? GraphicsDevice.DisplayMode.Height : ResolutionHeight;

            if (GraphicsDeviceManagerCurrent.IsFullScreen != Option.Fullscreen.Toggle || GraphicsDeviceManagerCurrent.PreferredBackBufferWidth != displayWidth || GraphicsDeviceManagerCurrent.PreferredBackBufferHeight != displayHeight)
            {
                GraphicsDeviceManagerCurrent.IsFullScreen = Option.Fullscreen.Toggle;

                GraphicsDeviceManagerCurrent.PreferredBackBufferWidth = displayWidth;
                GraphicsDeviceManagerCurrent.PreferredBackBufferHeight = displayHeight;

                GraphicsDeviceManagerCurrent.ApplyChanges();

                Window.Position = GraphicsDeviceManagerCurrent.IsFullScreen ? Point.Zero : WindowStartingPosition;
            }

            InputManager.Refresh();
            InputManager.Update();

            if (loading == null || UIManager.FadeElements[2].Alpha != UIManager.FadeElements[2].alphaMax)
            {
                World?.UpdateAreas();

                EntityManager.Update();
                Camera.Update();
            }

            UIManager.Update();
            SoundManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(114, 201, 255));

            SpriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Camera.Transform * Matrix.CreateScale(Camera.Scale, Camera.Scale, 0f));

            if (loading == null || UIManager.FadeElements[2].Alpha != UIManager.FadeElements[2].alphaMax)
            {
                if (World != null)
                {
                    World.DrawTilemaps();
                    Lighting.Draw();
                }

                EntityManager.Draw();
            }

            SpriteBatch.End();

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Matrix.CreateScale(UIManager.Scale, UIManager.Scale, 0f));

            UIManager.Draw();

            SpriteBatch.End();

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
                if (File.Exists(GameDirectory + "Config.dat"))
                {
                    ReadConfig();
                }

                if (File.Exists(GameDirectory + "Save.dat"))
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
            while (UIManager.FadeElements[2].Alpha < UIManager.FadeElements[2].alphaMax)
            {
                continue;
            }

            WriteConfig();
            WriteSave();

            EntityManager.Entities.Clear();

            UIManager.Init();
            SoundManager.Init();

            Lighting.Init();

            Camera.positionTo = new Vector2(Camera.Width, Camera.Height) / 2f;
            Camera.position = Camera.positionTo;
        }

        private static void ReadConfig()
        {
            Directory.CreateDirectory(GameDirectory);

            FileStream stream = new FileStream(GameDirectory + "Config.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                Config = (Config)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        private static void WriteConfig()
        {
            Directory.CreateDirectory(GameDirectory);

            FileStream stream = new FileStream(GameDirectory + "Config.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            Config = new Config();

            try
            {
                formatter.Serialize(stream, Config);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        private static void ReadSave()
        {
            Directory.CreateDirectory(GameDirectory);

            FileStream stream = new FileStream(GameDirectory + "Save.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                Save = (Save)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        private static void WriteSave()
        {
            Directory.CreateDirectory(GameDirectory);

            FileStream stream = new FileStream(GameDirectory + "Save.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            Save = new Save();

            try
            {
                formatter.Serialize(stream, Save);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                stream.Close();
            }
        }
    }
}