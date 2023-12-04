using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day02 : FileDay
  {
    private int GetSurface((int l, int w, int h) box) => 2 * (box.l * box.w + box.w * box.h + box.h * box.l) + GetSmallestSide(box);

    private int GetSmallestSide((int l, int w, int h) box)
    {
      if (box.l >= box.w && box.l >= box.h)
      {
        return box.w * box.h;
      }
      else if (box.w >= box.l && box.w >= box.h)
      {
        return box.l * box.h;
      }
      else
      {
        return box.w * box.l;
      }
    }

    public override string Task1()
    {
      var sum = 0;

      foreach (var line in ReadLines())
      {
        var box = Get3Ints(line, 'x');

        sum += GetSurface(box);
      }

      return sum.ToString();
    }

    private int GetRibbon((int l, int w, int h) box)
    {
      var smallest = GetSmallest2Sides(box);
      return smallest.Item1 * 2 + smallest.Item2 * 2 + box.l * box.w * box.h;
    }

    private (int, int) GetSmallest2Sides((int l, int w, int h) box)
    {
      if (box.l >= box.w && box.l >= box.h)
      {
        return (box.w, box.h);
      }
      else if (box.w >= box.l && box.w >= box.h)
      {
        return (box.l, box.h);
      }
      else
      {
        return (box.w, box.l);
      }
    }

    public override string Task2()
    {
      var sum = 0;

      foreach (var line in ReadLines())
      {
        var box = Get3Ints(line, 'x');

        sum += GetRibbon(box);
      }

      return sum.ToString();
    }
  }
}
