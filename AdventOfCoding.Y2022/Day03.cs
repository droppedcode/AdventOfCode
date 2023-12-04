using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day03 : FileDay
  {
    public override string Task1()
    {
      var sum = 0;

      foreach (var line in ReadLines())
      {
        var count = line.Length / 2;

        var set1 = new HashSet<char>();
        var set2 = new HashSet<char>();

        for (var i = 0; i < count; i++)
        {
          set1.Add(line[i]);
        }

        for (var i = count; i < line.Length; i++)
        {
          var c = line[i];
          if (!set2.Add(c)) continue;
          if (!set1.Add(c))
          {
            sum += GetPriority(c);
          }
        }
      }

      return sum.ToString();
    }

    private int GetPriority(char c)
    {
      return char.IsLower(c) ? (c - 'a' + 1) : (c - 'A' + 27);
    }

    public override string Task2()
    {
      var sum = 0;

      var line1 = "";
      var line2 = "";
      var line3 = "";

      var index = 0;

      foreach (var line in ReadLines())
      {
        if (index == 0)
        {
          line1 = line;
          index++;
        }
        else if (index == 1)
        {
          line2 = line;
          index++;
        }
        else if (index == 2)
        {
          line3 = line;
          index = 0;

          var set1 = new HashSet<char>(line1);
          var set2 = new HashSet<char>(line2);
          var set3 = new HashSet<char>(line3);

          var common = set1.Intersect(set2).Intersect(set3).ToArray();

          sum += GetPriority(common[0]);
        }
      }

      return sum.ToString();
    }
  }
}
