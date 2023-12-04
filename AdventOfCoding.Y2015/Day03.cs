using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day03 : FileDay
  {
    public override string Task1()
    {
      var coordinates = new HashSet<(int, int)>();

      var x = 0;
      var y = 0;

      coordinates.Add((x, y));

      foreach (var c in ReadFile())
      {
        switch (c)
        {
          case '<':
            x--;
            break;
          case '^':
            y--;
            break;
          case 'v':
            y++;
            break;
          case '>':
            x++;
            break;
        }

        coordinates.Add((x, y));
      }

      return coordinates.Count.ToString();
    }

    public override string Task2()
    {
      var coordinates = new HashSet<(int, int)>();

      var x1 = 0;
      var y1 = 0; 
      var x2 = 0;
      var y2 = 0;

      coordinates.Add((x1, y1));

      var roboTurn = false;

      foreach (var c in ReadFile())
      {
        if (roboTurn)
        {
          switch (c)
          {
            case '<':
              x1--;
              break;
            case '^':
              y1--;
              break;
            case 'v':
              y1++;
              break;
            case '>':
              x1++;
              break;
          }

          coordinates.Add((x1, y1));
        }
        else
        {
          switch (c)
          {
            case '<':
              x2--;
              break;
            case '^':
              y2--;
              break;
            case 'v':
              y2++;
              break;
            case '>':
              x2++;
              break;
          }

          coordinates.Add((x2, y2));
        }

        roboTurn = !roboTurn;
      }

      return coordinates.Count.ToString();
    }
  }
}
