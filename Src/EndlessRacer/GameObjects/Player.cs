using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRacer.GameObjects
{
    internal class Player
    {
        private Texture2D _sprite;
        private readonly Texture2D _spriteMove;
        private readonly Texture2D _spriteJump;
        private readonly Texture2D _spriteHurt;
        private readonly Texture2D _spriteVictory;

        private Vector2 _position;
        private const double BaseSpeed = 200f;          // speed when idle
        private const double AcceleratedSpeed = 400f;   // speed when holding down move button
        private Vector2 _speed;
        private KeyboardState _ks;

        private PlayerState _currentState;
        private Angle _angle;

        private const double JumpDuration = 1.5;
        private double _jumpTimeRemaining;

        private const double HurtDuration = 1.5;
        private double _hurtTimeRemaining;

        private const double InvincibleDuration = 1.5;
        private double _invincibleRemaining;
        private int _frame = 0;

        private const double TurnIntervalGround = 0.1f;
        private const double TurnIntervalAir = 0.05f;
        private double _turnInterval;
        private double _turnTimer;
        private bool _canTurn;

        public Player(Vector2 initialPosition, Texture2D spriteMove, Texture2D spriteJump, Texture2D spriteHurt, Texture2D spriteVictory)
        {
            _position = initialPosition;
            _currentState = PlayerState.Moving;

            _angle = Angle.Down;

            _spriteMove = spriteMove;
            _spriteJump = spriteJump;
            _spriteHurt = spriteHurt;
            _spriteVictory = spriteVictory;

            ChangeState(PlayerState.Moving);
        }

        public float Update(GameTime gameTime)
        {
            _ks = Keyboard.GetState();

            if (_currentState != PlayerState.Hurt)
            {
                if (_ks.IsKeyDown(Controls.TurnLeft)) { Turn(-1); }
                if (_ks.IsKeyDown(Controls.TurnRight)) { Turn(1); }
            }

            double speed;

            if (_ks.IsKeyDown(Controls.Move))
            {
                speed = AcceleratedSpeed;
            }
            else
            {
                speed = BaseSpeed;
            }

            if (_currentState == PlayerState.Moving || _currentState == PlayerState.Invincible)
            {
                Move(speed, gameTime);
            }

            if (_currentState == PlayerState.Invincible)
            {
                _invincibleRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_invincibleRemaining <= 0)
                {
                    ChangeState(PlayerState.Moving);
                }
            }

            _position.X += _speed.X;

            if (_currentState == PlayerState.Jumping)
            {
                _jumpTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_jumpTimeRemaining <= 0)
                {
                    if (_angle >= Angle.Left && _angle <= Angle.Right)
                    {
                        ChangeState(PlayerState.Moving);
                    }
                    else
                    {
                        ChangeState(PlayerState.Hurt);
                    }
                }
            }

            if (_currentState == PlayerState.Hurt)
            {
                _speed = Vector2.Zero;

                _hurtTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                if (_hurtTimeRemaining <= 0)
                {
                    ChangeState(PlayerState.Invincible);
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

            _frame++;

            // Y speed is passed to level to set scroll speed;
            return _speed.Y;
        }

        // turn direction:
        // -1 -> Left
        // +1 -> Right
        private void Turn(int turnDirection)
        {
            if (_canTurn)
            {
                _angle += turnDirection;

                if (_currentState == PlayerState.Moving || _currentState == PlayerState.Invincible)
                {
                    _angle = (Angle)Math.Clamp((int)_angle, (int)Angle.Left, (int)Angle.Right);
                }
                else if (_currentState == PlayerState.Jumping)
                {
                    if (_angle < 0)
                    {
                        _angle = Angle.LeftUp1;
                    }

                    if (_angle > Angle.LeftUp1)
                    {
                        _angle = Angle.Left;
                    }
                }

                _canTurn = false;
                _turnTimer = _turnInterval;
            }
        }

        private void Move(double speed, GameTime gameTime)
        {
            var xIntensity = _angle.ToXIntensity();
            var yIntensity = _angle.ToYIntensity();

            var xSpeed = (float)(xIntensity * speed * gameTime.ElapsedGameTime.TotalSeconds);
            var ySpeed = (float)(yIntensity * speed * gameTime.ElapsedGameTime.TotalSeconds);

            _speed = new Vector2(xSpeed, ySpeed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = new Rectangle((int)_angle * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize);

            // flicker when invincible
            if (_currentState == PlayerState.Invincible)
            {
                if (_frame % 2 == 0)
                {
                    return;
                }
            }

            if (_currentState == PlayerState.Hurt) // draw without source rectangle if hurt
            {
                spriteBatch.Draw(_sprite, _position, Color.White);
            }
            else
            {
                spriteBatch.Draw(_sprite, _position, sourceRectangle, Color.White);
            }
        }

        public void Jump()
        {
            if (_currentState == PlayerState.Moving || _currentState == PlayerState.Invincible)
            {
                ChangeState(PlayerState.Jumping);
            }
        }

        private void ChangeState(PlayerState newState)
        {
            _currentState = newState;

            switch (newState)
            {
                case PlayerState.Moving:
                    _sprite = _spriteMove;
                    _turnInterval = TurnIntervalGround;
                    break;
                case PlayerState.Invincible:
                    _sprite = _spriteMove;
                    _invincibleRemaining = InvincibleDuration;
                    _turnInterval = TurnIntervalGround;
                    break;
                case PlayerState.Jumping:
                    _sprite = _spriteJump;
                    _jumpTimeRemaining = JumpDuration;
                    _turnInterval = TurnIntervalAir;
                    break;
                case PlayerState.Hurt:
                    _sprite = _spriteHurt;
                    _hurtTimeRemaining = HurtDuration;
                    _angle = Angle.Down;
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
            if (_currentState == PlayerState.Moving)
            {
                ChangeState(PlayerState.Hurt);
            }
        }
    }
}
