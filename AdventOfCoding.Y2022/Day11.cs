using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day11 : FileDay
  {
    private class Monkey
    {
      public int Index { get; }
      public Func<ulong, ulong> Operation { get; set; }
      public ulong Test { get; set; }
      public Monkey TrueMonkey { get; set; }
      public Monkey FalseMonkey { get; set; }
      public Queue<ulong> Items { get; } = new Queue<ulong>();
      public ulong InspectionCount { get; private set; }

      public Monkey(int index)
      {
        Index = index;
      }

      public void Round()
      {
        while (Items.TryDequeue(out var item))
        {
          InspectionCount++;

          item = Operation(item) / 3;

          if (item % (ulong)Test == 0)
          {
            TrueMonkey.Items.Enqueue(item);
          }
          else
          {
            FalseMonkey.Items.Enqueue(item);
          }
        }
      }

      public void Round2(ulong mod)
      {
        while (Items.TryDequeue(out var item))
        {
          InspectionCount++;

          item = Operation(item);

          item %= mod;

          if (item % Test == 0)
          {
            TrueMonkey.Items.Enqueue(item);
          }
          else
          {
            FalseMonkey.Items.Enqueue(item);
          }
        }
      }

      public override string ToString()
      {
        return $"M{Index}, test: {Test}, items: [{string.Join(",", Items)}]";
      }
    }

    public override string Task1()
    {
      checked
      {
        List<Monkey> monkeys = ReadMonkeys();

        for (var i = 0; i < 20; i++)
        {
          foreach (var monkey in monkeys)
          {
            monkey.Round();
          }
        }

        var m = monkeys.OrderByDescending(o => o.InspectionCount).Take(2).ToArray();
        return (m[0].InspectionCount * m[1].InspectionCount).ToString();
      }
    }

    private List<Monkey> ReadMonkeys()
    {
      var monkeys = new List<Monkey>();

      Monkey getMonkey(int index)
      {
        while (monkeys.Count <= index)
        {
          monkeys.Add(new Monkey(monkeys.Count));
        }

        return monkeys[index];
      }

      Monkey current = null;
      var monkeyIndex = 0;
      var lineIndex = 0;

      foreach (var line in ReadLines())
      {
        switch (lineIndex)
        {
          case 0:
            if (monkeys.Count > monkeyIndex)
            {
              current = monkeys[monkeyIndex];
            }
            else
            {
              current = new Monkey(monkeyIndex);
              monkeys.Add(current);
            }
            break;
          case 1:
            foreach (var item in GetInts(line, ',', ' ', 18))
            {
              current.Items.Enqueue((ulong)item);
            }
            break;
          case 2:
            var op = line[23];
            var arg2 = GetWord(line, 25);

            var isNumber = char.IsDigit(arg2[0]);
            var arg2Num = isNumber ? (ulong)GetInt(arg2) : 0;

            current.Operation = (op, isNumber) switch
            {
              ('+', true) => (old) => old + arg2Num,
              ('*', true) => (old) => old * arg2Num,
              ('+', false) => (old) => old + old,
              ('*', false) => (old) => old * old,
              _ => throw new InvalidOperationException()
            };
            break;
          case 3:
            current.Test = (ulong)GetInt(line, 21);
            break;
          case 4:
            current.TrueMonkey = getMonkey(GetInt(line, 29));
            break;
          case 5:
            current.FalseMonkey = getMonkey(GetInt(line, 30));
            break;
          case 6:
            lineIndex = -1;
            monkeyIndex++;
            break;
        }

        lineIndex++;
      }

      return monkeys;
    }

    public override string Task2()
    {
      // For some reason this solution is not accepted

      checked
      {
        List<Monkey> monkeys = ReadMonkeys();

        var mod = 1ul;
        foreach (var monkey in monkeys)
        {
          mod *= monkey.Test;
        }

        for (var i = 0; i < 10000; i++)
        {
          foreach (var monkey in monkeys)
          {
            monkey.Round2(mod);
          }
        }

        var m = monkeys.OrderByDescending(o => o.InspectionCount).Take(2).ToArray();
        return (m[0].InspectionCount * m[1].InspectionCount).ToString();
      }
    }
  }
}
