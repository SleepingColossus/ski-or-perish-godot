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
        private const double BaseSpeed = 200f;          // speed when idle
        private const double AcceleratedSpeed = 400f;   // speed when holding down move button
        private double _speed;
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

        public float Update(GameTime gameTime)
        {
            _ks = Keyboard.GetState();

            if (_ks.IsKeyDown(Controls.TurnLeft)) { Turn(-1); }
            if (_ks.IsKeyDown(Controls.TurnRight)) { Turn(1); }

            if (_ks.IsKeyDown(Controls.Move))
            {
                _speed = AcceleratedSpeed;
            }
            else
            {
                _speed = BaseSpeed;
            }

            var ySpeed = Move(_speed, gameTime);

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

            return ySpeed;
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

        private float Move(double speed, GameTime gameTime)
        {
            var xIntensity = _angle.ToXIntensity();
            var yIntensity = _angle.ToYIntensity();

            var xSpeed = xIntensity * speed * gameTime.ElapsedGameTime.TotalSeconds;
            var ySpeed = yIntensity * speed * gameTime.ElapsedGameTime.TotalSeconds;

            _position.X += (float)xSpeed;

            // ySpeed is passed to level to set scroll speed;
            return (float)ySpeed;
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
            var location = new Point((int)_position.X, (int)_position.Y);
            var size = new Point(Constants.TileSize, Constants.TileSize);
            var rect = new Rectangle(location, size);

            return rect;
        }

        public void Crash()
        {
        }
    }
}
