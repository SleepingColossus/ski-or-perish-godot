using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class SpecialTile
    {
        private readonly SpecialTileType _type;
        private Vector2 _position;
        private bool _collided = false;

        public SpecialTile(SpecialTileType type, Vector2 position)
        {
            _type = type;
            _position = position;
        }

        public void Update(float scrollSPeed, Player player)
        {
            _position.Y -= scrollSPeed;

            // only process when close to the player
            if (_position.Y > Constants.PlayerYPosition * 2 && _position.Y < 0)
            {
                return;
            }

            var playerHitBox = player.GetHitBox();
            var myHitBox = GetHitBox();

            if (myHitBox.Intersects(playerHitBox))
            {
                if (_type == SpecialTileType.Obstacle)
                {
                    player.Crash();
                }

                if (_type == SpecialTileType.Jump)
                {
                    player.Jump();
                }

                if (_type == SpecialTileType.End)
                {
                    player.Win();
                }

                if (_type == SpecialTileType.Heart)
                {
                    _collided = true;
                    player.Heal();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D heartSprite)
        {
            if (_type != SpecialTileType.Heart || _collided)
            {
                return;
            }

            var sourcePosition = new Point(0, 0);
            var sourceSize = new Point(Constants.TileSize32, Constants.TileSize32);
            var sourceRectangle = new Rectangle(sourcePosition, sourceSize);

            spriteBatch.Draw(heartSprite, _position, sourceRectangle, Color.White);
        }

        public Rectangle GetHitBox()
        {
            var tileSize = _type == SpecialTileType.Obstacle ? Constants.ObstacleTileSize : Constants.TileSize64;
            var positionOffset = _type == SpecialTileType.Obstacle ? Constants.ObstaclePositionOffset : 0;

            var location = new Point((int)_position.X + positionOffset, (int)_position.Y + positionOffset);
            var size = new Point(tileSize, tileSize);
            var rect = new Rectangle(location, size);

            return rect;
        }
    }
}
