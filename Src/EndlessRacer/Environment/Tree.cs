using EndlessRacer.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Tree
    {
        private readonly Texture2D _sprite;

        private Vector2 _position;

        public Tree(Texture2D sprite, Vector2 initialPosition)
        {
            _sprite = sprite;
            _position = initialPosition;
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
    }
}
