using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace EndlessRacer.Menu
{
    internal class MainMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Texture2D _titleScreen;
        private Texture2D _buttonSheet;

        private MenuButton[] _buttons;
        private int _minButtonIndex = 0;
        private int _maxButtonIndex;
        private int _buttonIndex;
        private KeyboardState _ks;
        private KeyboardState _ksPrevious;

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _titleScreen = Game.Content.Load<Texture2D>("Menu/MenuScreen");
            _buttonSheet = Game.Content.Load<Texture2D>("Menu/Buttons");

            _buttons = new[]
            {
                new MenuButton(_buttonSheet, new Vector2(1300, 100), MenuButtonType.PlayCareer, true),
                new MenuButton(_buttonSheet, new Vector2(1300, 300), MenuButtonType.PlayEndless),
                new MenuButton(_buttonSheet, new Vector2(1300, 500), MenuButtonType.Help),
                new MenuButton(_buttonSheet, new Vector2(1300, 700), MenuButtonType.Exit),
            };

            _maxButtonIndex = _buttons.Length - 1;
        }

        public override void Update(GameTime gameTime)
        {
            _ks = Keyboard.GetState();

            if (_ks.IsKeyDown(Keys.Up) && !_ksPrevious.IsKeyDown(Keys.Up))
            {
                _buttonIndex--;

                if (_buttonIndex < _minButtonIndex)
                {
                    _buttonIndex = _minButtonIndex;
                }
            }

            if (_ks.IsKeyDown(Keys.Down) && !_ksPrevious.IsKeyDown(Keys.Down))
            {
                _buttonIndex++;

                if (_buttonIndex > _maxButtonIndex)
                {
                    _buttonIndex = _maxButtonIndex;
                }
            }

            if (_ks.IsKeyDown(Keys.Enter))
            {
                HandleButtonPress();
            }

            foreach (var button in _buttons)
            {
                button.Disable();
            }

            _buttons[_buttonIndex].Enable();

            _ksPrevious = _ks;
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Snow);

            Game.SpriteBatch.Begin();
            // ---

            Game.SpriteBatch.Draw(_titleScreen, Vector2.Zero, Color.White);

            foreach (var button in _buttons)
            {
                button.Draw(Game.SpriteBatch);
            }

            // ---
            Game.SpriteBatch.End();
        }

        private void HandleButtonPress()
        {
            var activeButton = _buttons.First(btn => btn.Enabled);

            switch (activeButton.Type)
            {
                case MenuButtonType.PlayEndless:
                    Game.LoadEndlessScreen();
                    break;
                case MenuButtonType.PlayCareer:
                    Game.LoadCareerProgressScreen();
                    break;
                case MenuButtonType.Help:
                    // TODO implement help screen
                    break;
                case MenuButtonType.Exit:
                    Game.Exit();
                    break;
            }
        }
    }
}
