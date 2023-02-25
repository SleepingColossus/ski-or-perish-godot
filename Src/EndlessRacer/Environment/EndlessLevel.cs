using System;
using System.Collections.Generic;
using System.Linq;
using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class EndlessLevel : Level
    {
        private readonly Dictionary<CrossingPoint, List<LevelSegmentTemplate>> _templates;
        private readonly Random _random;

        public EndlessLevel(Dictionary<CrossingPoint, List<LevelSegmentTemplate>> templates)
        {
            _templates = templates;
            _random = new Random();

            InitLevel();
        }

        public override void Update(float scrollSpeed, Player player)
        {
            foreach (var segment in Segments)
            {
                segment.Update(scrollSpeed, player);
            }

            if (Segments.First().IsOffScreen())
            {
                ExtendLevel();
            }
        }

        private void InitLevel()
        {
            AppendSegment(CrossingPoint.Center, 0, 0);
            AppendSegment(Segments[0].ExitPoint, SegmentHeight);
        }

        private void ExtendLevel()
        {
            // delete first segment
            Segments.RemoveAt(0);

            // check type of last segment
            var lastSegment = Segments.Last();
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

            Segments.Add(nextSegment);
        }
    }
}
