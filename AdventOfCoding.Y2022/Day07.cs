using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day07 : FileDay
  {
    private static readonly Regex _command = new(@"^\$ (?<cmd>\w+)[ ]?(?<arg>.+)?$");
    private static readonly Regex _dir = new(@"^dir (?<name>.+)$");
    private static readonly Regex _file = new(@"^(?<size>\d+) (?<name>.+)$");

    private List<Dir> ReadDirs()
    {
      var root = new Dir(null);
      var directories = new List<Dir>()
      {
        root
      };
      var current = root;

      foreach (var line in ReadLines())
      {
        var match = _command.Match(line);
        if (match.Success)
        {
          switch (match.Groups[1].Value)
          {
            case "cd":
              switch (match.Groups[2].Value)
              {
                case "..":
                  current = current.Parent;
                  break;
                case "/":
                  current = root;
                  break;
                default:
                  current = current.Directories[match.Groups[2].Value];
                  break;
              }
              break;
            case "ls":
              // Do nothing
              break;
          }

          continue;
        }

        match = _dir.Match(line);
        if (match.Success)
        {
          var dir = new Dir(current);
          directories.Add(dir);
          current.Directories.Add(match.Groups[1].Value, dir);

          continue;
        }

        match = _file.Match(line);
        if (match.Success)
        {
          current.Files.Add(match.Groups[2].Value, GetInt(match.Groups[1].Value));

          continue;
        }
      }

      return directories;
    }

    public override string Task1()
    {
      var directories = ReadDirs();

      return directories.Where(w => w.TotalSize <= 100000).Sum(s => s.TotalSize).ToString();
    }

    public override string Task2()
    {
      var directories = ReadDirs();

      var free = 70000000 - directories[0].TotalSize;
      var needed = 30000000 - free;

      return directories.Where(w => w.TotalSize >= needed).OrderBy(o => o.TotalSize).First().TotalSize.ToString();
    }

    private class Dir
    {
      public Dir(Dir? parent)
      {
        Parent = parent;
      }

      public bool IsRoot => Parent == null;
      public Dir? Parent { get; }
      public Dictionary<string, Dir> Directories { get; } = new Dictionary<string, Dir>();
      public Dictionary<string, int> Files { get; } = new Dictionary<string, int>();

      private int? _size;

      public int TotalSize => _size ?? (_size = Directories.Sum(s => s.Value.TotalSize) + Files.Sum(s => s.Value)).Value;

    }
  }
}
