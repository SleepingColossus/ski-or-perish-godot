using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class SpecialTile
    {
        private SpecialTileType _type;
        private Vector2 _position;

        private Texture2D _debugRect;

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
                    // TODO: handle case
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // only process when close to the player
            if (_position.Y > Constants.PlayerYPosition * 2 || _position.Y < 0)
            {
                return;
            }

            Color color;

            if (_type == SpecialTileType.Obstacle)  { color = Color.Red; }
            else if (_type == SpecialTileType.Jump) { color = Color.Green; }
            else if (_type == SpecialTileType.End)  { color = Color.Magenta; }
            else { color = Color.White; }

            _debugRect = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _debugRect.SetData(new Color[] { color});

            spriteBatch.Draw(_debugRect, GetHitBox(), Color.White);
        }

        public Rectangle GetHitBox()
        {
            var location = new Point((int)_position.X, (int)_position.Y);
            var size = new Point(Constants.TileSize, Constants.TileSize);
            var rect = new Rectangle(location, size);

            return rect;
        }
    }
}
