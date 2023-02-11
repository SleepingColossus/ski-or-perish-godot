using EndlessRacer.Constants;
using EndlessRacer.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRacer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _treeSprite;
        private Texture2D _rockSprite;
        private Texture2D _playerSprite;

        private Level _level;
        private Player _player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            EngineComponents.Set(_graphics, _spriteBatch);

            _level = new Level();
            _player = new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2, 100));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _treeSprite = Content.Load<Texture2D>("ObstacleLarge");
            _playerSprite = Content.Load<Texture2D>("Player");

            LevelSprites.Sprites.Add("tree", _treeSprite);
            LevelSprites.Sprites.Add("rock", _rockSprite);
            LevelSprites.Sprites.Add("player", _playerSprite);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _level.Update(gameTime);
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Snow);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _level.Draw();
            _player.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}