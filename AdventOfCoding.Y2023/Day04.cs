using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2023
{
  internal class Day04 : FileDay
  {
    public override string Task1()
    {
      var sum = 0;

      foreach (var line in ReadLines())
      {
        var span = line.AsSpan(10);
        var possible = new HashSet<int>();
        var point = 0;

        for (var i = 0; i < 30; i += 3)
        {
          var value = span.ParseFirstInt(i);
          Console.Write(value);
          Console.Write(' ');
          possible.Add(value);
        }

        for (var i = 32; i < 107; i += 3)
        {
          var value = span.ParseFirstInt(i);
          Console.Write(value);
          Console.Write(' ');

          if (possible.Contains(value))
          {
            if (point == 0)
            {
              point = 1;
            }
            else
            {
              point *= 2;
            }
          }
        }

        Console.WriteLine(point);

        sum += point;
      }

      return sum.ToString();
    }

    public override string Task2()
    {
      var sum = 219;
      var cards = new int[219];
      for (var i = 0; i < cards.Length; i++)
      {
        cards[i] = 1;
      }

      var index = 0;
      foreach (var line in ReadLines())
      {
        var span = line.AsSpan(10);
        var possible = new HashSet<int>();
        var diff = 1;

        for (var i = 0; i < 30; i += 3)
        {
          var value = span.ParseFirstInt(i);
          possible.Add(value);
        }

        for (var i = 32; i < 107; i += 3)
        {
          var value = span.ParseFirstInt(i);

          if (possible.Contains(value))
          {
            cards[index + diff] += cards[index];
            diff++;
            sum += cards[index];
          }
        }

        index++;
      }

      return sum.ToString();
    }
  }
}
