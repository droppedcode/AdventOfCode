using System.Security.Cryptography;
using System.Text;

namespace AdventOfCoding.Core
{
  public abstract class Day
  {
    public abstract string Task1();
    public abstract string Task2();

    /// <summary>
    /// Get words from a text separated by a separator character.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>Enumerable <see cref="ReadOnlyMemory{T}"/>.</returns>
    protected static IEnumerable<ReadOnlyMemory<char>> GetWords(string text, char separator = ' ')
    {
      var start = 0;
      var length = 0;
      var memory = text.AsMemory();
      for (int i = 0; i < text.Length; i++)
      {
        if (text[i] == separator)
        {
          yield return memory.Slice(start, length);
          start = i + 1;
          length = 0;
        }
        length++;
      }

      if (length > 0)
      {
        yield return memory.Slice(start, length);
      }
    }

    /// <summary>
    /// Get the a word from a text from a starting index.
    /// </summary>
    /// <param name="text">Text to use.</param>
    /// <param name="start">Start index.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The word.</returns>
    protected static ReadOnlySpan<char> GetWord(string text, int start = 0, char separator = ' ')
    {
      for (int i = start; i < text.Length; i++)
      {
        if (text[i] == separator)
        {
          return text.AsSpan(start, i - start);
        }
      }

      return text.AsSpan(start);
    }

    /// <summary>
    /// Get integers from a text separated by a character.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger values.</returns>
    protected static IEnumerable<int> GetInts(string text, char separator = ' ', int start = 0)
    {
      var value = 0;

      for (int i = start; i < text.Length; i++)
      {
        var c = text[i];
        if (c == separator)
        {
          yield return value;
          value = 0;
        }
        else
        {
          value = value * 10 + (c - '0');
        }
      }

      yield return value;
    }

    /// <summary>
    /// Get integers from a text separated by any of 2 characters.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator1">Separator character.</param>
    /// <param name="separator2">Separator character.</param>
    /// <returns>The interger values.</returns>
    protected static IEnumerable<int> GetInts(string text, char separator1 = ' ', char separator2 = ' ', int start = 0)
    {
      var value = 0;
      var hasValue = false;

      for (int i = start; i < text.Length; i++)
      {
        var c = text[i];
        if (c == separator1 || c == separator2)
        {
          if (hasValue)
          {
            yield return value;
          }
          hasValue = false;
          value = 0;
        }
        else
        {
          hasValue = true;
          value = value * 10 + (c - '0');
        }
      }

      if (hasValue)
      {
        yield return value;
      }
    }

    /// <summary>
    /// Get a tuple of int pair from a text.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger value tuple.</returns>
    protected static (int, int) Get2Ints(string text, char separator = ' ', int start = 0)
    {
      var it = GetInts(text, separator, start).GetEnumerator();
      it.MoveNext();
      var a = it.Current;
      it.MoveNext();
      var b = it.Current;
      return (a, b);
    }

    /// <summary>
    /// Get a tuple of int trio from a text.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger value tuple.</returns>
    protected static (int, int, int) Get3Ints(string text, char separator = ' ', int start = 0)
    {
      var it = GetInts(text, separator, start).GetEnumerator();
      it.MoveNext();
      var a = it.Current;
      it.MoveNext();
      var b = it.Current;
      it.MoveNext();
      var c = it.Current;
      return (a, b, c);
    }

    /// <summary>
    /// Get the an integer from a text from a starting index.
    /// </summary>
    /// <param name="text">Text to use.</param>
    /// <param name="start">Start index.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The integer.</returns>
    protected static int GetInt(ReadOnlySpan<char> text, int start = 0, char separator = ' ')
    {
      return GetInt(text, out _, start, separator);
    }

    /// <summary>
    /// Get the an integer from a text from a starting index.
    /// </summary>
    /// <param name="text">Text to use.</param>
    /// <param name="start">Start index.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The integer.</returns>
    protected static int GetInt(ReadOnlySpan<char> text, out int next, int start = 0, char separator = ' ')
    {
      var value = 0;
      var isMinus = false;

      if (text[start] == '-')
      {
        isMinus = true;
        start++;
      }

      for (int i = start; i < text.Length; i++)
      {
        var c = text[i];
        if (c == separator)
        {
          next = i + 1;
          return isMinus ? -value : value;
        }
        else
        {
          value = value * 10 + (c - '0');
        }
      }

      next = text.Length;
      return isMinus ? -value : value;
    }

    /// <summary>
    /// Get the an integer from a text from a starting index.
    /// </summary>
    /// <param name="text">Text to use.</param>
    /// <param name="start">Start index.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The integer.</returns>
    protected static int GetInt(ReadOnlySpan<char> text, ref int index, char separator1, char separator2)
    {
      var value = 0;
      var isMinus = false;

      if (text[index] == '-')
      {
        isMinus = true;
        index++;
      }

      for (; index < text.Length; index++)
      {
        var c = text[index];
        if (c == separator1 || c == separator2)
        {
          index++;
          return isMinus ? -value : value;
        }
        else
        {
          value = value * 10 + (c - '0');
        }
      }

      return isMinus ? -value : value;
    }

    /// <summary>
    /// Get integers from a text separated by a character.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger values.</returns>
    protected static IEnumerable<int> GetInts(ReadOnlySpan<char> text, char separator = ' ')
    {
      var list = new List<int>();
      var value = 0;

      for (int i = 0; i < text.Length; i++)
      {
        var c = text[i];
        if (c == separator)
        {
          list.Add(value);
          value = 0;
        }
        else
        {
          value = value * 10 + (c - '0');
        }
      }

      list.Add(value);

      return list;
    }

    /// <summary>
    /// Get a tuple of int pair from a text.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger value tuple.</returns>
    protected static (int, int) Get2Ints(ReadOnlySpan<char> text, char separator = ' ')
    {
      var it = GetInts(text, separator).GetEnumerator();
      it.MoveNext();
      var a = it.Current;
      it.MoveNext();
      var b = it.Current;
      return (a, b);
    }

    /// <summary>
    /// Get a tuple of int trio from a text.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger value tuple.</returns>
    protected static (int, int, int) Get3Ints(ReadOnlySpan<char> text, char separator = ' ')
    {
      var it = GetInts(text, separator).GetEnumerator();
      it.MoveNext();
      var a = it.Current;
      it.MoveNext();
      var b = it.Current;
      it.MoveNext();
      var c = it.Current;
      return (a, b, c);
    }

    /// <summary>
    /// Get integers from a text separated by a character.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger values.</returns>
    protected static IEnumerable<int> GetInts(ReadOnlyMemory<char> text, char separator = ' ')
    {
      var value = 0;

      for (int i = 0; i < text.Length; i++)
      {
        var c = text.Span[i];
        if (c == separator)
        {
          yield return value;
          value = 0;
        }
        else
        {
          value = value * 10 + (c - '0');
        }
      }

      yield return value;
    }

    /// <summary>
    /// Get a tuple of int pair from a text.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger value tuple.</returns>
    protected static (int, int) Get2Ints(ReadOnlyMemory<char> text, char separator = ' ')
    {
      var it = GetInts(text, separator).GetEnumerator();
      it.MoveNext();
      var a = it.Current;
      it.MoveNext();
      var b = it.Current;
      return (a, b);
    }

    /// <summary>
    /// Get a tuple of int trio from a text.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The interger value tuple.</returns>
    protected static (int, int, int) Get3Ints(ReadOnlyMemory<char> text, char separator = ' ')
    {
      var it = GetInts(text, separator).GetEnumerator();
      it.MoveNext();
      var a = it.Current;
      it.MoveNext();
      var b = it.Current;
      it.MoveNext();
      var c = it.Current;
      return (a, b, c);
    }

    /// <summary>
    /// Get the an integer from a text from a starting index.
    /// </summary>
    /// <param name="text">Text to use.</param>
    /// <param name="start">Start index.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The integer.</returns>
    protected static int GetInt(string text, int start = 0, char separator = ' ')
    {
      return GetInt(text, out _, start, separator);
    }

    /// <summary>
    /// Get the an integer from a text from a starting index.
    /// </summary>
    /// <param name="text">Text to use.</param>
    /// <param name="start">Start index.</param>
    /// <param name="separator">Separator character.</param>
    /// <returns>The integer.</returns>
    protected static int GetInt(string text, out int next, int start = 0, char separator = ' ')
    {
      var value = 0;
      var isMinus = false;

      if (text[start] == '-')
      {
        isMinus = true;
        start++;
      }

      for (int i = start; i < text.Length; i++)
      {
        var c = text[i];
        if (c == separator)
        {
          next = i + 1;
          return isMinus ? -value : value;
        }
        else
        {
          value = value * 10 + (c - '0');
        }
      }

      next = text.Length;
      return isMinus ? -value : value;
    }

    /// <summary>
    /// Calculate the MD5 hash of a generated input which gets an increasing integer until the hash hexadecimal reperesentation starts with given number of zeros.
    /// </summary>
    /// <param name="inputGenerator">Input generator function.</param>
    /// <param name="numberOfZeroes">Number of zeroes at the start of the hash.</param>
    /// <param name="startingNumber">Number to start with.</param>
    /// <returns>Tuple of hash and the number.</returns>
    protected static (byte[] hash, int number) GetMD5StartingWithZeroes(Func<int, string> inputGenerator, int numberOfZeroes, int startingNumber = 0)
    {
      static bool StartsWith(byte[] hash, int numberOfZeroes)
      {
        for (var i = 0; i < numberOfZeroes / 2; i++)
        {
          if (hash[i] != 0) return false;
        }

        return (numberOfZeroes % 2 == 0 || hash[numberOfZeroes / 2] < 16);
      }

      var index = startingNumber;

      var md5 = MD5.Create();

      while (true)
      {
        var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(inputGenerator(index)));

        if (StartsWith(hash, numberOfZeroes))
        {
          return (hash, index);
        }

        index++;
      }
    }

  }
}