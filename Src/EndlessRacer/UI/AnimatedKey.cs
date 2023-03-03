using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.UI
{
    internal class AnimatedKey
    {
        private const int SpriteSize = Constants.TileSize128;
        private const int UnpressedKeyY = 0;
        private const int PressedKeyY = SpriteSize;

        private Vector2 _position;
        private Texture2D _spriteSheet;
        private bool _animated;
        private AnimatedKeyType _type;

        private bool _keyPressed;
        private const double FrameDuration = 1;
        private double _timeToNextFrame;

        public AnimatedKey(AnimatedKeyType type, bool animated, Vector2 position, Texture2D spriteSheet)
        {
            _position = position;
            _spriteSheet = spriteSheet;
            _animated = animated;
            _type = type;

            _timeToNextFrame = FrameDuration;
        }

        public void Update(GameTime gameTime)
        {
            if (_animated)
            {
                _timeToNextFrame -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_timeToNextFrame <= 0)
                {
                    _keyPressed = !_keyPressed;
                    _timeToNextFrame = FrameDuration;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourcePosition = new Point((int)_type * SpriteSize, _keyPressed ? PressedKeyY : UnpressedKeyY);
            var sourceSize = new Point(SpriteSize, SpriteSize);
            var sourceRectangle = new Rectangle(sourcePosition, sourceSize);

            spriteBatch.Draw(_spriteSheet, _position, sourceRectangle, Color.White);
        }
    }
}
