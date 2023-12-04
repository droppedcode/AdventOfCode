using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day01 : FileDay
  {
    public override string Task1()
    {
      var max = 0;
      var current = 0;
      foreach (var line in ReadLines())
      {
        if (line == "")
        {
          if (max < current)
          {
            max = current;
          }
          current = 0;
        }
        else
        {
          current += GetInt(line, out _);
        }
      }

      return max.ToString();
    }

    public override string Task2()
    {
      var max1 = 0;
      var max2 = 0;
      var max3 = 0;
      var current = 0;
      foreach (var line in ReadLines())
      {
        if (line == "")
        {
          if (max1 < current)
          {
            max3 = max2;
            max2 = max1;
            max1 = current;
          }
          else if (max2 < current)
          {
            max3 = max2;
            max2 = current;
          }
          else if (max3 < current)
          {
            max3 = current;
          }

          current = 0;
        }
        else
        {
          current += GetInt(line, out _);
        }
      }

      return (max1 + max2 + max3).ToString();
    }
  }
}
