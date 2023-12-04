using AdventOfCoding.Core;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day05 : FileDay
  {
    public override string Task1()
    {
      var isReadingStacks = true;
      var stacks = new List<Stack<char>>();

      foreach (var line in ReadLines())
      {
        if (isReadingStacks)
        {
          if (line == "")
          {
            isReadingStacks = false;

            foreach (var stack in stacks)
            {
              var buf = stack.ToList();
              stack.Clear();
              foreach (var item in buf)
              {
                stack.Push(item);
              }
            }
          }
          else if (line[1] != '1')
          {
            var count = (line.Length + 1) / 4;

            for (var i = stacks.Count; i < count; i++)
            {
              stacks.Add(new Stack<char>());
            }

            for (var i = 0; i < count; i++)
            {
              var c = line[i * 4 + 1];

              if (c != ' ')
              {
                stacks[i].Push(c);
              }
            }
          }
        }
        else
        {
          var index = 5;
          var c = GetInt(line, out index, index);
          index += 5;
          var f = GetInt(line, out index, index) - 1;
          index += 3;
          var t = GetInt(line, index) - 1;

          for (var i = 0; i < c; i++)
          {
            stacks[t].Push(stacks[f].Pop());
          }
        }
      }

      return new string(stacks.Select(s => s.Pop()).ToArray());
    }

    public override string Task2()
    {
      var isReadingStacks = true;
      var stacks = new List<Stack<char>>();

      foreach (var line in ReadLines())
      {
        if (isReadingStacks)
        {
          if (line == "")
          {
            isReadingStacks = false;

            foreach (var stack in stacks)
            {
              var buf = stack.ToList();
              stack.Clear();
              foreach (var item in buf)
              {
                stack.Push(item);
              }
            }
          }
          else if (line[1] != '1')
          {
            var count = (line.Length + 1) / 4;

            for (var i = stacks.Count; i < count; i++)
            {
              stacks.Add(new Stack<char>());
            }

            for (var i = 0; i < count; i++)
            {
              var c = line[i * 4 + 1];

              if (c != ' ')
              {
                stacks[i].Push(c);
              }
            }
          }
        }
        else
        {
          var index = 5;
          var c = GetInt(line, out index, index);
          index += 5;
          var f = GetInt(line, out index, index) - 1;
          index += 3;
          var t = GetInt(line, index) - 1;

          var temp = new char[c];

          for (var i = 0; i < c; i++)
          {
            temp[i] = stacks[f].Pop();
          }

          for (var i = 0; i < c; i++)
          {
            stacks[t].Push(temp[c - i - 1]);
          }
        }
      }

      return new string(stacks.Select(s => s.Pop()).ToArray());
    }
  }
}
