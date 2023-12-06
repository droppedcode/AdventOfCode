using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Structure;

/// <summary>
/// Source and destination range, where source is mapped to destination.
/// </summary>
/// <param name="destinationStart"></param>
/// <param name="sourceStart"></param>
/// <param name="length"></param>
public struct MapRange(uint sourceStart, uint destinationStart, uint length)
{
  public uint SourceStart = sourceStart;
  public uint SourceEnd = sourceStart + length - 1;

  public uint DestinationStart = destinationStart;
  public uint DestinatinEnd = destinationStart + length - 1;

  public static readonly MapRange Empty;

  public override string ToString()
  {
    return $"{SourceStart}-{SourceEnd} => {DestinationStart}-{DestinatinEnd} # {length}";
  }
}

public class MapRangeSourceComparer : IComparer<MapRange>
{
  public static readonly MapRangeSourceComparer Instance = new();

  public int Compare(MapRange x, MapRange y)
  {
    if (x.SourceStart < y.SourceStart) return -1;
    if (x.SourceStart > y.SourceStart) return 1;
    return 0;
  }
}

public class MapRangeDestinationComparer : IComparer<MapRange>
{
  public static readonly MapRangeDestinationComparer Instance = new();

  public int Compare(MapRange x, MapRange y)
  {
    if (x.DestinationStart < y.DestinationStart) return -1;
    if (x.DestinationStart > y.DestinationStart) return 1;
    return 0;
  }
}

public class MapRangeSourceFinderComparer(uint value) : IComparer<MapRange>
{
  public int Compare(MapRange x, MapRange y)
  {
    var range = x.SourceStart == x.SourceEnd ? y : x;

    if (range.SourceStart > value) return 1;
    if (range.SourceEnd < value) return -1;
    return 0;
  }
}

public class MapRangeDestinationFinderComparer(uint value) : IComparer<MapRange>
{
  public int Compare(MapRange x, MapRange y)
  {
    var range = x.SourceStart == x.SourceEnd ? y : x;

    if (range.DestinationStart > value) return 1;
    if (range.DestinatinEnd < value) return -1;
    return 0;
  }
}

public static class MapRangeExtensions
{
  /// <summary>
  /// Returns the range from a sorted list where the given value is present.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Value to search for.</param>
  /// <returns>The range or null if not found.</returns>
  public static MapRange? GetRangeSourceBinary(this List<MapRange> ranges, uint value)
  {
    var index = ranges.BinarySearch(0, ranges.Count, MapRange.Empty, new MapRangeSourceFinderComparer(value));
    return index >= 0 ? ranges[index] : null;
  }

  /// <summary>
  /// Returns the range from a sorted list where the given value is present.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Value to search for.</param>
  /// <returns>The range.</returns>
  public static MapRange GetRangeBinarySourceFilled(this List<MapRange> ranges, uint value)
  {
    var index = ranges.BinarySearch(0, ranges.Count, MapRange.Empty, new MapRangeSourceFinderComparer(value));
    return index >= 0 ? ranges[index] : throw new InvalidOperationException("Value is missing from the ranges.");
  }

  /// <summary>
  /// Get the mapped value.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Source value to map to destination value.</param>
  /// <param name="defaultValue">Value to return if source was not found.</param>
  /// <returns>The destination value or default.</returns>
  public static uint GetMappedSource(this List<MapRange> ranges, uint value, uint defaultValue)
  {
    var range = ranges.GetRangeSourceBinary(value);
    return range.HasValue ? range.Value.DestinationStart + (value - range.Value.SourceStart) : defaultValue;
  }

  /// <summary>
  /// Get the mapped value.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Source value to map to destination value.</param>
  /// <returns>The destination value or default.</returns>
  public static uint GetMappedSourceFilled(this List<MapRange> ranges, uint value)
  {
    var range = ranges.GetRangeBinarySourceFilled(value);
    return range.DestinationStart + (value - range.SourceStart);
  }

  /// <summary>
  /// Returns the range from a sorted list where the given value is present.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Value to search for.</param>
  /// <returns>The range or null if not found.</returns>
  public static MapRange? GetRangeDestinationBinary(this List<MapRange> ranges, uint value)
  {
    var index = ranges.BinarySearch(0, ranges.Count, MapRange.Empty, new MapRangeDestinationFinderComparer(value));
    return index >= 0 ? ranges[index] : null;
  }

  /// <summary>
  /// Returns the range from a sorted list where the given value is present.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Value to search for.</param>
  /// <returns>The range.</returns>
  public static MapRange GetRangeBinaryDestinationFilled(this List<MapRange> ranges, uint value)
  {
    var index = ranges.BinarySearch(0, ranges.Count, MapRange.Empty, new MapRangeDestinationFinderComparer(value));
    return index >= 0 ? ranges[index] : throw new InvalidOperationException("Value is missing from the ranges.");
  }

  /// <summary>
  /// Get the mapped value.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Destination value to map to destination value.</param>
  /// <param name="defaultValue">Value to return if destination was not found.</param>
  /// <returns>The destination value or default.</returns>
  public static uint GetMappedDestination(this List<MapRange> ranges, uint value, uint defaultValue)
  {
    var range = ranges.GetRangeDestinationBinary(value);
    return range.HasValue ? range.Value.SourceStart + (value - range.Value.DestinationStart) : defaultValue;
  }

  /// <summary>
  /// Get the mapped value.
  /// </summary>
  /// <param name="ranges">Sorted ranges to search.</param>
  /// <param name="value">Destination value to map to destination value.</param>
  /// <returns>The destination value or default.</returns>
  public static uint GetMappedDestinationFilled(this List<MapRange> ranges, uint value)
  {
    var range = ranges.GetRangeBinaryDestinationFilled(value);
    return range.SourceStart + (value - range.DestinationStart);
  }

  /// <summary>
  /// Fill the missing ranges in the list.
  /// </summary>
  /// <param name="ranges">Sorted ranges to fill.</param>
  public static void FillHoles(this List<MapRange> ranges, uint maxValue)
  {
    if (ranges.Count == 0)
    {
      ranges.Add(new MapRange(0, 0, maxValue));
      return;
    }
    else if (ranges[0].SourceStart != 0)
    {
      ranges.Insert(0, new MapRange(0, 0, ranges[0].SourceStart));
    }

    for (var i = 0; i < ranges.Count - 1; i++)
    {
      if (ranges[i].SourceEnd + 1 != ranges[i + 1].SourceStart)
      {
        ranges.Insert(i + 1, new MapRange(ranges[i].SourceEnd + 1, ranges[i].SourceEnd + 1, ranges[i + 1].SourceStart - ranges[i].SourceEnd - 1));
      }
    }

    var end = ranges[^1].SourceEnd;
    if (end < maxValue)
    {
      ranges.Add(new MapRange(end, end, maxValue - end));
    }
  }

}