using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.UI
{
    internal class Score
    {
        private readonly Texture2D _spriteSheet;
        private const int DigitSize = Constants.TileSize32;
        private const int PositionY = 10;
        private const int StartingPositionX = 950;
        private const int ScoreRate = 100; // after how many pixels to award 1 point
        private const int JumpRate = 25;

        public float _distanceTraveled;
        private int _timesJumped;

        private int CurrentScore => (int)(_distanceTraveled / ScoreRate) + _timesJumped * JumpRate;

        public Score(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            _distanceTraveled = 0;
            _timesJumped = 0;
        }

        public void AddDistance(float deltaDistance)
        {
            _distanceTraveled += deltaDistance;
        }

        public void AddJump()
        {
            _timesJumped++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int[] digits = CurrentScore.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();

            for (int i = 0; i < digits.Length; i++)
            {
                var digit = digits[^(i+1)];

                var positionX = StartingPositionX - i * DigitSize;

                DrawDigit(spriteBatch, positionX, digit);
            }
        }

        private void DrawDigit(SpriteBatch spriteBatch, int positionX, int digit)
        {
            var sourcePosition = new Point(digit * DigitSize, 0);
            var sourceSize = new Point(DigitSize, DigitSize);
            var sourceRectangle = new Rectangle(sourcePosition, sourceSize);

            var position = new Vector2(positionX, PositionY);

            spriteBatch.Draw(_spriteSheet, position, sourceRectangle, Color.White);
        }
    }
}
