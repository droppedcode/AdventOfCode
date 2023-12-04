using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day01 : FileDay
  {
    public override string Task1()
    {
      var level = 0;

      foreach (var c in ReadFile())
      {
        if (c == '(')
        {
          level++;
        }
        else
        {
          level--;
        }
      }

      return level.ToString();

    }

    public override string Task2()
    {
      var level = 0;
      var index = 0;
      foreach (var c in ReadFile())
      {
        if (c == '(')
        {
          level++;
        }
        else
        {
          level--;
        }

        if (level == -1)
        {
          return (index + 1).ToString();
        }

        index++;
      }

      throw new InvalidOperationException();
    }
  }
}
