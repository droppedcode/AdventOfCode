using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day08 : FileDay
  {
    public override string Task1()
    {
      var code = 0;
      var character = 0;

      foreach (var line in ReadLines())
      {
        code += line.Length;

        var escape = false;
        for (var i = 1; i < line.Length - 1; i++)
        {
          if (escape)
          {
            escape = false;

            if (line[i] == 'x')
            {
              i += 2;
            }
            character++;
          }
          else if (line[i] == '\\')
          {
            escape = true;
          }
          else
          {
            character++;
          }
        }
      }

      return (code - character).ToString();
    }

    public override string Task2()
    {
      var orig = 0;
      var encoded = 0;

      foreach (var line in ReadLines())
      {
        encoded += 2;

        orig += line.Length;

        for (var i = 0; i < line.Length; i++)
        {
          if (line[i] is '\\' or '"')
          {
            encoded += 2;
          }
          else
          {
            encoded++;
          }
        }
      }

      return (encoded - orig).ToString();
    }
  }
}
