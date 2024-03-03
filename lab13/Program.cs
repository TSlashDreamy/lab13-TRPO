using lab13;
using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;

int[] mValues = { 2, 4, 6, 8, 10 }; // Число рядків матриці А
int[] nValues = { 2, 4, 6, 8, 10 }; // Число рядків і стовбців матриці B
int[] lValues = { 2, 4, 6, 8, 10 }; // Число стовбців матриці B
int blockSize = 2; // Розмір блоку

foreach (int m in mValues)
{
    foreach (int l in lValues)
    {
        Console.WriteLine($"Проводимо експеримент для m={m}, l={l}:");
        Console.WriteLine("----------------------------------------");

        double[,] A = MatrixOperations.GenerateMatrix(m, nValues[0]);
        double[,] B = MatrixOperations.GenerateMatrix(nValues[0], l);

        Stopwatch stopwatch = Stopwatch.StartNew();
        double[,] C = MatrixOperations.MultiplyMatricesBlock(A, B, blockSize);
        stopwatch.Stop();

        Console.WriteLine($"Час виконання: {stopwatch.Elapsed.TotalMilliseconds} мілісекунд(и)\n");

        Console.WriteLine("Матриця A:");
        MatrixOperations.PrintMatrix(A);

        Console.WriteLine("\nМатриця B:");
        MatrixOperations.PrintMatrix(B);

        Console.WriteLine("\nМатриця C:");
        MatrixOperations.PrintMatrix(C);

        Console.WriteLine("\n\n");
    }
}
