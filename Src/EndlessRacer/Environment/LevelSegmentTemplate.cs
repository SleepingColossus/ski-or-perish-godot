using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class LevelSegmentTemplate
    {
        public CrossingPoint EntryPoint { get; init; }
        public CrossingPoint ExitPoint { get; init; }
        public Texture2D Sprite { get; init; }
        public bool[,] ObstacleData { get; init; }

        public LevelSegmentTemplate(CrossingPoint entryPoint, CrossingPoint exitPoint, Texture2D sprite, bool[,] obstacleData)
        {
            EntryPoint = entryPoint;
            ExitPoint = exitPoint;
            Sprite = sprite;
            ObstacleData = obstacleData;
        }
    }
}
