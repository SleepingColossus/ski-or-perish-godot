using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.UI
{
    internal class GameOverOverlay
    {
        private bool _visible = false;

        private readonly Texture2D _gameOverTextSprite;
        private readonly Vector2 _gameOverTextPosition;

        private readonly Texture2D _tryAgainTextSprite;
        private readonly Vector2 _tryAgainTextPosition;

        private readonly Texture2D _quitTextSprite;
        private readonly Vector2 _quitTextPosition;

        private readonly AnimatedKey _escKey;
        private readonly AnimatedKey _rKey;

        public GameOverOverlay(Texture2D gameOverTextSprite, Texture2D tryAgainTextSprite, Texture2D quitTextSprite,
            Texture2D animatedKeys, Vector2 screenSize)
        {
            _gameOverTextSprite = gameOverTextSprite;
            _tryAgainTextSprite = tryAgainTextSprite;
            _quitTextSprite = quitTextSprite;

            _gameOverTextPosition = new Vector2(screenSize.X / 2 - _gameOverTextSprite.Width / 2,
                                                screenSize.Y / 4 - _gameOverTextSprite.Height / 2);
            _tryAgainTextPosition = new Vector2(screenSize.X / 2 - _tryAgainTextSprite.Width / 2 - _tryAgainTextSprite.Width,
                                                screenSize.Y / 2 - _tryAgainTextSprite.Height / 2);
            _quitTextPosition = new Vector2(screenSize.X / 2 - _quitTextSprite.Width / 2 + _quitTextSprite.Width,
                                            screenSize.Y / 2 - _quitTextSprite.Height / 2);

            var rKeyPosition = new Vector2(_tryAgainTextPosition.X + _tryAgainTextSprite.Width / 4,
                                           _tryAgainTextPosition.Y + Constants.TileSize128);
            _rKey = new AnimatedKey(AnimatedKeyType.R, true, rKeyPosition, animatedKeys);

            var escKeyPosition = new Vector2(_quitTextPosition.X + _quitTextSprite.Width / 4,
                                             _quitTextPosition.Y + Constants.TileSize128);
            _escKey = new AnimatedKey(AnimatedKeyType.Esc, true, escKeyPosition, animatedKeys);
        }

        public void Update(GameTime gameTime)
        {
            if (_visible)
            {
                _escKey.Update(gameTime);
                _rKey.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                _escKey.Draw(spriteBatch);
                _rKey.Draw(spriteBatch);

                spriteBatch.Draw(_gameOverTextSprite, _gameOverTextPosition, Color.White);
                spriteBatch.Draw(_tryAgainTextSprite, _tryAgainTextPosition, Color.White);
                spriteBatch.Draw(_quitTextSprite, _quitTextPosition, Color.White);
            }
        }

        public void Show() => _visible = true;
    }
}
