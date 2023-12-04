using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day08 : FileDay
  {
    const int _length = 99;

    public override string Task1()
    {
      var grid = ReadInput();

      var visible = new bool[_length, _length];

      for (var row = 0; row < _length; row++)
      {
        var max = -1;

        for (var col = 0; col < _length; col++)
        {
          var tree = grid[row, col];
          if (tree > max)
          {
            visible[row, col] = true;
            max = tree;
          }
        }

        max = -1;

        for (var col = _length - 1; col >= 0; col--)
        {
          var tree = grid[row, col];
          if (tree > max)
          {
            visible[row, col] = true;
            max = tree;
          }
        }
      }


      for (var col = 0; col < _length; col++)
      {
        var max = -1;

        for (var row = 0; row < _length; row++)
        {
          var tree = grid[row, col];
          if (tree > max)
          {
            visible[row, col] = true;
            max = tree;
          }
        }

        max = -1;

        for (var row = _length - 1; row >= 0; row--)
        {
          var tree = grid[row, col];
          if (tree > max)
          {
            visible[row, col] = true;
            max = tree;
          }
        }
      }

      var count = 0;

      for (var row = 0; row < _length; row++)
      {
        for (var col = 0; col < _length; col++)
        {
          if (visible[row, col])
          {
            count++;
          }
        }
      }

      return count.ToString();
    }

    private byte[,] ReadInput()
    {
      var row = 0;
      var grid = new byte[_length, _length];

      foreach (var line in ReadLines())
      {
        for (var col = 0; col < _length; col++)
        {
          grid[row, col] = (byte)(line[col] - '0');
        }

        row++;
      }

      return grid;
    }

    private int GetViewDistance(byte[,] grid, int row, int col, byte value)
    {
      return GetViewDistanceLeft(grid, row, col, value) * GetViewDistanceUp(grid, row, col, value) * GetViewDistanceRight(grid, row, col, value) * GetViewDistanceDown(grid, row, col, value);
    }

    private int GetViewDistanceLeft(byte[,] grid, int row, int col, byte value)
    {
      var count = 0;

      for (var i = col - 1; i >= 0; i--)
      {
        count++;

        if (grid[row, i] >= value)
        {
          break;
        }
      }

      return count;
    }

    private int GetViewDistanceUp(byte[,] grid, int row, int col, byte value)
    {
      var count = 0;

      for (var i = row - 1; i >= 0; i--)
      {
        count++;

        if (grid[i, col] >= value)
        {
          break;
        }
      }

      return count;
    }

    private int GetViewDistanceRight(byte[,] grid, int row, int col, byte value)
    {
      var count = 0;

      for (var i = col + 1; i < _length; i++)
      {
        count++;

        if (grid[row, i] >= value)
        {
          break;
        }
      }

      return count;
    }

    private int GetViewDistanceDown(byte[,] grid, int row, int col, byte value)
    {
      var count = 0;

      for (var i = row + 1; i < _length; i++)
      {
        count++;

        if (grid[i, col] >= value)
        {
          break;
        }
      }

      return count;
    }

    public override string Task2()
    {
      var grid = ReadInput();
      var max = 0;

      for (var row = 0; row < _length; row++)
      {
        for (var col = 0; col < _length; col++)
        {
          var dist = GetViewDistance(grid, row, col, grid[row, col]);
          if (dist > max)
          {
            max = dist;
          }
        }
      }

      return max.ToString();
    }
  }
}
