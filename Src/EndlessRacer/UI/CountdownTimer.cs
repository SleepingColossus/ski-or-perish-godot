using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.UI
{
    internal class CountdownTimer
    {
        private enum CountdownTimerState
        {
            Three,
            Two,
            One,
            Go,
        }

        private Texture2D _spriteSheet;
        private Vector2 _position;
        private CountdownTimerState _state;

        private const double CountDownInterval = 1;
        private double _timeToNextState;

        private const int SpriteSize = Constants.TileSize128;

        public CountdownTimer(Texture2D spriteSheet, Vector2 position)
        {
            _spriteSheet = spriteSheet;
            _position = new Vector2(position.X - SpriteSize / 2, position.Y - SpriteSize / 2);
            _state = CountdownTimerState.Three;
            _timeToNextState = CountDownInterval;
        }

        public void Update(GameTime gameTime)
        {
            if (IsReady)
            {
                return;
            }

            _timeToNextState -= gameTime.ElapsedGameTime.TotalSeconds;

            if (_timeToNextState <= 0)
            {
                _state++;
                _timeToNextState = CountDownInterval;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsReady)
            {
                return;
            }

            var sourceRectangle = new Rectangle((int)_state * SpriteSize, 0, SpriteSize, SpriteSize);

            spriteBatch.Draw(_spriteSheet, _position, sourceRectangle, Color.White);
        }

        public bool IsReady => _state > CountdownTimerState.Go;
    }
}
