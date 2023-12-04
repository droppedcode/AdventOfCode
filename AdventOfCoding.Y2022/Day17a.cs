using AdventOfCoding.Core;
using AdventOfCoding.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day17a : FileDay
  {
    /* Shapes in order
      
      ####

      .#.
      ###
      .#.

      ..#
      ..#
      ###

      #
      #
      #
      #

      ##
      ##

     */

    private const int _shapeCount = 5;
    private static readonly int[] _widths = new int[] { 4, 3, 3, 1, 2 };
    private static readonly int[] _heights = new int[] { 1, 3, 3, 4, 2 };

    private bool IsValid(bool[,] map, int shape, int x, long y)
    {
      if (x < 0) return false;
      if (x + _widths[shape] > map.GetLength(0)) return false;
      if (_heights[shape] > y + 1) return false;

      switch (shape)
      {
        case 0:
          if (map[x, y] || map[x + 1, y] || map[x + 2, y] || map[x + 3, y]) return false;
          break;
        case 1:
          if (map[x + 1, y] || map[x, y - 1] || map[x + 1, y - 1] || map[x + 2, y - 1] || map[x + 1, y - 2]) return false;
          break;
        case 2:
          if (map[x + 2, y] || map[x + 2, y - 1] || map[x, y - 2] || map[x + 1, y - 2] || map[x + 2, y - 2]) return false;
          break;
        case 3:
          if (map[x, y] || map[x, y - 1] || map[x, y - 2] || map[x, y - 3]) return false;
          break;
        case 4:
          if (map[x, y] || map[x + 1, y] || map[x, y - 1] || map[x + 1, y - 1]) return false;
          break;
      }

      return true;
    }

    private void Set(bool[,] map, int shape, int x, long y, bool value = true)
    {
      switch (shape)
      {
        case 0:
          map[x, y] = value;
          map[x + 1, y] = value;
          map[x + 2, y] = value;
          map[x + 3, y] = value;
          break;
        case 1:
          map[x + 1, y] = value;
          map[x, y - 1] = value;
          map[x + 1, y - 1] = value;
          map[x + 2, y - 1] = value;
          map[x + 1, y - 2] = value;
          break;
        case 2:
          map[x + 2, y] = value;
          map[x + 2, y - 1] = value;
          map[x, y - 2] = value;
          map[x + 1, y - 2] = value;
          map[x + 2, y - 2] = value;
          break;
        case 3:
          map[x, y] = value;
          map[x, y - 1] = value;
          map[x, y - 2] = value;
          map[x, y - 3] = value;
          break;
        case 4:
          map[x, y] = value;
          map[x + 1, y] = value;
          map[x, y - 1] = value;
          map[x + 1, y - 1] = value;
          break;
      }
    }

    public override string Task1()
    {
      var jet = ReadFile();

      return DropX(jet, 2022).ToString();
    }

    private long DropX(string jet, long count)
    {
      var arr = new bool[7, count * 4 + 7];
      // var arr = new bool[7, 20];

      var top = -1l;

      var jetIndex = 0;

      var shapeIndex = 0;

      for (var i = 0; i < count; i++)
      {
        var x = 2;
        var y = top + _heights[shapeIndex] + 3;

        // Console.WriteLine(arr.DrawMap('#', '.', invertY: true));

        while (true)
        {
          //Set(arr, shapeIndex, x, y);
          //Console.WriteLine(arr.DrawMap('#', '.', invertY: true));
          //Set(arr, shapeIndex, x, y, false);

          var xJet = jet[jetIndex] == '<' ? x - 1 : x + 1;
          if (IsValid(arr, shapeIndex, xJet, y))
          {
            x = xJet;
          }

          jetIndex++;
          if (jetIndex == jet.Length)
          {
            jetIndex = 0;
          }

          if (IsValid(arr, shapeIndex, x, y - 1))
          {
            y--;
          }
          else
          {
            Set(arr, shapeIndex, x, y);
            if (top < y)
            {
              top = y;
            }

            shapeIndex++;
            if (shapeIndex == _shapeCount)
            {
              shapeIndex = 0;
            }

            break;
          }
        }
      }

      // Console.WriteLine(arr.DrawMap('#', '.', invertY: true, toY: top + 3));

      return top + 1;
    }

    public override string Task2()
    {
      var jet = ReadFile();

      return DropX(jet, 1000000000000l).ToString();
    }
  }
}
