using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class Obstacle
    {
        private Vector2 _position;

        public Obstacle(Vector2 position)
        {
            _position = position;
        }

        public void Update(GameTime gameTime, Player player)
        {
            var adjustedSpeed = Constants.GetScrollSpeed(gameTime);

            _position.Y -= adjustedSpeed;

            var playerHitBox = player.GetHitBox();
            var myHitBox = GetHitBox();

            if (myHitBox.Intersects(playerHitBox))
            {
                player.Crash();
            }
        }

        public Rectangle GetHitBox()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, Constants.TileSize, Constants.TileSize);
        }
    }
}
