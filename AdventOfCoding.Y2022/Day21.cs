using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day21 : FileDay
  {
    private long GetValue(Dictionary<string, long> values, Dictionary<string, (string, char, string)> maths, string monkey)
    {
      if (values.TryGetValue(monkey, out var value)) return value;

      var m = maths[monkey];
      value = m.Item2 switch
      {
        '+' => GetValue(values, maths, m.Item1) + GetValue(values, maths, m.Item3),
        '*' => GetValue(values, maths, m.Item1) * GetValue(values, maths, m.Item3),
        '-' => GetValue(values, maths, m.Item1) - GetValue(values, maths, m.Item3),
        '/' => GetValue(values, maths, m.Item1) / GetValue(values, maths, m.Item3),
        _ => throw new InvalidOperationException()
      };

      values[monkey] = value;

      return value;
    }

    private bool TryGetValue(Dictionary<string, long> values, Dictionary<string, (string, char, string)> maths, string monkey, out long value)
    {
      if (values.TryGetValue(monkey, out value)) return true;

      if (!maths.TryGetValue(monkey, out var m)) return false;

      if (!TryGetValue(values, maths, m.Item1, out var a)) return false;
      if (!TryGetValue(values, maths, m.Item3, out var b)) return false;

      value = m.Item2 switch
      {
        '+' => a + b,
        '*' => a * b,
        '-' => a - b,
        '/' => a / b,
        _ => throw new InvalidOperationException()
      };

      values[monkey] = value;

      return true;
    }

    private long HumnValue(Dictionary<string, long> values, Dictionary<string, (string, char, string)> maths, string monkey, long result)
    {
      if (monkey == "humn") return result;

      var m = maths[monkey];

      if (TryGetValue(values, maths, m.Item1, out var a))
      {
        return m.Item2 switch
        {
          '+' => HumnValue(values, maths, m.Item3, result - a),
          '*' => HumnValue(values, maths, m.Item3, result / a),
          '-' => HumnValue(values, maths, m.Item3, a - result),
          '/' => HumnValue(values, maths, m.Item3, a / result),
          _ => throw new InvalidOperationException()
        };
      }
      else
      {
        var b = GetValue(values, maths, m.Item3);
        return m.Item2 switch
        {
          '+' => HumnValue(values, maths, m.Item1, result - b),
          '*' => HumnValue(values, maths, m.Item1, result / b),
          '-' => HumnValue(values, maths, m.Item1, result + b),
          '/' => HumnValue(values, maths, m.Item1, result * b),
          _ => throw new InvalidOperationException()
        };
      }      
    }

    public override string Task1()
    {
      var values = new Dictionary<string, long>();
      var maths = new Dictionary<string, (string, char, string)>();

      foreach (var line in ReadLines())
      {
        var m = line[..4];
        if (char.IsDigit(line[6]))
        {
          values.Add(m, GetInt(line, 6));
        }
        else
        {
          maths.Add(m, (line[6..10], line[11], line[13..17]));
        }
      }

      return GetValue(values, maths, "root").ToString();
    }

    public override string Task2()
    {
      var values = new Dictionary<string, long>();
      var maths = new Dictionary<string, (string, char, string)>();

      var left = "";
      var right = "";

      foreach (var line in ReadLines())
      {
        var m = line[..4];
        if (m == "root")
        {
          left = line[6..10];
          right = line[13..17];
        }
        else if (m == "humn")
        {
        }
        else if (char.IsDigit(line[6]))
        {
          values.Add(m, GetInt(line, 6));
        }
        else
        {
          maths.Add(m, (line[6..10], line[11], line[13..17]));
        }
      }

      var humn = TryGetValue(values, maths, left, out var leftValue) 
        ? HumnValue(values, maths, right, leftValue) 
        : HumnValue(values, maths, left, GetValue(values, maths, right));

      return humn.ToString();
    }
  }
}
