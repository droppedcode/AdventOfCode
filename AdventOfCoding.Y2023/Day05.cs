using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2023
{
  internal class Day05 : FileDay
  {
    private static SearchValues<char> _spaceSplit = SearchValues.Create([' ']);


    private struct Range(uint destinationStart, uint sourceStart, uint length)
    {
      public uint SourceStart = sourceStart;
      /// <summary>
      /// Exclusive
      /// </summary>
      public uint SourceEnd = sourceStart + length;

      public uint DestinationStart = destinationStart;
      /// <summary>
      /// Exclusive
      /// </summary>
      public uint DestinatinEnd = destinationStart + length;

      public static readonly Range Empty;
    }

    private class RangeComparer : IComparer<Range>
    {
      public static readonly RangeComparer Instance = new();

      public int Compare(Range x, Range y)
      {
        if (x.SourceStart < y.SourceStart) return -1;
        if (x.SourceStart > y.SourceStart) return 1;
        return 0;
      }
    }

    private class RangeFinderComparer(uint value) : IComparer<Range>
    {
      public int Compare(Range x, Range y)
      {
        var range = x.SourceStart == x.SourceEnd ? y : x;

        if (range.SourceStart > value) return 1;
        if (range.SourceEnd <= value) return -1;
        return 0;
      }
    }

    private static Range? GetRange(List<Range> ranges, uint value)
    {
      var index = ranges.BinarySearch(0, ranges.Count, Range.Empty, new RangeFinderComparer(value));
      return index >= 0 ? ranges[index] : null;
    }

    private static uint GetMapped(List<Range> ranges, uint value)
    {
      var range = GetRange(ranges, value);
      return range.HasValue ? range.Value.DestinationStart + (value - range.Value.SourceStart) : value;
    }

    public override string Task1()
    {
      var seeds = new List<uint>();

      foreach (var seed in ReadLines().First().AsSpan(7).SplitBy(_spaceSplit))
      {
        seeds.Add(seed.Value.ParseFirstUInt());
      }

      return Calculate(seeds).ToString();
    }

    private uint Calculate(IEnumerable<uint> seeds)
    {
      var index = 0;
      var maps = new List<Range>[] {
        [],
        [],
        [],
        [],
        [],
        [],
        [],
      };

      foreach (var line in ReadLines().Skip(2))
      {
        if (line == "")
        {
          maps[index].Sort(RangeComparer.Instance);
          index++;
        }
        else if (!line[0].IsDigitLatin())
        {
          continue;
        }
        else
        {
          var parts = line.SplitBy(_spaceSplit);
          parts.MoveNext();
          var dest = parts.Current.Value.ParseFirstUInt();
          parts.MoveNext();
          var src = parts.Current.Value.ParseFirstUInt();
          parts.MoveNext();
          var len = parts.Current.Value.ParseFirstUInt();

          maps[index].Add(new Range(dest, src, len));
        }
      }

      maps[index].Sort(RangeComparer.Instance);

      var min = uint.MaxValue;

      foreach (var seed in seeds)
      {
        var id = seed;
        for (var i = 0; i < maps.Length; i++)
        {
          id = GetMapped(maps[i], id);
        }
        if (id < min)
        {
          min = id;
        }
      }

      return min;
    }

    private class SeedEnumerator(List<uint> ranges) : IEnumerable<uint>
    {
      private readonly List<uint> _ranges = ranges;

      public IEnumerator<uint> GetEnumerator()
      {
        for (var i = 0; i < _ranges.Count; i += 2)
        {
          for (uint j = 0; j < _ranges[i + 1]; j++)
          {
            yield return _ranges[i] + j;
          }
        }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return GetEnumerator();
      }
    }

    public override string Task2()
    {
      var seeds = new List<uint>();

      foreach (var seed in ReadLines().First().AsSpan(7).SplitBy(_spaceSplit))
      {
        seeds.Add(seed.Value.ParseFirstUInt());
      }

      var ranges = new List<uint>[seeds.Count / 2];
      for (var i = 0; i < ranges.Length; i++)
      {
        ranges[i] = seeds.Skip(i * 2).Take(2).ToList();
      }

      var results = new uint[ranges.Length];

      Parallel.ForEach(ranges, new ParallelOptions() { MaxDegreeOfParallelism = 32 }, (s, p, i) =>
      {
        results[i] = Calculate(new SeedEnumerator(s));
      });

      return results.Min().ToString();
    }
  }
}
