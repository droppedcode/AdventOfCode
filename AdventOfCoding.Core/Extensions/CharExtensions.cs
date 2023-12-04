using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Extensions;

public static class CharExtensions
{
  public static bool IsDigitLatin(this char c)
  {
    return c >= '0' && c <= '9';
  }
}
