using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core
{
  public abstract class FileDay : Day
  {
    protected string FileName { get; set; }

    public FileDay()
    {
      FileName = GetType().Name + ".txt";
    }

    protected string ReadFile() => File.ReadAllText(FileName);

    protected IEnumerable<string> ReadLines() => File.ReadLines(FileName);

  }
}
