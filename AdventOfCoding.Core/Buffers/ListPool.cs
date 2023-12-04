using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Buffers
{
  public class ListPool<T>
  {
    private readonly Stack<List<T>> _list = new Stack<List<T>>();

    public List<T> Rent(int minSize = 0)
    {
      if (_list.Count > 0)
      {
        var list = _list.Pop();

        if (list.Capacity < minSize)
        {
          list.Capacity = minSize;
        }

        return list;
      }
      else
      {
        return new List<T>(minSize);
      }
    }

    public void Return(List<T> list, bool clear = false)
    {
      if (clear)
      {
        list.Clear();
      }

      _list.Push(list);
    }

  }
}
