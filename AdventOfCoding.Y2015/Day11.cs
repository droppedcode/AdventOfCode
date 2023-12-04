using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day11 : StringDay
  {
    protected override string Input => "cqjxjnds";

    private const int numberOfLetters = 26;

    private const byte ib = (byte)'i';
    private const byte ob = (byte)'o';
    private const byte lb = (byte)'l';

    private byte[] GetPasswordBytes(string password)
    {
      var arr = new byte[password.Length];
      for (var i = 0; i < password.Length; i++)
      {
        arr[i] = (byte)(password[password.Length - i - 1] - 'a');
      }
      return arr;
    }

    private string GetPasswordString(byte[] password)
    {
      return new string(password.Reverse().Select(s => (char)(s + 'a')).ToArray());
    }

    private byte[] Increase(byte[] password)
    {
      for (var i = 0; i < password.Length; i++)
      {
        if (password[i] == numberOfLetters - 1)
        {
          password[i] = 0;
        }
        else
        {
          password[i]++;
          break;
        }
      }

      if (password[password.Length - 1] == 0)
      {
        password = new byte[password.Length + 1];
        password[password.Length - 1] = 1;
      }

      return password;
    }

    private bool IsValidPassword(byte[] password)
    {
      var pairs = 0;
      var increasing = false;

      var prev = byte.MaxValue;

      var increasingLimit = password.Length - 3;

      for (var i = 0; i < password.Length; i++)
      {
        var c = password[i];

        if (c is ib or ob or lb) return false;

        if (!increasing)
        {
          if (i == increasingLimit) return false;

          if (c == password[i + 1] + 1 && c == password[i + 2] + 2)
          {
            increasing = true;
          }
        }

        if (c == prev)
        {
          pairs++;
          prev = byte.MaxValue;
        }
        else
        {
          prev = c;
        }
      }

      return increasing && pairs >= 2;
    }

    public override string Task1()
    {
      var password = GetPasswordBytes(Input);

      password = Increase(password);

      while (!IsValidPassword(password))
      {
        password = Increase(password);
      }

      return GetPasswordString(password);
    }

    public override string Task2()
    {
      var password = GetPasswordBytes(Input);

      password = Increase(password);

      while (!IsValidPassword(password))
      {
        password = Increase(password);
      }

      password = Increase(password);

      while (!IsValidPassword(password))
      {
        password = Increase(password);
      }

      return GetPasswordString(password);
    }
  }
}
