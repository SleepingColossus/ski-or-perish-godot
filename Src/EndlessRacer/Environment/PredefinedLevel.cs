using System.Collections.Generic;
using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class PredefinedLevel : Level
    {
        public PredefinedLevel(List<LevelSegmentTemplate> templates)
        {
            for (var i = 0; i < templates.Count; i++)
            {
                var template = templates[i];

                var x = 0;
                var y = SegmentHeight * i;
                var position = new Vector2(x, y);

                var segment = new LevelSegment(position, template);

                Segments.Add(segment);
            }
        }

        public override void Update(float scrollSpeed, Player player)
        {
            foreach (var segment in Segments)
            {
                segment.Update(scrollSpeed, player);
            }
        }
    }
}
