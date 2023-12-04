using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day10 : FileDay
  {
    public override string Task1()
    {
      var cycleOfInterests = new int[] { 20, 60, 100, 140, 180, 220 };

      var x = 1;
      var sum = 0;
      var cycle = 0;

      foreach (var line in ReadLines())
      {
        cycle++;

        if (cycleOfInterests.Contains(cycle))
        {
          sum += cycle * x;
        }

        if (line[0] == 'a')
        {
          cycle++;

          if (cycleOfInterests.Contains(cycle))
          {
            sum += cycle * x;
          }

          x += GetInt(line, 5);
        }
      }

      if (cycleOfInterests.Contains(cycle))
      {
        sum += cycle * x;
      }

      return sum.ToString();
    }

    public override string Task2()
    {
      var screen = new StringBuilder();

      var x = 0;
      var cycle = 0;

      foreach (var line in ReadLines())
      {
        if (cycle % 40 == 0)
        {
          screen.AppendLine();
        }

        var draw = (cycle % 40) - x;
        screen.Append(draw >= 0 && draw <= 2 ? '#' : '.');

        cycle++;

        if (line[0] == 'a')
        {
          if (cycle % 40 == 0)
          {
            screen.AppendLine();
          }

          draw = (cycle % 40) - x;
          screen.Append(draw >= 0 && draw <= 2 ? '#' : '.');

          cycle++;

          x += GetInt(line, 5);
        }
      }

      return screen.ToString();
    }
  }
}
