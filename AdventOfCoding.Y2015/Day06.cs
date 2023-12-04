using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day06 : FileDay
  {
    private void On(bool[,] bools, (int x, int y) from, (int x, int y) to)
    {
      for (var x = from.x; x <= to.x; x++)
      {
        for (var y = from.y; y <= to.y; y++)
        {
          bools[x, y] = true;
        }
      }
    }

    private void Off(bool[,] bools, (int x, int y) from, (int x, int y) to)
    {
      for (var x = from.x; x <= to.x; x++)
      {
        for (var y = from.y; y <= to.y; y++)
        {
          bools[x, y] = false;
        }
      }
    }

    private void Toggle(bool[,] bools, (int x, int y) from, (int x, int y) to)
    {
      for (var x = from.x; x <= to.x; x++)
      {
        for (var y = from.y; y <= to.y; y++)
        {
          bools[x, y] = !bools[x, y];
        }
      }
    }

    private int Count(bool[,] bools)
    {
      var count = 0;

      for (var x = 0; x < 1000; x++)
      {
        for (var y = 0; y < 1000; y++)
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
      var bools = new bool[1000, 1000];

      foreach (var line in ReadLines())
      {
        switch (line[6])
        {
          case 'n':
            {
              var w1 = GetWord(line, 8);
              var c1 = Get2Ints(w1, ',');
              var c2 = Get2Ints(GetWord(line, 8 + w1.Length + 9), ',');

              On(bools, c1, c2);
            }
            break;
          case 'f':
            {
              var w1 = GetWord(line, 9);
              var c1 = Get2Ints(w1, ',');
              var c2 = Get2Ints(GetWord(line, 9 + w1.Length + 9), ',');

              Off(bools, c1, c2);
            }
            break;
          case ' ':
            {
              var w1 = GetWord(line, 7);
              var c1 = Get2Ints(w1, ',');
              var c2 = Get2Ints(GetWord(line, 7 + w1.Length + 9), ',');

              Toggle(bools, c1, c2);
            }
            break;
          default:
            throw new InvalidOperationException();
        }
      }

      return Count(bools).ToString();
    }

    private void On2(int[,] lights, (int x, int y) from, (int x, int y) to)
    {
      for (var x = from.x; x <= to.x; x++)
      {
        for (var y = from.y; y <= to.y; y++)
        {
          lights[x, y]++;
        }
      }
    }

    private void Off2(int[,] lights, (int x, int y) from, (int x, int y) to)
    {
      for (var x = from.x; x <= to.x; x++)
      {
        for (var y = from.y; y <= to.y; y++)
        {
          if (lights[x, y] > 0)
          {
            lights[x, y]--;
          }
        }
      }
    }

    private void Toggle2(int[,] lights, (int x, int y) from, (int x, int y) to)
    {
      for (var x = from.x; x <= to.x; x++)
      {
        for (var y = from.y; y <= to.y; y++)
        {
          lights[x, y] += 2;
        }
      }
    }

    private int Count2(int[,] lights)
    {
      var count = 0;

      for (var x = 0; x < 1000; x++)
      {
        for (var y = 0; y < 1000; y++)
        {
          count += lights[x, y];
        }
      }

      return count;
    }

    public override string Task2()
    {
      var lights = new int[1000, 1000];

      foreach (var line in ReadLines())
      {
        switch (line[6])
        {
          case 'n':
            {
              var w1 = GetWord(line, 8);
              var c1 = Get2Ints(w1, ',');
              var c2 = Get2Ints(GetWord(line, 8 + w1.Length + 9), ',');

              On2(lights, c1, c2);
            }
            break;
          case 'f':
            {
              var w1 = GetWord(line, 9);
              var c1 = Get2Ints(w1, ',');
              var c2 = Get2Ints(GetWord(line, 9 + w1.Length + 9), ',');

              Off2(lights, c1, c2);
            }
            break;
          case ' ':
            {
              var w1 = GetWord(line, 7);
              var c1 = Get2Ints(w1, ',');
              var c2 = Get2Ints(GetWord(line, 7 + w1.Length + 9), ',');

              Toggle2(lights, c1, c2);
            }
            break;
          default:
            throw new InvalidOperationException();
        }
      }

      return Count2(lights).ToString();
    }
  }
}
