using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day18 : FileDay
  {
    private int Count(bool[,] bools)
    {
      var count = 0;

      for (var x = 0; x < 100; x++)
      {
        for (var y = 0; y < 100; y++)
        {
          if (bools[x, y])
          {
            count++;
          }
        }
      }

      return count;
    }

    public override string Task1()
    {
      var currentGrid = new bool[100, 100];
      var nextGrid = new bool[100, 100];

      var row = 0;
      foreach (var line in ReadLines())
      {
        var col = 0;

        foreach (var c in line)
        {
          currentGrid[row, col] = c == '#';

          col++;
        }

        row++;
      }

      for (var i = 0; i < 100; i++)
      {
        var buf = currentGrid;
        currentGrid = nextGrid;
        nextGrid = buf;
      }

      return Count(currentGrid).ToString();
    }

    public override string Task2()
    {
      throw new NotImplementedException();
    }
  }
}
