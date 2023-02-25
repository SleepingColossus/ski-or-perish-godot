using System.Collections.Generic;
using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment;

internal abstract class Level
{
    protected const int SegmentHeight = Constants.NumberOfRows * Constants.TileSize;

    protected readonly List<LevelSegment> Segments;

    protected Level()
    {
        Segments = new List<LevelSegment>();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var segment in Segments)
        {
            segment.Draw(spriteBatch);
        }
    }

    public abstract void Update(float scrollSpeed, Player player);
}