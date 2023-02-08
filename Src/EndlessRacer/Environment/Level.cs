using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class Level
    {
        private const int NumberOfRows = 17;
        private const int RowLength = 30;

        private const int InitialGap = 26;
        private int _currentGap;

        private const int MinOffset = -2;
        private const int MaxOffset = 2;
        private int _currentOffset;

        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _treeSprite;

        private readonly Tree[][] _trees;

        public Level(SpriteBatch spriteBatch, Texture2D treeSprite)
        {
            _spriteBatch = spriteBatch;
            _treeSprite = treeSprite;

            _currentGap = InitialGap;
            _currentOffset = 0;

            _trees = new Tree[NumberOfRows][];

            for (int row = 0; row < _trees.Length; row++)
            {
                _trees[row] = new Tree[RowLength];
            }

            InitializeTrees();
        }

        public void Update(GameTime gameTime)
        {
            for (int row = 0; row < _trees.Length; row++)
            {
                for (int col = 0; col < _trees[row].Length; col++)
                {
                    if (_trees[row][col] != null)
                    {
                        _trees[row][col].Update(gameTime);
                    }
                }
            }
        }

        public void Draw()
        {
            for (int row = 0; row < _trees.Length; row++)
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

        private void InitializeTrees()
        {
            for (int row = 0; row < _trees.Length; row++)
            {
                var cellsToFill = (RowLength - _currentGap) / 2;

                // fill left side
                for (var i = 0; i < cellsToFill; i++)
                {
                    _trees[row][i] = new Tree(_spriteBatch, _treeSprite,
                        IndexToCoordinates(row, i, _treeSprite.Bounds));
                }

                // fill left side
                for (var i = RowLength - 1; i > RowLength - cellsToFill - 1; i--)
                {
                    _trees[row][i] = new Tree(_spriteBatch, _treeSprite,
                        IndexToCoordinates(row, i, _treeSprite.Bounds));
                }

                //for (int col = 0; col < _trees[row].Length; col++)
                //{
                //    _trees[row][col] = new Tree(_spriteBatch, _treeSprite,
                //        IndexToCoordinates(row, col, _treeSprite.Bounds));
                //}
            }
        }

        private Vector2 IndexToCoordinates(int row, int col, Rectangle bounds)
        {
            var x = col * bounds.Size.X;
            var y = row * bounds.Size.Y;

            return new Vector2(x, y);
        }
    }
}
