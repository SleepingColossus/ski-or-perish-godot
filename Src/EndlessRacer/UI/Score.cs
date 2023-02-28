using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.UI
{
    internal class Score
    {
        private readonly Texture2D _spriteSheet;
        private const int DigitSize = 32;
        private const int PositionY = 10;
        private const int StartingPositionX = 950;
        private const int ScoreRate = 100; // after how many pixels to award 1 point

        public float DistanceTraveled { get; private set; }

        private int CurrentScore => (int)(DistanceTraveled / ScoreRate);

        public Score(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            DistanceTraveled = 0;
        }

        public void AddDistance(float deltaDistance)
        {
            DistanceTraveled += deltaDistance;
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
