using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class LevelSegment
    {
        public CrossingPoint EntryPoint { get; init; }
        public CrossingPoint ExitPoint { get; init; }

        private Vector2 _position;
        private Texture2D _sprite;
        private bool[,] _obstacles;

        public LevelSegment(CrossingPoint entryPoint, CrossingPoint exitPoint, Texture2D sprite, bool[,] obstacles)
        {
            _sprite = sprite;
            _obstacles = obstacles;
            EntryPoint = entryPoint;
            ExitPoint = exitPoint;
        }

        private void Update(GameTime gameTime)
        {
            var adjustedSpeed = Gameplay.GetScrollSpeed(gameTime);

            _position.Y += adjustedSpeed;
        }

        private void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _position, Color.White);
        }

        public bool IsOffScreen()
        {
            return _position.Y < -_sprite.Height;
        }
    }
}
