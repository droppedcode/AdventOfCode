using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2023
{
  internal class Day03 : FileDay
  {
    public override string Task1()
    {
      var sum = 0;

      var lines = ReadLines().ToArray();

      for (var row = 1; row < lines.Length - 1; row++)
      {
        for (var col = 1; col < lines[row].Length - 1; col++)
        {
          char c = lines[row][col];
          if (c != '.' && !c.IsDigitLatin())
          {
            sum += LookForNumbersInRow(lines, row - 1, col);
            sum += LookForNumbersInRow(lines, row + 1, col);

            if (lines[row][col + 1].IsDigitLatin())
            {
              var value = lines[row].AsSpan(col + 1).ParseFirstInt();
              Console.WriteLine(value);
              sum += value;
            }

            if (lines[row][col - 1].IsDigitLatin())
            {
              var colLook = col;
              while (colLook > 0 && lines[row][colLook - 1].IsDigitLatin())
              {
                colLook--;
              }

              var value = lines[row].AsSpan(colLook).ParseFirstInt();
              Console.WriteLine(value);
              sum += value;
            }
          }
        }
      }

      return sum.ToString();
    }

    private static int LookForNumbersInRow(string[] lines, int row, int col)
    {
      if (lines[row][col].IsDigitLatin())
      {
        // Number
        while (lines[row][col - 1].IsDigitLatin())
        {
          col--;
        }

        var value = lines[row].AsSpan(col).ParseFirstInt();
        return value;
      }
      else
      {
        var sum = 0;

        if (lines[row][col + 1].IsDigitLatin())
        {
          // Number, first digit
          sum += lines[row].AsSpan(col + 1).ParseFirstInt();
        }

        if (lines[row][col - 1].IsDigitLatin())
        {
          // Number, last digit
          while (col > 0 && lines[row][col - 1].IsDigitLatin())
          {
            col--;
          }

          var value = lines[row].AsSpan(col).ParseFirstInt();
          sum += value;
        }

        return sum;
      }
    }

    public override string Task2()
    {
      var sum = 0;

      var lines = ReadLines().ToArray();

      var numbers = new List<int>();

      for (var row = 1; row < lines.Length - 1; row++)
      {
        for (var col = 1; col < lines[row].Length - 1; col++)
        {
          char c = lines[row][col];
          if (c == '*')
          {
            LookForNumbersInRow2(lines, row - 1, col, numbers);
            LookForNumbersInRow2(lines, row + 1, col, numbers);

            if (lines[row][col + 1].IsDigitLatin())
            {
              var value = lines[row].AsSpan(col + 1).ParseFirstInt();
              numbers.Add(value);
            }

            if (lines[row][col - 1].IsDigitLatin())
            {
              var colLook = col;
              while (colLook > 0 && lines[row][colLook - 1].IsDigitLatin())
              {
                colLook--;
              }

              var value = lines[row].AsSpan(colLook).ParseFirstInt();
              numbers.Add(value);
            }
          }

          if (numbers.Count > 2)
          {
            throw new Exception("Too many numbers.");
          }
          else if (numbers.Count >= 2)
          {
            sum += numbers[0] * numbers[1];
          }

          numbers.Clear();
        }
      }

      return sum.ToString();
    }

    private static void LookForNumbersInRow2(string[] lines, int row, int col, List<int> values)
    {
      if (lines[row][col].IsDigitLatin())
      {
        // Number
        while (lines[row][col - 1].IsDigitLatin())
        {
          col--;
        }

        var value = lines[row].AsSpan(col).ParseFirstInt();
        values.Add(value);
      }
      else
      {
        if (lines[row][col + 1].IsDigitLatin())
        {
          // Number, first digit
          var value = lines[row].AsSpan(col + 1).ParseFirstInt();
          values.Add(value);
        }

        if (lines[row][col - 1].IsDigitLatin())
        {
          // Number, last digit
          while (col > 0 && lines[row][col - 1].IsDigitLatin())
          {
            col--;
          }

          var value = lines[row].AsSpan(col).ParseFirstInt();
          values.Add(value);
        }
      }
    }

  }
}
