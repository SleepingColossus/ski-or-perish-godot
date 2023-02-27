using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class LevelSegmentTemplate
    {
        public CrossingPoint EntryPoint { get; init; }
        public CrossingPoint ExitPoint { get; init; }
        public Texture2D Background { get; init; }
        public Texture2D Foreground { get; init; }
        public int[,] SpecialTileData { get; init; }

        public LevelSegmentTemplate(CrossingPoint entryPoint, CrossingPoint exitPoint, Texture2D background, int[,] specialTileData, Texture2D foreground=null)
        {
            EntryPoint = entryPoint;
            ExitPoint = exitPoint;
            Background = background;
            SpecialTileData = specialTileData;
            Foreground = foreground;
        }
    }
}
