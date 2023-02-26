using EndlessRacer.Career;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace EndlessRacer.GameScreens
{
    internal class CareerProgressScreen : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Texture2D _background;
        private Texture2D _mapMarkerSheet;
        private MapMarker[] _markers;

        public CareerProgressScreen(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _background = Game.Content.Load<Texture2D>("Career/CareerProgressScreen");
            _mapMarkerSheet = Game.Content.Load<Texture2D>("Career/MapMarker");


            _markers = new[]
            {
                new MapMarker(_mapMarkerSheet, new Vector2(395, 237), MapMarkerState.Next),
                new MapMarker(_mapMarkerSheet, new Vector2(510, 376), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(142, 429), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(485, 619), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(245, 805), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(527, 978), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(799, 731), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(1043, 942), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(1287, 825), MapMarkerState.NotCompleted),
                new MapMarker(_mapMarkerSheet, new Vector2(1641, 912), MapMarkerState.NotCompleted),
            };
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var marker in _markers)
            {
                marker.Update();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Snow);

            Game.SpriteBatch.Begin();
            // ---

            Game.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);

            foreach (var marker in _markers)
            {
                marker.Draw(Game.SpriteBatch);
            }

            // ---
            Game.SpriteBatch.End();
        }
    }
}
