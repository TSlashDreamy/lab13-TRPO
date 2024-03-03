using MathNet.Numerics.LinearAlgebra;

namespace lab13
{
    public class MatrixOperations
    {
        public static double[,] GenerateMatrix(int rows, int columns)
        {
            double[,] matrix = new double[rows, columns];
            Random rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rand.NextDouble() * 10;
                }
            }

            return matrix;
        }

        public static double[,] MultiplyMatricesBlock(double[,] A, double[,] B, int blockSize)
        {
            var matrixA = Matrix<double>.Build.DenseOfArray(A);
            var matrixB = Matrix<double>.Build.DenseOfArray(B);
            int rowsA = matrixA.RowCount;
            int columnsB = matrixB.ColumnCount;
            double[,] result = new double[rowsA, columnsB];

            for (int i = 0; i < rowsA; i += blockSize)
            {
                for (int j = 0; j < columnsB; j += blockSize)
                {
                    for (int k = 0; k < matrixA.ColumnCount; k += blockSize)
                    {
                        int blockRows = Math.Min(blockSize, rowsA - i);
                        int blockColumns = Math.Min(blockSize, columnsB - j);
                        int blockK = Math.Min(blockSize, matrixA.ColumnCount - k);

                        // Extract blocks
                        var blockA = matrixA.SubMatrix(i, blockRows, k, blockK);
                        var blockB = matrixB.SubMatrix(k, blockK, j, blockColumns);

                        // Multiply blocks
                        var blockResult = blockA.Multiply(blockB);

                        // Add to result
                        for (int ii = 0; ii < blockResult.RowCount; ii++)
                        {
                            for (int jj = 0; jj < blockResult.ColumnCount; jj++)
                            {
                                result[i + ii, j + jj] += blockResult[ii, jj];
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j].ToString("F3") + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
