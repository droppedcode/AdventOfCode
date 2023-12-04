using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2023
{
  internal partial class Day02 : FileDay
  {
    private SearchValues<char> _gameSplitters = SearchValues.Create([';']);
    private SearchValues<char> _showSplitters = SearchValues.Create([',']);

    private int _redMax = 12;
    private int _greenMax = 13;
    private int _blueMax = 14;

    private bool IsValid(string game)
    {
      foreach (var show in game.SplitBy(_gameSplitters))
      {
        var index = show.Value.IndexOf(":");

        foreach (var cube in show.Value.Slice(index + 1).SplitBy(_showSplitters))
        {
          var value = cube.Value.ParseFirstInt(1);
          switch (cube.Value[^1])
          {
            // red
            case 'd':
              if (_redMax < value) return false;
              break;
            // green
            case 'n':
              if (_greenMax < value) return false;
              break;
            // blue
            case 'e':
              if (_blueMax < value) return false;
              break;
          }
        }
      }

      return true;
    }

    public override string Task1()
    {
      var index = 0;
      var sum = 0;
      foreach (var game in ReadLines())
      {
        index++;

        if (IsValid(game))
        {
          sum += index;
        }
      }
      return sum.ToString();
    }


    private int Power(string game)
    {
      var red = 0;
      var green = 0;
      var blue = 0;

      foreach (var show in game.SplitBy(_gameSplitters))
      {
        var index = show.Value.IndexOf(":");

        foreach (var cube in show.Value.Slice(index + 1).SplitBy(_showSplitters))
        {
          var value = cube.Value.ParseFirstInt(1);
          switch (cube.Value[^1])
          {
            // red
            case 'd':
              if (value > red)
              {
                red = value;
              }
              break;
            // green
            case 'n':
              if (value > green)
              {
                green = value;
              }
              break;
            // blue
            case 'e':
              if (value > blue)
              {
                blue = value;
              }
              break;
          }
        }
      }

      return red * green * blue;
    }

    public override string Task2()
    {
      var index = 0;
      var sum = 0;
      foreach (var game in ReadLines())
      {
        index++;

        sum += Power(game);
      }
      return sum.ToString();
    }
  }
}
