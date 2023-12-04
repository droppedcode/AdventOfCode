using AdventOfCoding.Core.Helpers;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceProfiling
{
  [MemoryDiagnoser]
  public class Permutations
  {
    private byte[] data;

    [Params(3, 5, 10)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
      data = new byte[N];
      new Random(42).NextBytes(data);
    }

    [Benchmark]
    public void Permute() => Combinatorics.Permute(data).Count();

  }
}
