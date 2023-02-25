using System;

namespace Models
{
    public class CollisionData
    {
        public int[] Data;

        public int[,] ToMultiDimArray()
        {
            // TODO access from constants
            var width = 30;

            int height = (int)Math.Ceiling(Data.Length / (double)width);
            int[,] result = new int[height, width];

            for (int index = 0; index < Data.Length; index++)
            {
                var rowIndex = index / width;
                var colIndex = index % width;
                result[rowIndex, colIndex] = Data[index];
            }

            return result;
        }
    }
}
