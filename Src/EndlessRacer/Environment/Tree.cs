using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Tree
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _sprite;

        private Vector2 _position;

        public Tree(SpriteBatch spriteBatch, Texture2D sprite, Vector2 initialPosition)
        {
            _spriteBatch = spriteBatch;
            _sprite = sprite;
            _position = initialPosition;
        }

        public void Update(GameTime gameTime)
        {
            _position.Y -= Gameplay.ScrollSpeed;
        }

        public void Draw()
        {
            _spriteBatch.Draw(_sprite, _position, Color.White);
        }
    }
}
