using EndlessRacer.Environment;
using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace EndlessRacer.Career
{
    internal class CareerLevelMode : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Player _player;
        private Level _level;

        private double _victoryTimer = 3;

        public CareerLevelMode(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var playerMoveSprite = Content.Load<Texture2D>("Player/PlayerMove");
            var playerJumpSprite = Content.Load<Texture2D>("Player/PlayerJump");
            var playerHurtSprite = Content.Load<Texture2D>("Player/PlayerHurt");
            var playerVictorySprite = Content.Load<Texture2D>("Player/PlayerVictory");

            var playerPosition = new Vector2(Game.Graphics.PreferredBackBufferWidth / 2, Constants.PlayerYPosition);

            _player = new Player(playerPosition, playerMoveSprite, playerJumpSprite, playerHurtSprite, playerVictorySprite);

            var careerProgress = CareerProgress.Get();

            _level = new PredefinedLevel(LevelImporter.ImportCareerLevel(Content, careerProgress.CurrentLevel));
        }

        public override void Update(GameTime gameTime)
        {
            var scrollSpeed = _player.Update(gameTime);
            _level.Update(scrollSpeed, _player);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.LoadMainMenu();
            }

            if (_player.IsVictorious)
            {
                _victoryTimer -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_victoryTimer <= 0)
                {
                    Game.LoadCareerProgressScreen();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Snow);

            Game.SpriteBatch.Begin();
            // ---
            _level.Draw(Game.SpriteBatch);
            _player.Draw(Game.SpriteBatch);
            _level.DrawForeground(Game.SpriteBatch);
            // ---
            Game.SpriteBatch.End();
        }
    }
}
