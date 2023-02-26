using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace EndlessRacer.Career
{
    internal class CareerProgressScreen : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private CareerProgress _careerProgress;

        private Texture2D _background;
        private Texture2D _mapMarkerSheet;
        private MapMarker[] _markers;

        private double _timeout = 3;

        public CareerProgressScreen(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _background = Game.Content.Load<Texture2D>("Career/CareerProgressScreen");
            _mapMarkerSheet = Game.Content.Load<Texture2D>("Career/MapMarker");

            _careerProgress = CareerProgress.Get();

            _markers = new[]
            {
                new MapMarker(_mapMarkerSheet, new Vector2(395, 237)),
                new MapMarker(_mapMarkerSheet, new Vector2(510, 376)),
                new MapMarker(_mapMarkerSheet, new Vector2(142, 429)),
                new MapMarker(_mapMarkerSheet, new Vector2(485, 619)),
                new MapMarker(_mapMarkerSheet, new Vector2(245, 805)),
                new MapMarker(_mapMarkerSheet, new Vector2(527, 978)),
                new MapMarker(_mapMarkerSheet, new Vector2(799, 731)),
                new MapMarker(_mapMarkerSheet, new Vector2(1043, 942)),
                new MapMarker(_mapMarkerSheet, new Vector2(1287, 825)),
                new MapMarker(_mapMarkerSheet, new Vector2(1641, 912)),
            };

            for (int i = 0; i < _markers.Length; i++)
            {
                MapMarkerState state;

                if (i < _careerProgress.CurrentLevel)
                {
                    state = MapMarkerState.Completed;
                }
                else if (i == _careerProgress.CurrentLevel)
                {
                    state = MapMarkerState.Next;
                }
                else
                {
                    state = MapMarkerState.NotCompleted;
                }

                _markers[i].State = state;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var marker in _markers)
            {
                marker.Update();
            }

            _timeout -= gameTime.ElapsedGameTime.TotalSeconds;

            if (_timeout <= 0)
            {
                Game.LoadCareerLevel();
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
