using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace EndlessRacer.Environment
{
    internal class Level
    {
        private const int NumberOfRows = 18;
        private const int RowLength = 30;

        private const int InitialGap = 24;
        private int _currentGap;

        private const int MinOffset = -2;
        private const int MaxOffset = 2;
        private const int OffsetStep = 1;
        private int _currentOffset;
        private Random _random;

        private readonly List<Obstacle[]> _row;

        private const int RockInterval = 10;
        private int _rowsUntilRock = RockInterval;

        private const int RampInterval = 8;
        private int _rowsUntilRamp = RampInterval;

        public Level()
        {
            _currentGap = InitialGap;
            _currentOffset = 0;
            _random = new Random();

            _row = new List<Obstacle[]>();

            // initialize obstacles
            for (int row = 0; row < NumberOfRows; row++)
            {
                _row.Add(InitializeRow(row, InitMode.Row));
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int row = 0; row < _row.Count; row++)
            {
                for (int col = 0; col < _row[row].Length; col++)
                {
                    if (_row[row][col] != null)
                    {
                        _row[row][col].Update(gameTime);
                    }
                }
            }

            // is the first row out of bounds?
            var outOfBounds = _row[0][0].IsOffScreen();

            // if so, remove first row and add new row on bottom
            if (outOfBounds)
            {
                _row.RemoveAt(0);

                // get Y coord of last row
                var lastHeight = _row[^1][0].Height;

                var newRow = InitializeRow(lastHeight, InitMode.Height);

                _row.Add(newRow);
                SpawnRock(_row.Last());
                SpawnRamp(_row.Last());
            }
        }

        public void Draw()
        {
            for (int row = 0; row < _row.Count; row++)
            {
                for (int col = 0; col < _row[row].Length; col++)
                {
                    if (_row[row][col] != null)
                    {
                        _row[row][col].Draw();
                    }
                }
            }
        }

        private enum InitMode
        {
            Row,   // index in list
            Height // position in pixels
        }

        private Obstacle[] InitializeRow(float verticalPosition, InitMode mode)
        {
            var trees = new Obstacle[RowLength];

            var cellsToFill = (RowLength - _currentGap) / 2;

            // fill left side
            for (var i = 0; i < cellsToFill + _currentOffset; i++)
            {
                if (mode == InitMode.Row)
                {
                    trees[i] = Tree.BuildWithIndex(verticalPosition, i);
                }
                else
                {
                    trees[i] = Tree.BuildWithPosition(i, verticalPosition);
                }
            }

            // fill left side
            for (var i = RowLength - cellsToFill - 1 + _currentOffset; i < RowLength; i++)
            {
                if (mode == InitMode.Row)
                {
                    trees[i] = Tree.BuildWithIndex(verticalPosition, i);
                }
                else
                {
                    trees[i] = Tree.BuildWithPosition(i, verticalPosition);
                }
            }

            var offsetChange = _random.Next(-OffsetStep, OffsetStep + 1);

            _currentOffset += offsetChange;

            if (_currentOffset < MinOffset)
            {
                _currentOffset = MinOffset;
            }

            if (_currentOffset > MaxOffset)
            {
                _currentOffset = MaxOffset;
            }

            return trees;
        }

        private void SpawnRock(Obstacle[] currentRow)
        {
            _rowsUntilRock--;

            if (_rowsUntilRock <= 0)
            {
                _rowsUntilRock = RockInterval;

                var index = _random.Next(_currentOffset, _currentOffset + _currentGap);
                index = Math.Clamp(index, MaxOffset, RowLength - MaxOffset);

                currentRow[index] = Rock.BuildWithIndex(NumberOfRows, index);
            }
        }

        private void SpawnRamp(Obstacle[] currentRow)
        {
            _rowsUntilRamp--;

            if (_rowsUntilRamp <= 0)
            {
                _rowsUntilRamp = RampInterval;

                var index = _random.Next(_currentOffset, _currentOffset + _currentGap);
                index = Math.Clamp(index, MaxOffset, RowLength - MaxOffset);

                currentRow[index] = Ramp.BuildWithIndex(NumberOfRows, index);
            }
        }
    }
}
