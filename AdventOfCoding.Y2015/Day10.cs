using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day10 : StringDay
  {
    protected override string Input => "3113322113";

    private string LookAndSay(string input, StringBuilder sb)
    {
      var current = input[0];
      var count = 1;

      for (int i = 1; i < input.Length; i++)
      {
        if (current == input[i])
        {
          count++;
        }
        else
        {
          sb.Append(count);
          sb.Append(current);

          current = input[i];
          count = 1;
        }
      }

      sb.Append(count);
      sb.Append(current);

      return sb.ToString();
    }

    public override string Task1()
    {
      var current = Input;
      var sb = new StringBuilder();

      for (var i = 0; i < 40; i++)
      {
        current = LookAndSay(current, sb);
        sb.Clear();
      }

      return current.Length.ToString();
    }

    public override string Task2()
    {
      var current = Input;
      var sb = new StringBuilder();

      for (var i = 0; i < 50; i++)
      {
        current = LookAndSay(current, sb);
        sb.Clear();
      }

      return current.Length.ToString();
    }
  }
}
