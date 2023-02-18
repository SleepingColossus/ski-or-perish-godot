using System;

namespace Models
{
    public class CollisionData
    {
        public int[] Data;

        public bool[,] ToBoolMultiDimArray()
        {
            // TODO access from constants
            var width = 30;

            int height = (int)Math.Ceiling(Data.Length / (double)width);
            bool[,] result = new bool[height, width];
            int rowIndex, colIndex;

            for (int index = 0; index < Data.Length; index++)
            {
                rowIndex = index / width;
                colIndex = index % width;
                result[rowIndex, colIndex] = ToBool(Data[index]);
            }
            return result;
        }

        private bool ToBool(int i) => i != 0;
    }
}
