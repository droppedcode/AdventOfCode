using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using AdventOfCoding.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day17 : FileDay
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

    private const int _mapWidth = 7;
    private const int _shapeCount = 5;
    private static readonly int[] _widths = new int[] { 4, 3, 3, 1, 2 };
    private static readonly int[] _heights = new int[] { 1, 3, 3, 4, 2 };

    private static readonly byte _filledRow = GetAsByte(new bool[7] { true, true, true, true, true, true, true });

    private static readonly byte[][][] _shapes;

    static Day17()
    {
      _shapes = new byte[_shapeCount][][];
      for (var s = 0; s < _shapeCount; s++)
      {
        _shapes[s] = new byte[_mapWidth][];

        for (var x = 0; x < _mapWidth; x++)
        {
          if (_widths[s] + x <= _mapWidth)
          {
            _shapes[s][x] = BuildShape(s, x);
          }
        }
      }
    }

    private static bool[] GetBools(int start, params bool[] bools)
    {
      var result = new bool[bools.Length + start];
      bools.CopyTo(result, start);
      return result;
    }

    private static byte[] BuildShape(int shape, int x)
    {
      switch (shape)
      {
        case 0:
          return new[] {
            GetAsByte(GetBools(x, true, true, true, true))
          };
        case 1:
          return new[] {
            GetAsByte(GetBools(x + 1, true)),
            GetAsByte(GetBools(x, true, true, true)),
            GetAsByte(GetBools(x + 1, true)),
          };
        case 2:
          return new[] {
            GetAsByte(GetBools(x + 2, true)),
            GetAsByte(GetBools(x + 2, true)),
            GetAsByte(GetBools(x, true, true, true)),
          };
        case 3:
          return new[] {
            GetAsByte(GetBools(x, true)),
            GetAsByte(GetBools(x, true)),
            GetAsByte(GetBools(x, true)),
            GetAsByte(GetBools(x, true)),
          };
        case 4:
          return new[] {
            GetAsByte(GetBools(x, true, true)),
            GetAsByte(GetBools(x, true, true)),
          };
      }

      throw new InvalidOperationException();
    }

    private bool IsValid(byte[] map, int shape, int x, int y, int top)
    {
      if (x < 0) return false;
      if (x + _widths[shape] > _mapWidth) return false;

      var h = _heights[shape];

      if (h > y + 1) return false;
      if (top < y - h + 1) return true;

      var shapeArr = _shapes[shape][x];

      for (var r = 0; r < shapeArr.Length; r++)
      {
        if ((map[y - r] & shapeArr[r]) > 0) return false;
      }

      return true;
    }

    private void Set(byte[] map, int shape, int x, int y, bool value = true)
    {
      var shapeArr = _shapes[shape][x];
      for (var r = 0; r < shapeArr.Length; r++)
      {
        if (value)
        {
          map[y - r] = (byte)(map[y - r] | shapeArr[r]);
        }
        else
        {
          map[y - r] = (byte)(map[y - r] ^ shapeArr[r]);
        }
      }
    }

    private static byte GetAsByte(bool[] source)
    {
      byte result = 0;
      // This assumes the array never contains more than 8 elements!
      int index = 0;

      // Loop through the array
      foreach (bool b in source)
      {
        // if the element is 'true' set the bit at that position
        if (b)
        {
          result |= (byte)(1 << index);
        }

        index++;
      }

      return result;
    }

    private static bool IsFilled(byte[] map, int y) => (map[y] & _filledRow) == _filledRow;

    public override string Task1()
    {
      var jet = ReadFile();

      return DropX(jet, 2022).ToString();
    }

    private BigInteger DropX(string jet, long dropCount)
    {
      var top = -1;
      var topFilled = 0L;

      var jetIndex = 0;

      var shapeIndex = 0;

      var topFilledNotShifted = 0;

      //var report = count / 1000;
      //var watch = Stopwatch.StartNew();

      var cycleLength = jet.Length * _shapeCount;
      var cycleCount = dropCount / cycleLength;
      var cycleTop = 0L;

      var arr = new byte[Math.Min(dropCount, cycleLength) * 4 + 3 + 4];

      var cache = new List<(byte[], int, int)>();

      for (var i = 0L; i < dropCount; i++)
      {
        var x = 2;
        var y = top + _heights[shapeIndex] + 3;

        // Console.WriteLine(arr.DrawMap('#', '.', invertY: true, toY: 20, toX: 7));

        while (true)
        {
          //Set(arr, shapeIndex, x, y);
          //Console.WriteLine(arr.DrawMap('#', '.', invertY: true, toY: 10, toX: 7));
          //Set(arr, shapeIndex, x, y, false);

          var xJet = jet[jetIndex] == '<' ? x - 1 : x + 1;
          if (IsValid(arr, shapeIndex, xJet, y, top))
          {
            x = xJet;
          }

          if (IsValid(arr, shapeIndex, x, y - 1, top))
          {
            y--;
          }
          else
          {
            Set(arr, shapeIndex, x, y);

            for (var ti = 0; ti < _heights[shapeIndex]; ti++)
            {
              var tempY = y - ti;
              if (shiftArr(arr, ref top, ref topFilled, tempY, ref topFilledNotShifted))
              {
                y = ti;
                break;
              }
            }

            if (top < y)
            {
              top = y;
            }

            var current = (arr.Take(top).ToArray(), shapeIndex, jetIndex);
            var same = new List<int>();

            for (var ci = 0; ci < cache.Count; ci++)
            {
              var old = cache[ci];
              if (current.shapeIndex == old.Item2 && current.jetIndex == old.Item3 && current.Item1.IsSame(old.Item1))
              {
                same.Add(ci);
              }
            }

            if (same.Count > 0)
            {
              Console.WriteLine($"{i}: {string.Join(',', same)}");
            }

            cache.Add(current);

            if (i == cycleLength)
            {
              cycleTop = topFilled + top;

              var remaining = dropCount % cycleLength;

              // i = dropCount - remaining;
            }

            jetIndex++;
            if (jetIndex == jet.Length)
            {
              jetIndex = 0;
            }

            shapeIndex++;
            if (shapeIndex == _shapeCount)
            {
              shapeIndex = 0;
            }


            break;
          }
        }

        //if (i % report == 0)
        //{
        //  Console.WriteLine($"{i}: {watch.Elapsed}");
        //}
      }

      // Console.WriteLine(arr.DrawMap('#', '.', invertY: true, toY: top + 3));

      return cycleTop * cycleCount + topFilled + top;

      bool shiftArr(byte[] arr, ref int top, ref long topFilled, int y, ref int topFilledNotShifted)
      {
        if (IsFilled(arr, y))
        {
          //if (y < arr.Length * 0.8)
          //{
          //  topFilledNotShifted = y;
          //  return false;
          //}
          //else
          {
            for (var ty = y; ty < arr.Length; ty++)
            {
              arr[ty - y] = arr[ty];
            }
            for (var ty = arr.Length - y; ty < arr.Length; ty++)
            {
              arr[ty] = 0;
            }

            topFilledNotShifted = 0;
            topFilled += y;
            top -= y;

            return true;
          }
        }
        else
        {
          return false;
        }
      }
    }

    // Not good
    public override string Task2()
    {
      var jet = ReadFile();

      return DropX(jet, 1_000_000_000_000L).ToString();
    }
  }
}
