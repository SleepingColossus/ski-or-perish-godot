using EndlessRacer.Environment;
using EndlessRacer.GameObjects;
using EndlessRacer.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;

namespace EndlessRacer.Endless
{
    internal class EndlessMode : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Player _player;
        private Level _level;

        // UI
        private Score _score;
        private CountdownTimer _countdownTimer;
        private HealthIndicator _healthIndicator;
        private GameOverOverlay _gameOverOverlay;
        private EndlessGameState _gameState;

        private Song _bgm;

        public EndlessMode(Game game) : base(game)
        {
            _gameState = EndlessGameState.Ready;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var centerWidth = Game.Graphics.PreferredBackBufferWidth / 2;
            var centerHeight = Game.Graphics.PreferredBackBufferHeight / 2;

            var playerMoveSprite = Content.Load<Texture2D>("Player/PlayerMove");
            var playerJumpSprite = Content.Load<Texture2D>("Player/PlayerJump");
            var playerHurtSprite = Content.Load<Texture2D>("Player/PlayerHurt");
            var playerVictorySprite = Content.Load<Texture2D>("Player/PlayerVictory");
            var playerVfx = Content.Load<Texture2D>("Player/PlayerVFX");
            var playerSprites = new PlayerSprites(playerMoveSprite, playerJumpSprite, playerHurtSprite, playerVictorySprite, playerVfx);

            var playerCrashSound = Game.Content.Load<SoundEffect>("Audio/Crash");
            var playerWinSound = Game.Content.Load<SoundEffect>("Audio/Victory");
            var playerJumpSound = Game.Content.Load<SoundEffect>("Audio/Jump");
            var playerSpinSound = Game.Content.Load<SoundEffect>("Audio/Spin360");
            var playerSounds = new PlayerSounds(playerCrashSound, playerJumpSound, playerSpinSound, playerWinSound);

            var playerPosition = new Vector2(centerWidth, Constants.PlayerYPosition);
            _player = new Player(playerPosition, playerSprites, playerSounds);

            _player.FullCircleJump += Player_HandleFullCircleJump;
            _player.PlayerCrashed += Player_OnPlayerCrashed;

            var scoreSheet = Content.Load<Texture2D>("UI/Score");
            var countdownSheet = Content.Load<Texture2D>("UI/StartCounter");
            var heartSprite = Content.Load<Texture2D>("UI/Heart");
            var keysSheet = Content.Load<Texture2D>("UI/Keys");

            var gameOverText = Content.Load<Texture2D>("UI/GameOver");
            var tryAgainText = Content.Load<Texture2D>("UI/TryAgain");
            var quitText = Content.Load<Texture2D>("UI/Quit");

            _bgm = Game.Content.Load<Song>("Audio/StageTheme");

            MediaPlayer.Play(_bgm);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            _level = new EndlessLevel(LevelImporter.ImportByEntryPoint(Content));

            var screenSize = new Vector2(Game.Graphics.PreferredBackBufferWidth,
                Game.Graphics.PreferredBackBufferHeight);

            _score = new Score(scoreSheet);
            _countdownTimer = new CountdownTimer(countdownSheet, new Vector2(centerWidth, centerHeight));
            _healthIndicator = new HealthIndicator(heartSprite);
            _gameOverOverlay = new GameOverOverlay(gameOverText, tryAgainText, quitText, keysSheet, screenSize);
        }

        public override void Update(GameTime gameTime)
        {
            if (_countdownTimer.IsReady && _gameState == EndlessGameState.Ready)
            {
                _gameState = EndlessGameState.Playing;
                _player.Start();
            }
            else
            {
                _countdownTimer.Update(gameTime);
            }

            if (_gameState == EndlessGameState.Playing)
            {
                var scrollSpeed = _player.Update(gameTime);
                _level.Update(scrollSpeed, _player);
                _score.AddDistance(scrollSpeed);
            }

            if (_gameState == EndlessGameState.GameOver)
            {
                _gameOverOverlay.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.LoadMainMenu();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Game.LoadEndlessScreen();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Snow);

            Game.SpriteBatch.Begin();
            // ---

            // Environment
            _level.Draw(Game.SpriteBatch);
            _player.Draw(Game.SpriteBatch);
            _level.DrawForeground(Game.SpriteBatch);

            // UI
            _score.Draw(Game.SpriteBatch);
            _countdownTimer.Draw(Game.SpriteBatch);
            _healthIndicator.Draw(Game.SpriteBatch);
            _gameOverOverlay.Draw(Game.SpriteBatch);

            // ---
            Game.SpriteBatch.End();
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Play(_bgm);
        }

        private void Player_HandleFullCircleJump(object sender, System.EventArgs e)
        {
            _score.AddJump();
        }

        private void Player_OnPlayerCrashed(object sender, System.EventArgs e)
        {
            _healthIndicator.Damage();

            if (_healthIndicator.IsDead())
            {
                _gameState = EndlessGameState.GameOver;
                _gameOverOverlay.Show();
            }
        }
    }
}
