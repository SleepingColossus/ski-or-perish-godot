using EndlessRacer.Constants;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class Tree : Obstacle
    {
        private const string UniqueSpriteName = "tree";

        private Tree(Vector2 initialPosition) : base(initialPosition)
        {
        }

        protected override string SpriteName()
        {
            return UniqueSpriteName;
        }

        public static Tree BuildWithIndex(float row, float column)
        {
            var position = IndexToCoordinates(row, column, LevelSprites.Sprites[UniqueSpriteName]);

            return new Tree(position);
        }

        public static Tree BuildWithPosition(float horizontalIndex, float lastVerticalPosition)
        {
            var horizontalPosition = horizontalIndex * LevelSprites.Sprites[UniqueSpriteName].Width;
            var verticalPosition = lastVerticalPosition + LevelSprites.Sprites[UniqueSpriteName].Height;
            var position = new Vector2(horizontalPosition, verticalPosition);

            return new Tree(position);
        }
    }
}
