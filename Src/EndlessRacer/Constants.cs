using Microsoft.Xna.Framework;

namespace EndlessRacer
{
    internal static class Constants
    {
        private const int BaseScrollSpeed = 0;
        public const int TileSize = 64;

        public const int NumberOfRows = 18;
        public const int NumberOfColumns = 30;

        public static float GetScrollSpeed(GameTime gameTime)
        {
            return BaseScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
