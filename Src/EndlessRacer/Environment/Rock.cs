using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class Rock : Obstacle
    {
        private const string UniqueSpriteName = "rock";

        private Rock(Vector2 initialPosition) : base(initialPosition)
        {
        }

        protected override string SpriteName()
        {
            return UniqueSpriteName;
        }

        public static Rock BuildWithIndex(float row, float column)
        {
            var position = IndexToCoordinates(row, column, LevelSprites.Sprites[UniqueSpriteName]);

            return new Rock(position);
        }

        public static Rock BuildWithPosition(float horizontalIndex, float lastVerticalPosition)
        {
            var horizontalPosition = horizontalIndex * LevelSprites.Sprites[UniqueSpriteName].Width;
            var verticalPosition = lastVerticalPosition + LevelSprites.Sprites[UniqueSpriteName].Height;
            var position = new Vector2(horizontalPosition, verticalPosition);

            return new Rock(position);
        }
    }
}