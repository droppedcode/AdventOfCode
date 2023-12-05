using AdventOfCoding.Core;

namespace AdventOfCoding.Y2023
{
  internal class Program
  {
    private static readonly Runner runner = new();

    static void Main(string[] args)
    {
      runner.RunTask2<Day05>();
      Console.ReadKey();
    }
  }
}