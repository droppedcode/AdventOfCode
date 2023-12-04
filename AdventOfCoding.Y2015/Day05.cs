using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day05 : FileDay
  {
    private bool IsNice(string text)
    {
      var hasDouble = false;
      var vowels = 0;
      var prev = '\0';

      foreach (var c in text)
      {
        if (prev == 'a' && c == 'b') return false;
        if (prev == 'c' && c == 'd') return false;
        if (prev == 'p' && c == 'q') return false;
        if (prev == 'x' && c == 'y') return false;

        if (c is 'a' or 'e' or 'i' or 'o' or 'u')
        {
          vowels++;
        }
        if (prev == c)
        {
          hasDouble = true;
        }

        prev = c;
      }

      return hasDouble && vowels >= 3;
    }

    public override string Task1()
    {
      var count = 0;

      foreach (var line in ReadLines())
      {
        if (IsNice(line))
        {
          count++;
        }
      }

      return count.ToString();
    }

    private bool IsNice2(string text)
    {
      var hasDouble = false;
      var hasRepeat = false;
      var prev2 = '\0';
      var prev = '\0';
      var prevPair = ('\0', '\0');

      var pairs = new HashSet<(char, char)>();

      foreach (var c in text)
      {
        if (prev2 == c)
        {
          hasRepeat = true;
        }

        var pair = (prev, c);

        if (pairs.Contains(pair))
        {
          hasDouble = true;
        }

        pairs.Add(prevPair);
        prevPair = pair;

        prev2 = prev;
        prev = c;
      }

      return hasRepeat && hasDouble;
    }

    public override string Task2()
    {
      var count = 0;

      foreach (var line in ReadLines())
      {
        if (IsNice2(line))
        {
          count++;
        }
      }

      return count.ToString();
    }
  }
}
