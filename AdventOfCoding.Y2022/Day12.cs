using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day12 : FileDay
  {
    private List<(int row, int col)> DrawNextLevel(List<char[]> map, int[,] steps, List<(int row, int col)> positions, int step)
    {
      var result = new List<(int row, int col)>();

      foreach (var position in positions)
      {
        var possibles = new (int row, int col)[] {
          (position.row - 1, position.col),
          (position.row, position.col - 1),
          (position.row + 1, position.col),
          (position.row, position.col + 1)
        };

        foreach (var possible in possibles)
        {
          if (IsValidPosition(map, possible) && steps[possible.row, possible.col] == 0 && CanGoBack(map, position, possible))
          {
            result.Add(possible);
            steps[possible.row, possible.col] = step;
          }
        }
      }

      return result;
    }

    private bool IsValidPosition(List<char[]> map, (int row, int col) position)
    {
      if (position.row < 0) return false;
      if (position.col < 0) return false;
      if (position.row >= map.Count) return false;
      if (position.col >= map[0].Length) return false;

      return true;
    }

    private bool CanGoBack(List<char[]> map, (int row, int col) position, (int row, int col) target)
    {
      var currentPosition = map[position.row][position.col];
      var targetPosition = map[target.row][target.col];

      return currentPosition - 1 <= targetPosition;
    }

    public override string Task1()
    {
      var map = new List<char[]>();

      (int row, int col) endPosition = (-1, -1);
      (int row, int col) startPosition = (-1, -1);

      foreach (var line in ReadLines())
      {
        map.Add(line.ToCharArray());

        for (var i = 0; i < line.Length; i++)
        {
          if (line[i] == 'S')
          {
            startPosition = (map.Count - 1, i);
          }
          else if (line[i] == 'E')
          {
            endPosition = (map.Count - 1, i);
          }
        }
      }

      map[startPosition.row][startPosition.col] = 'a';
      map[endPosition.row][endPosition.col] = 'z';

      var steps = new int[map.Count, map[0].Length];
      steps[endPosition.row, endPosition.col] = 1;

      var positions = new List<(int row, int col)>() {
        endPosition
      };

      var step = 2;

      while (positions.Count > 0)
      {
        positions = DrawNextLevel(map, steps, positions, step);

        //Console.WriteLine();
        //Console.WriteLine();
        //for (var y = 0; y < steps.GetLength(0); y++)
        //{
        //  for (var x = 0; x < steps.GetLength(1); x++)
        //  {
        //    if (steps[y, x] == step)
        //    {
        //      Console.Write('*');
        //    }
        //    else if (steps[y, x] == 0)
        //    {
        //      Console.Write(' ');
        //    }
        //    else
        //    {
        //      Console.Write('+');
        //    }
        //  }
        //  Console.WriteLine();
        //}

        step++;
      }

      return (steps[startPosition.row, startPosition.col] - 1).ToString();
    }

    public override string Task2()
    {
      var map = new List<char[]>();

      (int row, int col) endPosition = (-1, -1);
      (int row, int col) startPosition = (-1, -1);

      foreach (var line in ReadLines())
      {
        map.Add(line.ToCharArray());

        for (var i = 0; i < line.Length; i++)
        {
          if (line[i] == 'S')
          {
            startPosition = (map.Count - 1, i);
          }
          else if (line[i] == 'E')
          {
            endPosition = (map.Count - 1, i);
          }
        }
      }

      map[startPosition.row][startPosition.col] = 'a';
      map[endPosition.row][endPosition.col] = 'z';

      var steps = new int[map.Count, map[0].Length];
      steps[endPosition.row, endPosition.col] = 1;

      var positions = new List<(int row, int col)>() {
        endPosition
      };

      var step = 2;

      while (positions.Count > 0)
      {
        positions = DrawNextLevel(map, steps, positions, step);

        foreach (var position in positions)
        {
          if (map[position.row][position.col] == 'a')
          {
            return (step - 1).ToString();
          }
        }

        step++;
      }

      throw new InvalidOperationException();
    }
  }
}
