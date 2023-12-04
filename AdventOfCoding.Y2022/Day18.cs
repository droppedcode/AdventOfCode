using AdventOfCoding.Core;
using AdventOfCoding.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day18 : FileDay
  {
    public override string Task1()
    {
      var cubes = new List<(int x, int y, int z)>();
      var sides = new List<(int x, int y, int z)>();

      var minX = int.MaxValue;
      var minY = int.MaxValue;
      var minZ = int.MaxValue;

      var maxX = int.MinValue;
      var maxY = int.MinValue;
      var maxZ = int.MinValue;

      foreach (var line in ReadLines())
      {
        var cube = Get3Ints(line, ',');

        if (cube.Item1 < minX) { minX = cube.Item1; }
        if (cube.Item2 < minY) { minY = cube.Item2; }
        if (cube.Item3 < minZ) { minZ = cube.Item3; }
        if (cube.Item1 > maxX) { maxX = cube.Item1; }
        if (cube.Item2 > maxY) { maxY = cube.Item2; }
        if (cube.Item3 > maxZ) { maxZ = cube.Item3; }

        cubes.Add(cube);
      }

      foreach (var cube in cubes)
      {
        sides.AddRange(GetSides(cube));
      }

      foreach (var cube in cubes)
      {
        sides.RemoveAll(c => c == cube);
      }

      return sides.Count.ToString();
    }

    private static IEnumerable<(int x, int y, int z)> GetSides((int x, int y, int z) item)
    {
      var (x, y, z) = item;

      yield return (x - 1, y, z);
      yield return (x + 1, y, z);
      yield return (x, y - 1, z);
      yield return (x, y + 1, z);
      yield return (x, y, z - 1);
      yield return (x, y, z + 1);
    }

    private static IEnumerable<(int x, int y, int z)> GetSides((int x, int y, int z) item, int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
    {
      var (x, y, z) = item;

      if (x > 0) yield return (x - 1, y, z);
      if (x < maxX - minX + 2) yield return (x + 1, y, z);
      if (y > 0) yield return (x, y - 1, z);
      if (y < maxY - minY + 2) yield return (x, y + 1, z);
      if (z > 0) yield return (x, y, z - 1);
      if (z < maxZ - minZ + 2) yield return (x, y, z + 1);
    }

    public override string Task2()
    {
      var cubes = new List<(int x, int y, int z)>();

      var minX = int.MaxValue;
      var minY = int.MaxValue;
      var minZ = int.MaxValue;

      var maxX = int.MinValue;
      var maxY = int.MinValue;
      var maxZ = int.MinValue;

      foreach (var line in ReadLines())
      {
        var cube = Get3Ints(line, ',');

        if (cube.Item1 < minX) { minX = cube.Item1; }
        if (cube.Item2 < minY) { minY = cube.Item2; }
        if (cube.Item3 < minZ) { minZ = cube.Item3; }
        if (cube.Item1 > maxX) { maxX = cube.Item1; }
        if (cube.Item2 > maxY) { maxY = cube.Item2; }
        if (cube.Item3 > maxZ) { maxZ = cube.Item3; }

        cubes.Add(cube);
      }

      var grid = new byte[maxX - minX + 3, maxY - minY + 3, maxZ - minZ + 3];

      for (var i = 0; i < cubes.Count; i++)
      {
        var cube = cubes[i];
        cube = (cube.x - minX + 1, cube.y - minY + 1, cube.z - minZ + 1);
        cubes[i] = cube;
        grid[cube.x, cube.y, cube.z] = 2;
      }

      void air((int x, int y, int z) cube)
      {
        var list = new List<(int x, int y, int z)>()
        {
          cube
        };

        for (var i = 0; i < list.Count; i++)
        {
          var current = list[i];
          if (grid[current.x, current.y, current.z] == 0)
          {
            grid[current.x, current.y, current.z] = 1;
            foreach (var side in GetSides(current, minX, minY, minZ, maxX, maxY, maxZ))
            {
              if (grid[side.x, side.y, side.z] == 0)
              {
                list.Add(side);
              }
            }
          }
        }
      }

      air((0, 0, 0));

      var count = 0;

      foreach (var cube in cubes)
      {
        foreach (var side in GetSides(cube, minX, minY, minZ, maxX, maxY, maxZ))
        {
          if (grid[side.x, side.y, side.z] == 1)
          {
            count++;
          }
        }
      }

      Console.WriteLine(grid.DrawMap(b => b switch
      {
        0 => '+',
        1 => ' ',
        2 => '#'
      }));

      return count.ToString();
    }
  }
}
