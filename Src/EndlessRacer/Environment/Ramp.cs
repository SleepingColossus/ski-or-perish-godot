﻿using EndlessRacer.Constants;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class Ramp : Obstacle
    {
        private const string UniqueSpriteName = "ramp";

        private Ramp(Vector2 initialPosition) : base(initialPosition)
        {
        }

        protected override string SpriteName()
        {
            return UniqueSpriteName;
        }

        public static Ramp BuildWithIndex(float row, float column)
        {
            var position = IndexToCoordinates(row, column, LevelSprites.Sprites[UniqueSpriteName]);

            return new Ramp(position);
        }

        public static Ramp BuildWithPosition(float horizontalIndex, float lastVerticalPosition)
        {
            var horizontalPosition = horizontalIndex * LevelSprites.Sprites[UniqueSpriteName].Width;
            var verticalPosition = lastVerticalPosition + LevelSprites.Sprites[UniqueSpriteName].Height;
            var position = new Vector2(horizontalPosition, verticalPosition);

            return new Ramp(position);
        }
    }
}