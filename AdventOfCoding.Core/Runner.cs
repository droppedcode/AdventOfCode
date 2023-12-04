using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core
{
  public class Runner
  {
    public void RunDay<D>() where D : Day, new()
    {
      RunTask1<D>();
      RunTask2<D>();
    }

    public void RunTask1<D>() where D : Day, new()
    {
      var day = new D();

      var watch = Stopwatch.StartNew();
      var result = day.Task1();
      watch.Stop();
      Console.WriteLine($"{day.GetType().Name} / Task1: {result} ({watch.ElapsedMilliseconds}ms)");
    }

    public void RunTask2<D>() where D : Day, new()
    {
      var day = new D();

      var watch = Stopwatch.StartNew();
      var result = day.Task2();
      watch.Stop();
      Console.WriteLine($"{day.GetType().Name} / Task2: {result} ({watch.ElapsedMilliseconds}ms)");
    }
  }
}
