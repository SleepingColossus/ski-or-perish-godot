using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class SpecialTile
    {
        private Vector2 _position;

        public SpecialTile(Vector2 position)
        {
            _position = position;
        }

        public void Update(float scrollSPeed, Player player)
        {
            _position.Y -= scrollSPeed;

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
