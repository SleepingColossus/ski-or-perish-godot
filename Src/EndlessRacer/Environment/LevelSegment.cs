using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class LevelSegment : LevelSegmentTemplate
    {
        public Vector2 _position;
        private readonly SpecialTile[,] _specialTiles;

        public float GetY => _position.Y;

        public LevelSegment(Vector2 position, CrossingPoint entryPoint, CrossingPoint exitPoint, Texture2D sprite, int[,] specialTileData) :
            base(entryPoint, exitPoint, sprite, specialTileData)
        {
            _position = position;
            _specialTiles = InitSpecialTiles(specialTileData);
        }

        public LevelSegment(Vector2 position, LevelSegmentTemplate template) :
            base(template.EntryPoint, template.ExitPoint, template.Sprite, template.SpecialTileData)
        {
            _position = position;
            _specialTiles = InitSpecialTiles(template.SpecialTileData);
        }

        public void Update(float scrollSpeed, Player player)
        {
            _position.Y -= scrollSpeed;

            foreach (var specialTile in _specialTiles)
            {
                specialTile?.Update(scrollSpeed, player);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, _position, Color.White);
        }

        public bool IsOffScreen()
        {
            return _position.Y < - Sprite.Height;
        }

        private SpecialTile[,] InitSpecialTiles(int[,] specialTileData)
        {
            var size1 = specialTileData.GetLength(0);
            var size2 = specialTileData.GetLength(1);

            var specialTiles = new SpecialTile[size1, size2];

            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    if (specialTileData[i, j] > 0 && specialTileData[i, j] < 4)
                    {
                        var height = size1 * Constants.TileSize + _position.Y;
                        var width = size2 * Constants.TileSize;

                        var position = new Vector2(height, width);

                        var type = (SpecialTileType)specialTileData[i, j];

                        specialTiles[i, j] = new SpecialTile(type, position);
                    }
                }
            }

            return specialTiles;
        }
    }
}
