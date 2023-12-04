using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day04 : FileDay
  {
    private bool FullyContains(int a1, int a2, int b1, int b2)
    {
      if (a1 >= b1 && a2 <= b2) return true;
      if (b1 >= a1 && b2 <= a2) return true;

      return false;
    }

    public override string Task1()
    {
      var count = 0;

      foreach (var line in ReadLines())
      {
        var array = GetInts(line, '-', ',').ToArray();

        if (FullyContains(array[0], array[1], array[2], array[3]))
        {
          count++;
        }
      }

      return count.ToString();
    }

    private bool Contains(int a1, int a2, int b1, int b2)
    {
      if (a1 >= b1 && a1 <= b2) return true;
      if (a2 >= b1 && a2 <= b2) return true;
      if (b1 >= a1 && b1 <= a2) return true;
      if (b2 >= a1 && b2 <= a2) return true;

      return false;
    }

    public override string Task2()
    {
      var count = 0;

      foreach (var line in ReadLines())
      {
        var array = GetInts(line, '-', ',').ToArray();

        if (Contains(array[0], array[1], array[2], array[3]))
        {
          count++;
        }
      }

      return count.ToString();
    }
  }
}
