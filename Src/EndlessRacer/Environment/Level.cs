using System;
using System.Collections.Generic;
using System.Linq;
using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Level
    {
        private const int SegmentHeight = Constants.NumberOfRows * Constants.TileSize;

        private readonly List<LevelSegment> _segments;
        private readonly Dictionary<CrossingPoint, List<LevelSegmentTemplate>> _templates;
        private readonly Random _random;

        public Level(Dictionary<CrossingPoint, List<LevelSegmentTemplate>> templates)
        {
            _segments = new List<LevelSegment>();
            _templates = templates;
            _random = new Random();

            InitLevel();
        }

        public void Update(GameTime gameTime, Player player)
        {
            foreach (var segment in _segments)
            {
                segment.Update(gameTime, player);
            }

            if (_segments.First().IsOffScreen())
            {
                ExtendLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var segment in _segments)
            {
                segment.Draw(spriteBatch);
            }
        }

        private void InitLevel()
        {
            AppendSegment(CrossingPoint.Center, 0, 0);
            AppendSegment(CrossingPoint.Center, SegmentHeight);
        }

        private void ExtendLevel()
        {
            // delete first segment
            _segments.RemoveAt(0);

            // check type of last segment
            var lastSegment = _segments.Last();
            AppendSegment(lastSegment.ExitPoint, lastSegment.GetY);
        }

        private void AppendSegment(CrossingPoint lastExitPoint, float lastYPosition, int? offset=null)
        {
            // get random segment that matches end of last segment
            var matchingTemplates = _templates[lastExitPoint];

            var x = 0;
            var y = lastYPosition + offset ?? SegmentHeight;
            var position = new Vector2(x, y);

            var randomIndex = _random.Next(matchingTemplates.Count);
            var nextTemplate = matchingTemplates[randomIndex];
            var nextSegment = new LevelSegment(position, nextTemplate);

            _segments.Add(nextSegment);
        }
    }
}
