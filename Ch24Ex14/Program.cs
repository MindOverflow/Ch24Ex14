using static System.Console;
using System.Threading.Tasks;

namespace Ch24Ex14
{
    internal static class Program
    {

        private static int[] _data;

        private static void Main()
        {
            WriteLine("Основной поток выполнения запущен.");

            _data = new int[100000000];

            // Последовательный вариант инициализации массива в цикле.
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = i;
            }

            _data[100] = -10;

            var parallelLoopResult = Parallel.ForEach(
                _data,
                (v, pls) =>
                {
                    if (v < 0)
                    {
                        pls.Break();
                    }
                    WriteLine($"Значение: {v}");
                }
                );

            if (!parallelLoopResult.IsCompleted)
            {
                WriteLine(
                    "\nЦикл завершился преждевременно из-за того, " + 
                    "что обнаружено отрицательное значение\n" +
                    $"на шаге цикла номер: {parallelLoopResult.LowestBreakIteration}. \n");
            }

            WriteLine("Основной поток выполнения завершён.");
        }
    }
}