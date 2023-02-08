using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private readonly List<Tree[]> _trees;

        public Level()
        {
            _currentGap = InitialGap;
            _currentOffset = 0;
            _random = new Random();

            _trees = new List<Tree[]>();

            // initialize trees
            for (int row = 0; row < NumberOfRows; row++)
            {
                _trees.Add(InitializeRow(row));
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int row = 0; row < _trees.Count; row++)
            {
                for (int col = 0; col < _trees[row].Length; col++)
                {
                    if (_trees[row][col] != null)
                    {
                        _trees[row][col].Update(gameTime);
                    }
                }
            }

            // is the first row out of bounds?
            var outOfBounds = _trees[0][0].IsOffScreen();

            // if so, remove first row and add new row on bottom
            if (outOfBounds)
            {
                _trees.RemoveAt(0);

                var newRow = InitializeRow(NumberOfRows - 1);

                _trees.Add(newRow);
            }
        }

        public void Draw()
        {
            for (int row = 0; row < _trees.Count; row++)
            {
                for (int col = 0; col < _trees[row].Length; col++)
                {
                    if (_trees[row][col] != null)
                    {
                        _trees[row][col].Draw();
                    }
                }
            }
        }

        private Tree[] InitializeRow(int row)
        {
            var trees = new Tree[RowLength];

            var cellsToFill = (RowLength - _currentGap) / 2;

            // fill left side
            for (var i = 0; i < cellsToFill + _currentOffset; i++)
            {
                trees[i] = Tree.BuildWithIndex(row, i);
            }

            // fill left side
            for (var i = RowLength - cellsToFill - 1 + _currentOffset; i < RowLength; i++)
            {
                trees[i] = Tree.BuildWithIndex(row, i);
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
    }
}
