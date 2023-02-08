using EndlessRacer.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Tree
    {
        private const string SpriteName = "tree";
        private readonly Texture2D _sprite;

        private Vector2 _position;
        public float Height => _position.Y;

        private Tree(Vector2 initialPosition)
        {
            _sprite = LevelSprites.Sprites[SpriteName];
            _position = initialPosition;
        }

        public static Tree BuildWithIndex(float row, float column)
        {
            var position = IndexToCoordinates(row, column, LevelSprites.Sprites[SpriteName]);

            return new Tree(position);
        }

        public static Tree BuildWithPosition(float horizontalIndex, float lastVerticalPosition)
        {
            var horizontalPosition = horizontalIndex * LevelSprites.Sprites[SpriteName].Width;
            var verticalPosition = lastVerticalPosition + LevelSprites.Sprites[SpriteName].Height;
            var position = new Vector2(horizontalPosition, verticalPosition);

            return new Tree(position);
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
