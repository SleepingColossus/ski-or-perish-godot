using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Career
{
    internal class MapMarker
    {
        private const int NotCompletedFrame = 0;
        private const int CompletedFrame = 9;

        private const int spriteW = 83;
        private const int spriteH = 50;

        private Texture2D _spriteSheet;
        private Vector2 _position;
        public MapMarkerState State { get; set; }

        private int _currentFrame = 0;
        private int _frameStep = 1;

        private const int FrameRate = 5;
        private int _animationFrame = 0;

        public MapMarker(Texture2D spriteSheet, Vector2 position)
        {
            _spriteSheet = spriteSheet;
            _position = position;
        }

        public void Update()
        {
            _animationFrame++;

            if (_animationFrame >= FrameRate)
            {
                _animationFrame = 0;

                _currentFrame += _frameStep;

                if (_currentFrame == NotCompletedFrame || _currentFrame == CompletedFrame)
                {
                    _frameStep *= -1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;

            if (State == MapMarkerState.NotCompleted)
            {
                sourceRectangle = new Rectangle(NotCompletedFrame, 0, spriteW, spriteH);
            }
            else if (State == MapMarkerState.Completed)
            {
                sourceRectangle = new Rectangle(CompletedFrame * spriteW, 0, spriteW, spriteH);
            }
            else
            {
                sourceRectangle = new Rectangle(_currentFrame * spriteW, 0, spriteW, spriteH);
            }

            spriteBatch.Draw(_spriteSheet, _position, sourceRectangle, Color.White);
        }
    }
}
