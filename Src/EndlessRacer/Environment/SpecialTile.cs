using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class SpecialTile
    {
        private SpecialTileType _type;
        private Vector2 _position;

        public SpecialTile(SpecialTileType type, Vector2 position)
        {
            _type = type;
            _position = position;
        }

        public void Update(float scrollSPeed, Player player)
        {
            _position.Y -= scrollSPeed;

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

        public Rectangle GetHitBox()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, Constants.TileSize, Constants.TileSize);
        }
    }
}
