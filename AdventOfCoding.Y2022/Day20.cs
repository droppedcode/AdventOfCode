using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day20 : FileDay
  {
    private class Node
    {
      public Node Prev { get; set; }
      public Node Next { get; set; }
      public long Value { get; set; }

      public Node FindValue(long value) => Value == value ? this : Next.FindValue(value);

      public Node GetNext(long step = 1) => step == 0 ? this : Next.GetNext(step - 1);

      public Node GetPrev(long step = 1) => step == 0 ? this : Prev.GetPrev(step - 1);

      public void Move(long count)
      {
        if (Value == 0) return;

        var prev = Value < 0 ? GetNext((Value % (count - 1)) + count - 1) : GetNext(Value % (count - 1));

        //Console.WriteLine($"{Value} moves between {prev.Value} and {prev.Next.Value}");

        if (prev == this) return;

        Prev.Next = Next;
        Next.Prev = Prev;

        Prev = prev;
        Next = prev.Next;

        prev.Next.Prev = this;
        prev.Next = this;
      }

      public override string ToString()
      {
        return $"{Value} ({Prev?.Value} -> {Value} -> {Next?.Value})";
      }

      public string Print()
      {
        var sb = new StringBuilder();

        sb.Append(Value);

        var current = Next;
        while (current != this)
        {
          sb.Append(", ");
          sb.Append(current.Value);
          current = current.Next;
        }

        return sb.ToString();
      }

      public string PrintBack()
      {
        var sb = new StringBuilder();

        sb.Append(Value);

        var current = Prev;
        while (current != this)
        {
          sb.Append(", ");
          sb.Append(current.Value);
          current = current.Prev;
        }

        return sb.ToString();
      }
    }


    public override string Task1()
    {
      return Solve();
    }

    private string Solve(long key = 1L, int mixCount = 1)
    {
      Node first = null!;
      Node prev = null!;
      var list = new List<Node>();
      foreach (var line in ReadLines())
      {
        var current = new Node()
        {
          Value = key * GetInt(line)
        };

        list.Add(current);

        if (first == null)
        {
          first = current;
        }
        if (prev == null)
        {
          prev = current;
        }

        current.Prev = prev;
        prev.Next = current;

        prev = current;
      }

      if (prev == null || first == null) throw new InvalidOperationException();

      first.Prev = prev;
      prev.Next = first;

      for (var i = 0; i < mixCount; i++)
      {
        foreach (var node in list)
        {
          node.Move(list.Count);
          //Console.WriteLine(node.Print());
          //Console.WriteLine(node.PrintBack());
        }
      }

      var zero = first.FindValue(0);
      var m1 = zero.GetNext(1000);
      var m2 = m1.GetNext(1000);
      var m3 = m2.GetNext(1000);

      var result = m1.Value + m2.Value + m3.Value;

      return result.ToString();
    }

    public override string Task2()
    {
      return Solve(811589153L, 10);
    }
  }
}
