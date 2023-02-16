using Microsoft.Xna.Framework;

namespace EndlessRacer
{
    internal static class Gameplay
    {
        private const int BaseScrollSpeed = 100;

        public static float GetScrollSpeed(GameTime gameTime)
        {
            return BaseScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
