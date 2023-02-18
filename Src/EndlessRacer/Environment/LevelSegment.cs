using EndlessRacer.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Environment
{
    internal class LevelSegment : LevelSegmentTemplate
    {
        public Vector2 _position;
        private readonly Obstacle[,] _obstacles;

        public float GetY => _position.Y;

        public LevelSegment(Vector2 position, CrossingPoint entryPoint, CrossingPoint exitPoint, Texture2D sprite, bool[,] obstacleData) :
            base(entryPoint, exitPoint, sprite, obstacleData)
        {
            _position = position;
            _obstacles = InitObstacles(obstacleData);
        }

        public LevelSegment(Vector2 position, LevelSegmentTemplate template) :
            base(template.EntryPoint, template.ExitPoint, template.Sprite, template.ObstacleData)
        {
            _position = position;
            _obstacles = InitObstacles(template.ObstacleData);
        }

        public void Update(float scrollSpeed, Player player)
        {
            _position.Y -= scrollSpeed;

            foreach (var obstacle in _obstacles)
            {
                obstacle?.Update(scrollSpeed, player);
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

        private Obstacle[,] InitObstacles(bool[,] obstacleData)
        {
            var size1 = obstacleData.GetLength(0);
            var size2 = obstacleData.GetLength(1);

            var obstacles = new Obstacle[size1, size2];

            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    if (obstacleData[i, j])
                    {
                        var height = size1 * Constants.TileSize + _position.Y;
                        var width = size2 * Constants.TileSize;

                        var position = new Vector2(height, width);

                        obstacles[i, j] = new Obstacle(position);
                    }
                }
            }

            return obstacles;
        }
    }
}
