using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Extensions;
public static class StringParseExtensions
{
  public static int ParseFirstInt(this string str)
  {
    return ParseFirstInt(str.AsSpan());
  }

  public static int ParseFirstInt(this ReadOnlySpan<char> str, int from = 0)
  {
    var value = 0;

    for (var i = from; i < str.Length; i++)
    {
      if (str[i] == ' ')
      {
        from++;
      }
      else
      {
        break;
      }
    }

    for (var i = from; i < str.Length; i++)
    {
      if (!char.IsDigit(str[i])) break;

      value *= 10;
      value += str[i] - '0';
    }

    return value;
  }

  public static uint ParseFirstUInt(this ReadOnlySpan<char> str, int from = 0)
  {
    uint value = 0;

    for (var i = from; i < str.Length; i++)
    {
      if (str[i] == ' ')
      {
        from++;
      }
      else
      {
        break;
      }
    }

    for (var i = from; i < str.Length; i++)
    {
      if (!char.IsDigit(str[i])) break;

      value *= 10;
      value += (uint)(str[i] - '0');
    }

    return value;
  }

  public static ulong ParseFirstULong(this ReadOnlySpan<char> str, int from = 0)
  {
    ulong value = 0;

    for (var i = from; i < str.Length; i++)
    {
      if (str[i] == ' ')
      {
        from++;
      }
      else
      {
        break;
      }
    }

    for (var i = from; i < str.Length; i++)
    {
      if (!char.IsDigit(str[i])) break;

      value *= 10;
      value += (uint)(str[i] - '0');
    }

    return value;
  }
}
