using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using AdventOfCoding.Core.Structure;
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
    private static readonly SearchValues<char> _spaceSplit = SearchValues.Create([' ']);

    public override string Task1()
    {
      var seeds = new List<uint>();

      foreach (var seed in ReadLines().First().AsSpan(7).SplitBy(_spaceSplit))
      {
        seeds.Add(seed.Value.ParseFirstUInt());
      }

      return Calculate(seeds, GetMaps(false)).ToString();
    }

    private uint Calculate(IEnumerable<uint> seeds, List<MapRange>[] maps)
    {
      var min = uint.MaxValue;

      foreach (var seed in seeds)
      {
        var id = seed;
        for (var i = 0; i < maps.Length; i++)
        {
          //id = maps[i].GetMappedFilled(id);
          id = maps[i].GetMappedSource(id, id);
        }
        if (id < min)
        {
          min = id;
        }
      }

      return min;
    }

    private uint CalculateReverse(List<MapRange> seeds, List<MapRange>[] maps, uint start, uint step)
    {
      for (uint location = start; location < uint.MaxValue; location += step)
      {
        var id = location;
        for (var i = maps.Length - 1; i >= 0; i--)
        {
          id = maps[i].GetMappedDestination(id, id);
        }
        if (seeds.GetMappedSource(id, uint.MaxValue) != uint.MaxValue)
        {
          return location;
        }
      }

      return uint.MaxValue;
    }

    private List<MapRange>[] GetMaps(bool byDestination)
    {
      var index = 0;
      var maps = new List<MapRange>[] {
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
          maps[index].Sort(byDestination ? MapRangeDestinationComparer.Instance : MapRangeSourceComparer.Instance);
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

          maps[index].Add(new MapRange(src, dest, len));
        }
      }

      maps[index].Sort(byDestination ? MapRangeDestinationComparer.Instance : MapRangeSourceComparer.Instance);

      return maps;
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

    //public override string Task2()
    //{
    //  var seeds = new List<uint>();

    //  foreach (var seed in ReadLines().First().AsSpan(7).SplitBy(_spaceSplit))
    //  {
    //    seeds.Add(seed.Value.ParseFirstUInt());
    //  }

    //  var ranges = new List<uint>[seeds.Count / 2];
    //  for (var i = 0; i < ranges.Length; i++)
    //  {
    //    ranges[i] = seeds.Skip(i * 2).Take(2).ToList();
    //  }

    //  var results = new uint[ranges.Length];
    //  var maps = GetMaps(false);

    //  Parallel.ForEach(ranges, new ParallelOptions() { MaxDegreeOfParallelism = 32 }, (s, p, i) =>
    //  {
    //    results[i] = Calculate(new SeedEnumerator(s), maps);
    //  });

    //  return results.Min().ToString();
    //}

    /// <summary>
    /// This solution looks from the location side for seeds that are present in more granular steps.
    /// </summary>
    public override string Task2()
    {
      var seeds = new List<uint>();

      foreach (var seed in ReadLines().First().AsSpan(7).SplitBy(_spaceSplit))
      {
        seeds.Add(seed.Value.ParseFirstUInt());
      }

      var ranges = new List<MapRange>();
      for (var i = 0; i < seeds.Count; i += 2)
      {
        ranges.Add(new MapRange(seeds[i], seeds[i], seeds[i + 1]));
      }
      ranges.Sort(MapRangeSourceComparer.Instance);

      var maps = GetMaps(true);

      // Solution 1, working solution going up 1 at a time
      // return CalculateReverse(ranges, maps, 0, 1, false).ToString();

      // Solution 2, start with large steps to determine a possible solution and backtrack from that to fine it down
      // Too large starting number can give false result and no real speed benefit
      //var step = uint.MaxValue / 16; // << first valid
      var step = 100_000u;

      var limit = CalculateReverse(ranges, maps, 0, step);

      while (step / 2 > 0)
      {
        limit = CalculateReverse(ranges, maps, limit - step, step /= 2);
      }

      return limit.ToString();

      // Solution 3, automatically tries to determine the step based on trying multiple values until gets the same result
      //var lastResult = uint.MaxValue;

      //for (var retry = 1; retry < 10; retry++)
      //{
      //  var step = uint.MaxValue / (uint)Math.Pow(4, retry);

      //  var limit = CalculateReverse(ranges, maps, 0, step);

      //  while (step / 2 > 0)
      //  {
      //    limit = CalculateReverse(ranges, maps, limit - step, step /= 2);
      //  }

      //  if (lastResult == limit)
      //  {
      //    return limit.ToString() + ", try: " + retry;
      //  }
      //  else
      //  {
      //    lastResult = limit;
      //  }
      //}

      //return "Error";
    }
  }
}
