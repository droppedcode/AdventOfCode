using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Helpers
{
  public class NumberSystem
  {
    private const string alpha = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public List<byte> Bytes { get; } = new List<byte>();

    public int System { get; }

    public bool IsMinus { get; private set; }

    public NumberSystem(int system, int value = 0)
    {
      if (system < 2 || system > 256) throw new ArgumentOutOfRangeException(nameof(system), "System must be between 2 and 256 inclusive.");

      System = system;

      Set(value);
    }

    public void Set(int value)
    {
      if (value < 0)
      {
        value = -value;
        IsMinus = true;
      }
      else
      {
        IsMinus = false;
      }

      Bytes.Clear();

      var index = 0;

      while (value > 0)
      {
        Bytes[index] = (byte)(value % System);
        value /= System;

        index++;
      }
    }

    public int Value => Get();

    public int Get()
    {
      var value = 0;

      for (var i = Bytes.Count - 1; i >= 0; i--)
      {
        value = value * System + Bytes[i];
      }

      return IsMinus ? -value : value;
    }

    public override string ToString() => ToString(alpha);

    public string ToString(string chars)
    {
      var sb = new StringBuilder();

      if (System > chars.Length)
      {
        for (var i = Bytes.Count - 1; i > 0; i--)
        {
          sb.Append(Bytes[i]);
          sb.Append(',');
        }

        sb.Append(Bytes[0]);
      }
      else
      {
        for (var i = Bytes.Count - 1; i >= 0; i--)
        {
          sb.Append(chars[Bytes[i]]);
        }
      }

      return sb.ToString();
    }

    public static NumberSystem operator +(NumberSystem a) => a;
    public static NumberSystem operator -(NumberSystem a) => new(a.System, -a.Value);
    public static NumberSystem operator +(NumberSystem a, NumberSystem b) => new(a.System, a.Value + b.Value);
    public static NumberSystem operator -(NumberSystem a, NumberSystem b) => new(a.System, a.Value - b.Value);
    public static NumberSystem operator ++(NumberSystem a) => new(a.System, a.Value + 1);
    public static NumberSystem operator --(NumberSystem a) => new(a.System, a.Value - 1);

  }
}
