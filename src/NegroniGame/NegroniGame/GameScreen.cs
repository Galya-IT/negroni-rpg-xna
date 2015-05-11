namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;
    using Microsoft.Xna.Framework.Audio;
    using NegroniGame.Handlers;
    using System;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;
    using log4net;
    using log4net.Config;
    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public sealed class GameScreen : Microsoft.Xna.Framework.Game
    {
        // Singleton !
        private static GameScreen instance;

        #region Fields Declaration

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        private static readonly ILog log = LogManager.GetLogger(typeof(GameScreen));

        private readonly GraphicsDeviceManager graphics;
        private Vector2 cursorPos = new Vector2();
        private List<Texture2D> monster1Textures, monster2Textures, monster3Textures, monster4Textures, monster5Textures, monster6Textures;
        private Video video;
        private VideoPlayer videoPlayer;
        private Texture2D videoTexture;
        private Color videoColor;
        private Song inGameMusic, gameOverMusic;

        #endregion

        private GameScreen()
        {
            // Windows settings
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 700;
            graphics.ApplyChanges(); //Changes the settings that you just applied

            Content.RootDirectory = "Content";

            GameState = 0;
        }

        public static GameScreen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameScreen();
                }
                return instance;
            }
        }

        #region Properties Declarations

        public MouseState MouseState { get; set; }
        public MouseState MouseStatePrevious { get; set; }
        public KeyboardState KeyboardState { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public int GameState { get; set; }
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }
        public SpriteFont FontMessages { get; private set; }
        public SpriteFont FontInfoBox { get; private set; }
        public KeyboardState KeyboardStatePrevious { get; private set; }
        public List<List<Texture2D>> MonstersTextures { get; private set; }
        public List<Texture2D> AllSceneryTextures { get; private set; }
        public List<Texture2D> PlayerTextures { get; private set; }
        public List<Texture2D> SlotsTextures { get; private set; }
        public List<Texture2D> MajesticSetTextures { get; private set; }
        public List<Texture2D> NegroniHPTextures { get; private set; }
        public List<Texture2D> ShotsTextures { get; private set; }
        public List<Texture2D> DropTextures { get; private set; }
        public List<Texture2D> HealthBars { get; private set; }
        public Texture2D NewbieStaffTexture { get; private set; }
        public Texture2D MysticStaffTexture { get; private set; }
        public Texture2D CoinsTexture { get; private set; }
        public Texture2D ElixirsTexture { get; private set; }
        public Texture2D CursorTexture { get; private set; }
        public Texture2D InfoBoxTexture { get; private set; }
        public Texture2D InfoBox1Texture { get; private set; }
        public Texture2D FireballsTexture { get; private set; }
        public Texture2D MarketDialog { get; private set; }
        public Texture2D BuyButton { get; private set; }
        public Texture2D GameOverTex { get; private set; }
        public Texture2D NpcSorcererTexture { get; private set; }
        public Texture2D NpcHelperTexture { get; private set; }
        public SoundEffect PickUpSound { get; private set; }
        public SoundEffect FireAttackSound { get; private set; }
        public SoundEffect DrinkElixir { get; private set; }
        public SoundEffect DrinkWell { get; private set; }
        public SoundEffect WeaponBought { get; private set; }
        public SoundEffect ArmorBought { get; private set; }
        public SoundEffect ElixirBought { get; private set; }
        public List<SoundEffect> HitSounds { get; private set; }

        #endregion

        protected override void Initialize()
        {
            FileAppender fileAppender = new FileAppender();
            fileAppender.File = "GameStatusLogs.txt";
            fileAppender.AppendToFile = true;
            fileAppender.Layout = new log4net.Layout.SimpleLayout();
            fileAppender.Threshold = Level.Info;
            fileAppender.ActivateOptions();

            BasicConfigurator.Configure(fileAppender);

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            // device = graphics.GraphicsDevice;

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            video = Content.Load<Video>("media/IntroVideo");
            videoPlayer = new VideoPlayer();
            videoPlayer.Play(video);
            videoColor = new Color(255, 255, 255);

            base.Initialize();

            log.Info("XNA Game Initialization completed");
        }

        protected override void LoadContent()
        {
            NpcSorcererTexture = Content.Load<Texture2D>("media/sprites/sorcerer");

            NpcHelperTexture = Content.Load<Texture2D>("media/sprites/player2");

            FontMessages = Content.Load<SpriteFont>("Segoe UI Mono");

            FontInfoBox = Content.Load<SpriteFont>("Segoe UI Mono Smaller");

            CursorTexture = Content.Load<Texture2D>("media/cursor1"); // cursor

            CoinsTexture = Content.Load<Texture2D>("media/drop/coins");

            ElixirsTexture = Content.Load<Texture2D>("media/drop/elixirs");

            NewbieStaffTexture = Content.Load<Texture2D>("media/drop/newbieStaff");

            MysticStaffTexture = Content.Load<Texture2D>("media/drop/mysticStaff");

            InfoBoxTexture = Content.Load<Texture2D>("media/infoBox");

            InfoBox1Texture = Content.Load<Texture2D>("media/infoBox1");

            MarketDialog = Content.Load<Texture2D>("media/marketDialog");

            BuyButton = Content.Load<Texture2D>("media/buy");

            GameOverTex = Content.Load<Texture2D>("media/GameOver");

            // background, toolbar, well, playerPic, equipmentShop
            AllSceneryTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/background"),
                Content.Load<Texture2D>("media/toolbar"),
                Content.Load<Texture2D>("media/well"),
                Content.Load<Texture2D>("media/Elvina"),
                Content.Load<Texture2D>("media/market")
            };

            PlayerTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/Elvina-right"),
                Content.Load<Texture2D>("media/sprites/Elvina-left"),
                Content.Load<Texture2D>("media/sprites/Elvina-up"),
                Content.Load<Texture2D>("media/sprites/Elvina-down"),
                Content.Load<Texture2D>("media/sprites/Elvina-dead"),
            };

            MajesticSetTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/drop/majesticBoots"),
                Content.Load<Texture2D>("media/drop/majesticGloves"),
                Content.Load<Texture2D>("media/drop/majesticHelmet"),
                Content.Load<Texture2D>("media/drop/majesticRobe"),
                Content.Load<Texture2D>("media/drop/majesticShield")
            };

            monster1Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster1-down"),
                Content.Load<Texture2D>("media/sprites/monster1-left"),
                Content.Load<Texture2D>("media/sprites/monster1-right"),
                Content.Load<Texture2D>("media/sprites/monster1-up"),
            };

            monster2Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster2-down"),
                Content.Load<Texture2D>("media/sprites/monster2-left"),
                Content.Load<Texture2D>("media/sprites/monster2-right"),
                Content.Load<Texture2D>("media/sprites/monster2-up"),
            };

            monster3Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster3-down"),
                Content.Load<Texture2D>("media/sprites/monster3-left"),
                Content.Load<Texture2D>("media/sprites/monster3-right"),
                Content.Load<Texture2D>("media/sprites/monster3-up"),
            };

            monster4Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster4-down"),
                Content.Load<Texture2D>("media/sprites/monster4-left"),
                Content.Load<Texture2D>("media/sprites/monster4-right"),
                Content.Load<Texture2D>("media/sprites/monster4-up"),
            };

            monster5Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster5-down"),
                Content.Load<Texture2D>("media/sprites/monster5-left"),
                Content.Load<Texture2D>("media/sprites/monster5-right"),
                Content.Load<Texture2D>("media/sprites/monster5-up"),
            };

            monster6Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster6-down"),
                Content.Load<Texture2D>("media/sprites/monster6-left"),
                Content.Load<Texture2D>("media/sprites/monster6-right"),
                Content.Load<Texture2D>("media/sprites/monster6-up"),
            };

            MonstersTextures = new List<List<Texture2D>>()
            {
                monster1Textures,
                monster2Textures,
                monster3Textures,
                monster4Textures,
                monster5Textures,
                monster6Textures
            };

            SlotsTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/slots/defaultSlot1"),
                Content.Load<Texture2D>("media/slots/defaultSlot2"),
                Content.Load<Texture2D>("media/slots/defaultSlot3"),
                Content.Load<Texture2D>("media/slots/defaultSlot4"),
                Content.Load<Texture2D>("media/slots/defaultSlot5"),
                Content.Load<Texture2D>("media/slots/defaultSlot6"),
                Content.Load<Texture2D>("media/slots/defaultSlot7"),
                Content.Load<Texture2D>("media/slots/defaultSlot8"),
            };

            NegroniHPTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/negroniHPfull"),
                Content.Load<Texture2D>("media/negroniHP2of3"),
                Content.Load<Texture2D>("media/negroniHP1of3"),
                Content.Load<Texture2D>("media/negroniHPempty")
            };

            ShotsTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/fireballs")
            };

            FireballsTexture = Content.Load<Texture2D>("media/sprites/fireballs");

            DropTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/drop/coins2"),
                Content.Load<Texture2D>("media/drop/elixirs")
            };

            HealthBars = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/health-full"),
                Content.Load<Texture2D>("media/health-low")
            };

            ElixirBought = Content.Load<SoundEffect>("media/sounds/potionBought");

            WeaponBought = Content.Load<SoundEffect>("media/sounds/weaponBought");

            ArmorBought = Content.Load<SoundEffect>("media/sounds/armorBought");

            HitSounds = new List<SoundEffect>()
            {
                Content.Load<SoundEffect>("media/sounds/hit1"),
                Content.Load<SoundEffect>("media/sounds/hit2"),
                Content.Load<SoundEffect>("media/sounds/hit3"),
            };

            DrinkWell = Content.Load<SoundEffect>("media/sounds/drinkWell");

            DrinkElixir = Content.Load<SoundEffect>("media/sounds/drinkPotion");

            FireAttackSound = Content.Load<SoundEffect>("media/sounds/firehit");

            PickUpSound = Content.Load<SoundEffect>("media/sounds/pickup");

            gameOverMusic = Content.Load<Song>("media/sounds/DST-GameOver");

            inGameMusic = Content.Load<Song>("media/sounds/DST-Exanos");

            Player.Instance.Initialize();
            NpcSorcerer.Instance.Initialize();

            log.Info("XNA Game load completed");
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState = Mouse.GetState();
            cursorPos = new Vector2(MouseState.X, MouseState.Y); // cursor update

            KeyboardState = Keyboard.GetState();

            // Allows the game to exit
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                this.OnExiting(this, new EventArgs());
                //this.Exit();
            }

            switch (GameState)
            {
                case 0: // Video Intro
                    if (videoPlayer.State != MediaState.Stopped)
                    {
                        videoTexture = videoPlayer.GetTexture();
                    }
                    else
                    {
                        MediaPlayer.Play(inGameMusic);
                        MediaPlayer.IsRepeating = true;
                        GameState = 1;
                    }
                    break;

                case 1: // Game Started
                    if (videoColor.A > 1)
                    {
                        videoColor.A -= 2;
                    }

                    // Checks for Pause
                    if (KeyboardState.IsKeyDown(Keys.P) && KeyboardStatePrevious.IsKeyUp(Keys.P))
                    {
                        MediaPlayer.Pause();
                        GameState = 2;
                    }

                    Player.Instance.Update(gameTime);
                    MonstersHandler.Instance.Update(gameTime);
                    DropHandler.Instance.Update(gameTime);
                    ShotsHandler.Instance.UpdateShots(gameTime, KeyboardState);
                    Toolbar.InventorySlots.Instance.Update(gameTime, MouseState);
                    Toolbar.SystemMsg.Instance.GetLastMessages();
                    Toolbar.HP.Instance.Update(gameTime);
                    InfoBoxes.Instance.Update(gameTime, MouseState);
                    ElixirsHandler.Instance.Update(gameTime); // updates elixir reuse time
                    Well.Instance.Update(gameTime); // updates well reuse time
                    MarketDialogHandler.Instance.Update(MouseState, MouseStatePrevious);
                    GameOverHandler.Instance.Update(gameTime);

                    NpcHelperHandler.Update(gameTime);

                    break;

                case 2: // Paused

                    // Checks for Resume
                    if (KeyboardState.IsKeyDown(Keys.P) && KeyboardStatePrevious.IsKeyUp(Keys.P))
                    {
                        MediaPlayer.Resume();
                        GameState = 1;
                    }

                    Toolbar.HP.Instance.Update(gameTime);
                    InfoBoxes.Instance.Update(gameTime, MouseState);
                    MarketDialogHandler.Instance.Update(MouseState, MouseStatePrevious);

                    break;

                case 3: // Game Over
                    if (MediaPlayer.Queue.ActiveSong == inGameMusic)
                    {
                        MediaPlayer.Play(gameOverMusic);
                    }
                    InfoBoxes.Instance.Update(gameTime, MouseState);
                    break;
            }

            this.KeyboardStatePrevious = KeyboardState;
            this.MouseStatePrevious = MouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            if (GameState != 0 && videoColor.A != 255)
            {
                SceneryHandler.Instance.Draw(); // Scenery

                DropHandler.Instance.Draw(); // Drop

                ShotsHandler.Instance.Draw(); // Shots

                Player.Instance.Draw(); // Player

                MonstersHandler.Instance.Draw(gameTime); // Monsters

                MarketDialogHandler.Instance.Draw(); // Market dialog

                Toolbar.InventorySlots.Instance.Draw(); // Inventory
                Toolbar.SystemMsg.Instance.DrawText(); // System messages
                Toolbar.HP.Instance.Draw(); // HP bar

                NpcHelperHandler.Draw();

                InfoBoxes.Instance.Draw(); // Pop-up info boxes

                GameOverHandler.Instance.Draw();

                SpriteBatch.Draw(CursorTexture, cursorPos, Color.White); // draws cursor
            }

            if (videoColor.A > 1)
            {
                SpriteBatch.Draw(videoTexture, new Vector2(0, 0), videoColor); // Intro Video
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            uint exitEV = MessageBox(new IntPtr(0), "Would you like to exit?", "Game Over", 1);

            if (exitEV == 1) //ok
            {
                this.Exit();
            }
        }
    }
}