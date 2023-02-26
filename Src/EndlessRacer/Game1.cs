using EndlessRacer.GameScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace EndlessRacer
{
    public class Game1 : Game
    {
        public readonly GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

        private readonly ScreenManager _screenManager;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            LoadTitleScreen();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void LoadTitleScreen()
        {
            _screenManager.LoadScreen(new TitleScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void LoadEndlessScreen()
        {
            _screenManager.LoadScreen(new EndlessMode(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void LoadCareerProgressScreen()
        {
            _screenManager.LoadScreen(new CareerProgressScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}