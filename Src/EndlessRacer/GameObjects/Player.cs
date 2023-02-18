using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRacer.GameObjects
{
    internal class Player
    {
        private readonly Texture2D _sprite;

        private Vector2 _position;
        private readonly float _baseSpeed = 100f;
        private KeyboardState _ks;

        private PlayerState _state;
        private Angle _angle;

        private const double JumpDuration = 2.5;
        private double _jumpTimeRemaining;

        private const double TurnInterval = 0.1f;
        private double _turnTimer;
        private bool _canTurn;

        public Player(Vector2 initialPosition, Texture2D sprite)
        {
            _sprite = sprite;
            _position = initialPosition;
            _state = PlayerState.Moving;
            _angle = Angle.Down;

            _turnTimer = TurnInterval;
        }

        public void Update(GameTime gameTime)
        {
            _ks = Keyboard.GetState();

            var adjustedSpeed = _baseSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_ks.IsKeyDown(Controls.TurnLeft)) { Turn(-1); }
            if (_ks.IsKeyDown(Controls.TurnRight)) { Turn(1); }

            if (_state == PlayerState.Jumping)
            {
                _jumpTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_jumpTimeRemaining <= 0)
                {
                    ChangeState(PlayerState.Moving);
                }
            }

            if (!_canTurn)
            {
                _turnTimer -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_turnTimer <= 0)
                {
                    _canTurn = true;
                }
            }
        }

        // turn direction:
        // -1 -> Left
        // +1 -> Right
        private void Turn(int turnDirection)
        {
            if (_canTurn)
            {
                _angle += turnDirection;

                _angle = (Angle)Math.Clamp((int)_angle, (int)Angle.Left, (int)Angle.Right);

                _canTurn = false;
                _turnTimer = TurnInterval;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = new Rectangle((int)_angle * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize);

            spriteBatch.Draw(_sprite, _position, sourceRectangle, Color.White);
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
                    //_sprite = LevelSprites.Sprites[SpriteMoving];
                    break;
                case PlayerState.Jumping:
                    _state = newState;
                    //_sprite = LevelSprites.Sprites[SpriteJumping];
                    break;
            }
        }

        public Rectangle GetHitBox()
        {
            var rect = _sprite.Bounds;
            rect.X = (int)_position.X;
            rect.Y = (int)_position.Y;
            return rect;
        }

        public void Crash()
        {
            System.Diagnostics.Debug.WriteLine("I crashed! B(");
        }
    }
}
