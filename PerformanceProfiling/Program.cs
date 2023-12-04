// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using PerformanceProfiling;

BenchmarkSwitcher.FromTypes(new[] { typeof(Permutations) }).RunAll();