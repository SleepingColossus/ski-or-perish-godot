using EndlessRacer.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRacer.GameObjects
{
    internal class Player
    {
        private const string SpriteMoving = "player-moving";
        private const string SpriteJumping = "player-jumping";
        private Texture2D _sprite;

        private Vector2 _position;
        private readonly float _baseSpeed = 100f;
        private KeyboardState _ks;

        private PlayerState _state;

        private const double JumpDuration = 2.5;
        private double _jumpTimeRemaining;

        public Player(Vector2 initialPosition)
        {
            _sprite = LevelSprites.Sprites[SpriteMoving];
            _position = initialPosition;
            _state = PlayerState.Moving;
        }

        public void Update(GameTime gameTime)
        {
            _ks = Keyboard.GetState();

            var adjustedSpeed = _baseSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_ks.IsKeyDown(Keys.Left))
            {
                _position.X -= adjustedSpeed;
            }

            if (_ks.IsKeyDown(Keys.Right))
            {
                _position.X += adjustedSpeed;
            }

            if (_state == PlayerState.Jumping)
            {
                _jumpTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_jumpTimeRemaining <= 0)
                {
                    ChangeState(PlayerState.Moving);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _position, Color.White);
        }

        public void Jump()
        {
            if (_state == PlayerState.Moving)
            {
                ChangeState(PlayerState.Jumping);

                _jumpTimeRemaining = JumpDuration;
            }
        }

        private void ChangeState(PlayerState newState)
        {
            switch (newState)
            {
                case PlayerState.Moving:
                    _state = newState;
                    _sprite = LevelSprites.Sprites[SpriteMoving];
                    break;
                case PlayerState.Jumping:
                    _state = newState;
                    _sprite = LevelSprites.Sprites[SpriteJumping];
                    break;
            }
        }

        public Rectangle GetRectangle()
        {
            var rect = _sprite.Bounds;
            rect.X = (int)_position.X;
            rect.Y = (int)_position.Y;
            return rect;
        }
    }
}
