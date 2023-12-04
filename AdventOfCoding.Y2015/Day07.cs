using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day07 : FileDay
  {
    public override string Task1()
    {
      var values = new Dictionary<string, ushort>();
      var oAnd = new Dictionary<string, (string, string)>();
      var oOr = new Dictionary<string, (string, string)>();
      var oLshift = new Dictionary<string, (string, int)>();
      var oRshift = new Dictionary<string, (string, int)>();
      var oNot = new Dictionary<string, string>();
      var oDirect = new Dictionary<string, string>();

      string storeIfValue(string v)
      {
        if (char.IsDigit(v, 0))
        {
          var buf = $"_{v}";
          values[buf] = (ushort)GetInt(v);
          v = buf;
        }

        return v;
      }

      foreach (var line in ReadLines())
      {
        var index = line.IndexOf(" -> ");
        var wire = line[(index + 4)..];

        if (line[0] == 'N')
        {
          oNot[wire] = storeIfValue(new string(GetWord(line, 4)));
        }
        else
        {
          var a = new string(GetWord(line));
          var op = new string(GetWord(line, a.Length + 1));
          var b = new string(GetWord(line, a.Length + op.Length + 2));

          switch (op)
          {
            case "AND":
              oAnd[wire] = (storeIfValue(a), storeIfValue(b));
              break;
            case "OR":
              oOr[wire] = (storeIfValue(a), storeIfValue(b));
              break;
            case "LSHIFT":
              oLshift[wire] = (storeIfValue(a), GetInt(b));
              break;
            case "RSHIFT":
              oRshift[wire] = (storeIfValue(a), GetInt(b));
              break;
            case "->":
              oDirect[wire] = storeIfValue(a);
              break;
            default:
              throw new InvalidOperationException();
          }
        }
      }

      var total = values.Count + oAnd.Count + oOr.Count + oLshift.Count + oRshift.Count + oNot.Count + oDirect.Count;

      while (values.Count < total)
      {
        foreach (var kv in oNot)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value))
          {
            values[kv.Key] = (ushort)(~values[kv.Value]);
          }
        }

        foreach (var kv in oDirect)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value))
          {
            values[kv.Key] = values[kv.Value];
          }
        }

        foreach (var kv in oLshift)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] << kv.Value.Item2);
          }
        }

        foreach (var kv in oRshift)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] >> kv.Value.Item2);
          }
        }

        foreach (var kv in oAnd)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1) && values.ContainsKey(kv.Value.Item2))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] & values[kv.Value.Item2]);
          }
        }

        foreach (var kv in oOr)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1) && values.ContainsKey(kv.Value.Item2))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] | values[kv.Value.Item2]);
          }
        }
      }

      return values["a"].ToString();
    }

    public override string Task2()
    {
      var values = new Dictionary<string, ushort>();
      var oAnd = new Dictionary<string, (string, string)>();
      var oOr = new Dictionary<string, (string, string)>();
      var oLshift = new Dictionary<string, (string, int)>();
      var oRshift = new Dictionary<string, (string, int)>();
      var oNot = new Dictionary<string, string>();
      var oDirect = new Dictionary<string, string>();

      string storeIfValue(string v)
      {
        if (char.IsDigit(v, 0))
        {
          var buf = $"_{v}";
          values[buf] = (ushort)GetInt(v);
          v = buf;
        }

        return v;
      }

      foreach (var line in ReadLines())
      {
        var index = line.IndexOf(" -> ");
        var wire = line[(index + 4)..];

        if (line[0] == 'N')
        {
          oNot[wire] = storeIfValue(new string(GetWord(line, 4)));
        }
        else
        {
          var a = new string(GetWord(line));
          var op = new string(GetWord(line, a.Length + 1));
          var b = new string(GetWord(line, a.Length + op.Length + 2));

          switch (op)
          {
            case "AND":
              oAnd[wire] = (storeIfValue(a), storeIfValue(b));
              break;
            case "OR":
              oOr[wire] = (storeIfValue(a), storeIfValue(b));
              break;
            case "LSHIFT":
              oLshift[wire] = (storeIfValue(a), GetInt(b));
              break;
            case "RSHIFT":
              oRshift[wire] = (storeIfValue(a), GetInt(b));
              break;
            case "->":
              oDirect[wire] = storeIfValue(a);
              break;
            default:
              throw new InvalidOperationException();
          }
        }
      }

      var bOverride = Task1();

      values["b"] = (ushort)GetInt(bOverride);
      oAnd.Remove("b");
      oOr.Remove("b");
      oLshift.Remove("b");
      oRshift.Remove("b");
      oNot.Remove("b");
      oDirect.Remove("b");

      var total = values.Count + oAnd.Count + oOr.Count + oLshift.Count + oRshift.Count + oNot.Count + oDirect.Count;

      while (values.Count < total)
      {
        foreach (var kv in oNot)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value))
          {
            values[kv.Key] = (ushort)(~values[kv.Value]);
          }
        }

        foreach (var kv in oDirect)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value))
          {
            values[kv.Key] = values[kv.Value];
          }
        }

        foreach (var kv in oLshift)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] << kv.Value.Item2);
          }
        }

        foreach (var kv in oRshift)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] >> kv.Value.Item2);
          }
        }

        foreach (var kv in oAnd)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1) && values.ContainsKey(kv.Value.Item2))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] & values[kv.Value.Item2]);
          }
        }

        foreach (var kv in oOr)
        {
          if (values.ContainsKey(kv.Key)) continue;

          if (values.ContainsKey(kv.Value.Item1) && values.ContainsKey(kv.Value.Item2))
          {
            values[kv.Key] = (ushort)(values[kv.Value.Item1] | values[kv.Value.Item2]);
          }
        }
      }

      return values["a"].ToString();
    }
  }
}
