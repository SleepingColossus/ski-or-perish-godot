using EndlessRacer.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Rock
    {
        private const string SpriteName = "rock";
        private readonly Texture2D _sprite;

        private Vector2 _position;
        public float Height => _position.Y;

        private Rock(Vector2 initialPosition)
        {
            _sprite = LevelSprites.Sprites[SpriteName];
            _position = initialPosition;
        }

        public static Rock BuildWithIndex(float row, float column)
        {
            var position = IndexToCoordinates(row, column, LevelSprites.Sprites[SpriteName]);

            return new Rock(position);
        }

        public static Rock BuildWithPosition(float horizontalIndex, float lastVerticalPosition)
        {
            var horizontalPosition = horizontalIndex * LevelSprites.Sprites[SpriteName].Width;
            var verticalPosition = lastVerticalPosition + LevelSprites.Sprites[SpriteName].Height;
            var position = new Vector2(horizontalPosition, verticalPosition);

            return new Rock(position);
        }

        public void Update(GameTime gameTime)
        {
            _position.Y -= Gameplay.ScrollSpeed;
        }

        public void Draw()
        {
            EngineComponents.SpriteBatch.Draw(_sprite, _position, Color.White);
        }

        public bool IsOffScreen() => _position.Y < -_sprite.Height;

        private static Vector2 IndexToCoordinates(float row, float col, Texture2D sprite)
        {
            var bounds = sprite.Bounds;

            var x = col * bounds.Size.X;
            var y = row * bounds.Size.Y;

            return new Vector2(x, y);
        }
    }
}