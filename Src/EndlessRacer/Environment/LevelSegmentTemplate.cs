using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class LevelSegmentTemplate
    {
        public CrossingPoint EntryPoint { get; init; }
        public CrossingPoint ExitPoint { get; init; }
        public Texture2D Sprite { get; init; }
        public int[,] SpecialTileData { get; init; }

        public LevelSegmentTemplate(CrossingPoint entryPoint, CrossingPoint exitPoint, Texture2D sprite, int[,] specialTileData)
        {
            EntryPoint = entryPoint;
            ExitPoint = exitPoint;
            Sprite = sprite;
            SpecialTileData = specialTileData;
        }
    }
}
