using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2023
{
  internal partial class Day01 : FileDay
  {
    public override string Task1()
    {
      var sum = 0;
      foreach (var line in ReadLines())
      {
        sum += (int)(line.First(f => char.IsDigit(f)) - '0') * 10 + (int)(line.Last(f => char.IsDigit(f)) - '0');
      }
      return sum.ToString();
    }

    [GeneratedRegex("(one|two|three|four|five|six|seven|eight|nine)")]
    private static partial Regex NumberFinder();

    [GeneratedRegex("(eno|owt|eerht|ruof|evif|xis|neves|thgie|enin)")]
    private static partial Regex NumberFinderReverse();

    public override string Task2()
    {
      var sum = 0;

      var regex = NumberFinder();
      var regexReverse = NumberFinderReverse();

      foreach (var line in ReadLines())
      {
        var lineMod = regex.Replace(line, m => {
          return m.Captures[0].Value switch
          {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => throw new NotImplementedException()
          };
        });

        var lineModReverse = regexReverse.Replace(string.Join("", line.Reverse()), m => {
          return m.Captures[0].Value switch
          {
            "eno" => "1",
            "owt" => "2",
            "eerht" => "3",
            "ruof" => "4",
            "evif" => "5",
            "xis" => "6",
            "neves" => "7",
            "thgie" => "8",
            "enin" => "9",
            _ => throw new NotImplementedException()
          };
        });

        Console.WriteLine((int)(lineMod.First(f => char.IsDigit(f)) - '0') * 10 + (int)(lineModReverse.First(f => char.IsDigit(f)) - '0'));

        sum += (int)(lineMod.First(f => char.IsDigit(f)) - '0') * 10 + (int)(lineModReverse.First(f => char.IsDigit(f)) - '0');
      }
      return sum.ToString();
    }
  }
}
