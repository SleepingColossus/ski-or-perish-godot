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
        private List<LevelSegment> _segments;
        private Dictionary<CrossingPoint, List<LevelSegmentTemplate>> _templates;
        private Random _random;

        public Level()
        {
            _random = new Random();
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

        public void ExtendLevel()
        {
            // delete first segment
            _segments.RemoveAt(0);

            // check type of last segment
            var lastSegment = _segments.Last();
            var lastType = lastSegment.ExitPoint;

            // get random segment that matches end of last segment
            var matchingTemplates = _templates[lastType];
            var randomIndex = _random.Next(matchingTemplates.Count);
            var nextTemplate = matchingTemplates[randomIndex];

            var x = 0;
            var y = lastSegment.GetY + Constants.NumberOfRows * Constants.TileSize;
            var position = new Vector2(x, y);

            var nextSegment = new LevelSegment(position, nextTemplate);

            _segments.Add(nextSegment);
        }
    }
}
