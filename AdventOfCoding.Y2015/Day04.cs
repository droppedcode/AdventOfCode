using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day04 : StringDay
  {
    protected override string Input => "ckczppom";

    public override string Task1()
    {
      return GetMD5StartingWithZeroes(i => Input + i, 5).number.ToString();
    }

    public override string Task2()
    {
      return GetMD5StartingWithZeroes(i => Input + i, 6).number.ToString();
    }
  }
}
