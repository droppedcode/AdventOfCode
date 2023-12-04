using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day13 : FileDay
  {
    private static int IsValid(List<object> left, List<object> right)
    {
      var count = Math.Min(left.Count, right.Count);
      for (var i = 0; i < count; i++)
      {
        if (left[i] is int li && right[i] is int ri)
        {
          if (li < ri) return -1;
          if (li > ri) return 1;
        }
        else
        {
          var ll = left[i] is List<object> lo ? lo : new List<object>() { left[i] };
          var rl = right[i] is List<object> ro ? ro : new List<object>() { right[i] };

          var result = IsValid(ll, rl);

          if (result != 0) return result;
        }
      }

      if (left.Count < right.Count) return -1;
      if (left.Count > right.Count) return 1;
      return 0;
    }

    private List<object> GetList(string line, ref int from)
    {
      from++;

      var result = new List<object>();

      for (; from < line.Length; from++)
      {
        var c = line[from];
        if (c == '[')
        {
          result.Add(GetList(line, ref from));
        }
        else if (c == ']')
        {
          break;
        }
        else if (c == ',')
        {

        }
        else
        {
          result.Add(GetInt(line, ref from, ',', ']'));
          from -= 2;
        }
      }

      return result;
    }

    public override string Task1()
    {
      var isLeft = true;
      var index = 1;
      var line1 = "";

      var sum = 0;

      foreach (var line in ReadLines())
      {
        if (line == "") continue;

        if (isLeft)
        {
          line1 = line;
          isLeft = false;
        }
        else
        {
          int li = 0;
          int ri = 0;
          if (IsValid(GetList(line1, ref li), GetList(line, ref ri)) == -1)
          {
            sum += index;
          }

          isLeft = true;
          index++;
        }
      }

      return sum.ToString();
    }

    public override string Task2()
    {
      var packets = new List<List<object>>();

      foreach (var line in ReadLines())
      {
        if (line == "") continue;

        var i = 0;
        packets.Add(GetList(line, ref i));
      }

      packets.Add(new List<object>() { new List<object>() { 2 } });
      packets.Add(new List<object>() { new List<object>() { 6 } });

      packets.Sort(new ListComparer());

      //var sb = new StringBuilder();
      //foreach (var packet in packets)
      //{
      //  ListToString(packet, sb);

      //  Console.WriteLine(sb.ToString());

      //  sb.Clear();
      //}

      var i1 = packets.FindIndex(f => f.Count == 1 && f[0] is List<object> l1 && l1.Count == 1 && l1[0] is int v && v == 2) + 1;
      var i2 = packets.FindIndex(f => f.Count == 1 && f[0] is List<object> l1 && l1.Count == 1 && l1[0] is int v && v == 6) + 1;

      return (i1 * i2).ToString();
    }

    private void ListToString(List<object> list, StringBuilder builder)
    {
      builder.Append('[');

      bool isFirst = true;

      foreach (var item in list)
      {
        if (isFirst)
        {
          isFirst = false;
        }
        else
        {
          builder.Append(',');
        }

        if (item is List<object> l1)
        {
          ListToString(l1, builder);
        }
        else
        {
          builder.Append(item.ToString());
        }
      }

      builder.Append(']');
    }

    private class ListComparer : IComparer<List<object>>
    {
      public int Compare(List<object>? x, List<object>? y)
      {
        return IsValid(x, y);
      }
    }
  }
}
