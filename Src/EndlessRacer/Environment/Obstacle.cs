using EndlessRacer.Constants;
using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal abstract class Obstacle
    {
        //private const string SpriteName = "tree";
        private readonly Texture2D _sprite;

        private Vector2 _position;
        public float Height => _position.Y;

        protected Obstacle(Vector2 initialPosition)
        {
            _sprite = LevelSprites.Sprites[SpriteName()];
            _position = initialPosition;
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            _position.Y -= Gameplay.ScrollSpeed;
        }

        public void Draw()
        {
            EngineComponents.SpriteBatch.Draw(_sprite, _position, Color.White);
        }

        public bool IsOffScreen() => _position.Y < -_sprite.Height;

        protected static Vector2 IndexToCoordinates(float row, float col, Texture2D sprite)
        {
            var bounds = sprite.Bounds;

            var x = col * bounds.Size.X;
            var y = row * bounds.Size.Y;

            return new Vector2(x, y);
        }

        protected abstract string SpriteName();

        protected Rectangle GetRectangle()
        {
            var rect = _sprite.Bounds;
            rect.X = (int)_position.X;
            rect.Y = (int)_position.Y;
            return rect;
        }
    }
}
