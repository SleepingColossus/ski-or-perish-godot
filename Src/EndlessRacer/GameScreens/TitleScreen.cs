using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace EndlessRacer.GameScreens
{
    internal class TitleScreen : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Texture2D _titleScreen;

        public TitleScreen(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _titleScreen = Game.Content.Load<Texture2D>("TitleScreen");
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: main menu controls
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Snow);

            Game.SpriteBatch.Begin();
            // ---
            Game.SpriteBatch.Draw(_titleScreen, Vector2.Zero, Color.White);
            // ---
            Game.SpriteBatch.End();
        }
    }
}
