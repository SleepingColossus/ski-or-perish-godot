using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Level
    {
        private const int NumberOfRows = 17;
        private const int RowLength = 30;

        private const int InitialGap = 24;
        private int _currentGap;

        private const int MinOffset = -2;
        private const int MaxOffset = 2;
        private const int OffsetStep = 1;
        private int _currentOffset;
        private Random _random;

        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _treeSprite;

        private readonly Queue<Tree[]> _trees;

        public Level(SpriteBatch spriteBatch, Texture2D treeSprite)
        {
            _spriteBatch = spriteBatch;
            _treeSprite = treeSprite;

            _currentGap = InitialGap;
            _currentOffset = 0;
            _random = new Random();

            // initialize trees
            var trees = new Tree[NumberOfRows][];

            for (int row = 0; row < trees.Length; row++)
            {
                trees[row] = InitializeRow(row);
            }

            _trees = new Queue<Tree[]>(trees);
        }

        public void Update(GameTime gameTime)
        {
            var treeArray = _trees.ToArray();

            for (int row = 0; row < treeArray.Length; row++)
            {
                for (int col = 0; col < treeArray[row].Length; col++)
                {
                    if (treeArray[row][col] != null)
                    {
                        treeArray[row][col].Update(gameTime);
                    }
                }
            }
        }

        public void Draw()
        {
            var treeArray = _trees.ToArray();

            for (int row = 0; row < treeArray.Length; row++)
            {
                for (int col = 0; col < treeArray[row].Length; col++)
                {
                    if (treeArray[row][col] != null)
                    {
                        treeArray[row][col].Draw();
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
                trees[i] = new Tree(_spriteBatch, _treeSprite,
                    IndexToCoordinates(row, i, _treeSprite.Bounds));
            }

            // fill left side
            for (var i = RowLength - cellsToFill - 1 + _currentOffset; i < RowLength; i++)
            {
                trees[i] = new Tree(_spriteBatch, _treeSprite,
                    IndexToCoordinates(row, i, _treeSprite.Bounds));
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

        private Vector2 IndexToCoordinates(int row, int col, Rectangle bounds)
        {
            var x = col * bounds.Size.X;
            var y = row * bounds.Size.Y;

            return new Vector2(x, y);
        }
    }
}
