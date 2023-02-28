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
        private Score _score;

        private Song _bgm;

        public EndlessMode(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var playerMoveSprite = Content.Load<Texture2D>("Player/PlayerMove");
            var playerJumpSprite = Content.Load<Texture2D>("Player/PlayerJump");
            var playerHurtSprite = Content.Load<Texture2D>("Player/PlayerHurt");
            var playerVictorySprite = Content.Load<Texture2D>("Player/PlayerVictory");
            var playerSprites = new PlayerSprites(playerMoveSprite, playerJumpSprite, playerHurtSprite, playerVictorySprite);


            var playerCrashSound = Game.Content.Load<SoundEffect>("Audio/Crash");
            var playerWinSound = Game.Content.Load<SoundEffect>("Audio/Victory");
            var playerJumpSound = Game.Content.Load<SoundEffect>("Audio/Jump");
            var playerSpinSound = Game.Content.Load<SoundEffect>("Audio/Spin360");
            var playerSounds = new PlayerSounds(playerCrashSound, playerJumpSound, playerSpinSound, playerWinSound);

            var playerPosition = new Vector2(Game.Graphics.PreferredBackBufferWidth / 2, Constants.PlayerYPosition);
            _player = new Player(playerPosition, playerSprites, playerSounds);

            var scoreSheet = Content.Load<Texture2D>("UI/Score");

            _bgm = Game.Content.Load<Song>("Audio/StageTheme");

            MediaPlayer.Play(_bgm);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            _level = new EndlessLevel(LevelImporter.ImportByEntryPoint(Content));

            _score = new Score(scoreSheet);
        }

        public override void Update(GameTime gameTime)
        {
            var scrollSpeed = _player.Update(gameTime);
            _level.Update(scrollSpeed, _player);
            _score.AddDistance(scrollSpeed);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.LoadMainMenu();
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
            _score.Draw(Game.SpriteBatch);
            // ---
            Game.SpriteBatch.End();
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Play(_bgm);
        }
    }
}
