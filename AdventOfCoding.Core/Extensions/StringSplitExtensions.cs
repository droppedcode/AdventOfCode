using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Extensions;

public static class StringSplitExtensions
{
  public static LineSplitEnumerator SplitLines(this ReadOnlySpan<char> str)
  {
    // LineSplitEnumerator is a struct so there is no allocation here
    return new LineSplitEnumerator(str);
  }

  public static LineSplitEnumerator SplitLines(this string str)
  {
    // LineSplitEnumerator is a struct so there is no allocation here
    return new LineSplitEnumerator(str.AsSpan());
  }

  public static SplitEnumerator SplitBy(this ReadOnlySpan<char> str, SearchValues<char> searchValues)
  {
    // LineSplitEnumerator is a struct so there is no allocation here
    return new SplitEnumerator(str, searchValues);
  }

  public static SplitEnumerator SplitBy(this string str, SearchValues<char> searchValues)
  {
    // LineSplitEnumerator is a struct so there is no allocation here
    return new SplitEnumerator(str.AsSpan(), searchValues);
  }

  // Must be a ref struct as it contains a ReadOnlySpan<char>
  public ref struct SplitEnumerator
  {
    private ReadOnlySpan<char> _str;
    private readonly SearchValues<char> _searchValues;

    public SplitEnumerator(ReadOnlySpan<char> str, SearchValues<char> searchValues)
    {
      _str = str;
      _searchValues = searchValues;
      Current = default;
    }

    // Needed to be compatible with the foreach operator
    public SplitEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
      var span = _str;
      if (span.Length == 0) // Reach the end of the string
        return false;

      var index = span.IndexOfAny(_searchValues);
      if (index == -1) // The string is composed of only one line
      {
        _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
        Current = new SplitEntry(span, ReadOnlySpan<char>.Empty);
        return true;
      }

      Current = new SplitEntry(span.Slice(0, index), span.Slice(index, 1));
      _str = span.Slice(index + 1);
      return true;
    }

    public SplitEntry Current { get; private set; }
  }

  // Must be a ref struct as it contains a ReadOnlySpan<char>
  public ref struct LineSplitEnumerator
  {
    private ReadOnlySpan<char> _str;

    public LineSplitEnumerator(ReadOnlySpan<char> str)
    {
      _str = str;
      Current = default;
    }

    // Needed to be compatible with the foreach operator
    public LineSplitEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
      var span = _str;
      if (span.Length == 0) // Reach the end of the string
        return false;

      var index = span.IndexOfAny('\r', '\n');
      if (index == -1) // The string is composed of only one line
      {
        _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
        Current = new SplitEntry(span, ReadOnlySpan<char>.Empty);
        return true;
      }

      if (index < span.Length - 1 && span[index] == '\r')
      {
        // Try to consume the '\n' associated to the '\r'
        var next = span[index + 1];
        if (next == '\n')
        {
          Current = new SplitEntry(span.Slice(0, index), span.Slice(index, 2));
          _str = span.Slice(index + 2);
          return true;
        }
      }

      Current = new SplitEntry(span.Slice(0, index), span.Slice(index, 1));
      _str = span.Slice(index + 1);
      return true;
    }

    public SplitEntry Current { get; private set; }
  }

  public readonly ref struct SplitEntry
  {
    public SplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
    {
      Value = line;
      Separator = separator;
    }

    public ReadOnlySpan<char> Value { get; }
    public ReadOnlySpan<char> Separator { get; }

    // This method allow to deconstruct the type, so you can write any of the following code
    // foreach (var entry in str.SplitLines()) { _ = entry.Line; }
    // foreach (var (line, endOfLine) in str.SplitLines()) { _ = line; }
    // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct?WT.mc_id=DT-MVP-5003978#deconstructing-user-defined-types
    public void Deconstruct(out ReadOnlySpan<char> line, out ReadOnlySpan<char> separator)
    {
      line = Value;
      separator = Separator;
    }

    // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
    // foreach (ReadOnlySpan<char> entry in str.SplitLines())
    public static implicit operator ReadOnlySpan<char>(SplitEntry entry) => entry.Value;
  }
}
