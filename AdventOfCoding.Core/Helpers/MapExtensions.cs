using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Helpers
{
  public static class MapExtensions
  {
    public static string DrawMap(this bool[,] map, char trueCharacter, char falseCharacter, bool drawBorder = true, Func<(int, int), char?> getCharacter = null, bool invertX = false, bool invertY = false, int fromX = 0, int toX = int.MaxValue, int fromY = 0, int toY = int.MaxValue)
    {
      var sb = new StringBuilder();

      var xm = Math.Min(toX, map.GetLength(0));
      var ym = map.GetLength(1);

      if (drawBorder)
      {
        sb.Append(' ');
        for (var x = 0; x < xm; x++)
        {
          sb.Append('-');
        }
        sb.Append(' ');
      }

      sb.AppendLine();

      var yMax = Math.Min(toY, ym);
      for (var yi = fromY; yi < yMax; yi++)
      {
        var y = invertY ? yMax - yi - 1 : yi;

        if (drawBorder)
        {
          sb.Append('|');
        }

        if (invertX)
        {
          for (var x = xm - 1; x >= fromX; x--)
          {
            sb.Append(getCharacter?.Invoke((x, y)) ?? (map[x, y] ? trueCharacter : falseCharacter));
          }
        }
        else
        {
          for (var x = fromX; x < xm; x++)
          {
            sb.Append(getCharacter?.Invoke((x, y)) ?? (map[x, y] ? trueCharacter : falseCharacter));
          }
        }

        if (drawBorder)
        {
          sb.Append('|');
        }

        sb.AppendLine();
      }

      if (drawBorder)
      {
        sb.Append(' ');
        for (var x = 0; x < xm; x++)
        {
          sb.Append('-');
        }
        sb.Append(' ');
      }

      return sb.ToString();
    }

    public static string DrawMap(this byte[] map, char trueCharacter, char falseCharacter, bool drawBorder = true, Func<(int, int), char?> getCharacter = null, bool invertX = false, bool invertY = false, int fromX = 0, int toX = int.MaxValue, int fromY = 0, int toY = int.MaxValue)
    {
      var sb = new StringBuilder();

      var xm = Math.Min(toX, 8);
      var ym = map.Length;

      if (drawBorder)
      {
        sb.Append(' ');
        for (var x = 0; x < xm; x++)
        {
          sb.Append('-');
        }
        sb.Append(' ');
      }

      sb.AppendLine();

      var yMax = Math.Min(toY, ym);
      for (var yi = fromY; yi < yMax; yi++)
      {
        var y = invertY ? yMax - yi - 1 : yi;

        if (drawBorder)
        {
          sb.Append('|');
        }

        if (invertX)
        {
          for (var x = xm - 1; x >= fromX; x--)
          {
            var bit = 1 << x;
            sb.Append(getCharacter?.Invoke((x, y)) ?? (((map[y] & bit) == bit) ? trueCharacter : falseCharacter));
          }
        }
        else
        {
          for (var x = fromX; x < xm; x++)
          {
            var bit = 1 << x;
            sb.Append(getCharacter?.Invoke((x, y)) ?? (((map[y] & bit) == bit) ? trueCharacter : falseCharacter));
          }
        }

        if (drawBorder)
        {
          sb.Append('|');
        }

        sb.AppendLine();
      }

      if (drawBorder)
      {
        sb.Append(' ');
        for (var x = 0; x < xm; x++)
        {
          sb.Append('-');
        }
        sb.Append(' ');
      }

      return sb.ToString();
    }

    public static string DrawMap<T>(this T[,,] map, Func<T, char> getCharacter, bool drawBorder = true, bool invertX = false, bool invertY = false, int fromX = 0, int toX = int.MaxValue, int fromY = 0, int toY = int.MaxValue)
    {
      var sb = new StringBuilder();

      var xm = Math.Min(toX, map.GetLength(0));
      var ym = map.GetLength(1);
      var zm = map.GetLength(2);

      if (drawBorder)
      {
        sb.Append(' ');
        for (var x = 0; x < xm; x++)
        {
          sb.Append('-');
        }
        sb.Append(' ');
      }


      for (var zi = 0; zi < zm; zi++)
      {
        sb.AppendLine();

        var yMax = Math.Min(toY, ym);
        for (var yi = fromY; yi < yMax; yi++)
        {
          var y = invertY ? yMax - yi - 1 : yi;

          if (drawBorder)
          {
            sb.Append('|');
          }

          if (invertX)
          {
            for (var x = xm - 1; x >= fromX; x--)
            {
              sb.Append(getCharacter(map[x, y, zi]));
            }
          }
          else
          {
            for (var x = fromX; x < xm; x++)
            {
              sb.Append(getCharacter(map[x, y, zi]));
            }
          }

          if (drawBorder)
          {
            sb.Append('|');
          }

          sb.AppendLine();
        }

        if (drawBorder)
        {
          sb.Append(' ');
          for (var x = 0; x < xm; x++)
          {
            sb.Append('-');
          }
          sb.Append(' ');
        }
      }

      return sb.ToString();
    }
  }
}
