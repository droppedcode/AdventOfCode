using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day06 : FileDay
  {
    private int FindStartOfPacket(string stream)
    {
      var c0 = stream[0];
      var c1 = stream[0];
      var c2 = stream[0];

      var index = 0;

      foreach (var c in stream)
      {
        index++;

        if (c2 != c1 && c2 != c0 && c2 != c && c1 != c0 && c1 != c && c0 != c) return index;

        c2 = c1;
        c1 = c0;
        c0 = c;
      }

      return -1;
    }

    public override string Task1()
    {
      var file = ReadFile();

      return FindStartOfPacket(file).ToString();
    }

    public override string Task2()
    {
      var file = ReadFile();

      var start = FindStartOfPacket(file);

      var check = new char[13];

      for (var i = 0; i < check.Length; i++)
      {
        check[i] = file[start + i];
      }

      for (var i = start + check.Length; i < file.Length; i++)
      {
        var c = file[i];

        if (!check.Contains(c) && check.Distinct().Count() == check.Length) return (i + 1).ToString();

        for (var ci = 1; ci < check.Length; ci++)
        {
          check[ci - 1] = check[ci];
        }

        check[check.Length - 1] = c;
      }

      return null;
    }
  }
}
