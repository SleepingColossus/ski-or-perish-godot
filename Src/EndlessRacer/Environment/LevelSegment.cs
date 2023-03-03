using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class LevelSegment : LevelSegmentTemplate
    {
        private Vector2 _position;
        private readonly SpecialTile[,] _specialTiles;

        public float GetY => _position.Y;

        public LevelSegment(Vector2 position, LevelSegmentTemplate template) :
            base(template.EntryPoint, template.ExitPoint, template.Background, template.SpecialTileData, template.Foreground)
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
            spriteBatch.Draw(Background, _position, Color.White);

//#if DEBUG
//            {
//                foreach (var specialTile in _specialTiles)
//                {
//                    specialTile?.Draw(spriteBatch);
//                }
//            }
//#endif
        }

        public void DrawForeground(SpriteBatch spriteBatch)
        {
            if (Foreground != null)
            {
                spriteBatch.Draw(Foreground, _position, Color.White);
            }
        }

        public bool IsOffScreen()
        {
            return _position.Y < - Background.Height;
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
                        var x = j * Constants.TileSize64;
                        var y = i * Constants.TileSize64 + _position.Y;

                        var position = new Vector2(x, y);

                        var type = (SpecialTileType)specialTileData[i, j];

                        specialTiles[i, j] = new SpecialTile(type, position);
                    }
                }
            }

            return specialTiles;
        }
    }
}
