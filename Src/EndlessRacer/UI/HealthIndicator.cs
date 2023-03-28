using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.UI
{
    internal class HealthIndicator
    {
        private const int HeartTileSize = Constants.TileSize32;
        private const int FullHeartX = 0;
        private const int EmptyHeartX = HeartTileSize;
        private const int MaxHealth = 3;

        private readonly Vector2 _startingPosition = new Vector2(100, 10);
        private Texture2D _spriteSheet;
        private int _currentHealth;

        public HealthIndicator(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            _currentHealth = MaxHealth;
        }

        public void Damage()
        {
            _currentHealth--;
        }

        public void Heal()
        {
            _currentHealth++;

            if (_currentHealth > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
        }

        public bool IsDead()
        {
            return _currentHealth <= 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MaxHealth; i++)
            {
                var fullHeart = _currentHealth >= i + 1;

                var sourceX = fullHeart ? FullHeartX : EmptyHeartX;
                var sourcePosition = new Point(sourceX, 0);
                var sourceSize = new Point(HeartTileSize, HeartTileSize);

                var positionX = _startingPosition.X + i * HeartTileSize;
                var positionY = _startingPosition.Y;
                var position = new Vector2(positionX, positionY);

                var sourceRectangle = new Rectangle(sourcePosition, sourceSize);

                spriteBatch.Draw(_spriteSheet, position, sourceRectangle, Color.White);
            }
        }
    }
}
