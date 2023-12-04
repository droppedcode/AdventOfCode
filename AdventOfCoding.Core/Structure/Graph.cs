using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCoding.Core.Extensions;
using AdventOfCoding.Core.Helpers;

namespace AdventOfCoding.Core.Structure
{
  public class Graph
  {
    public List<string> Nodes { get; } = new();
    private readonly List<List<int>> _edges = new();
    private readonly List<Dictionary<int, int>> _edgeValues = new();
    private readonly bool _twoWay;
    private readonly bool _weightless;

    public Graph(bool twoWay = false, bool weightless = true)
    {
      _twoWay = twoWay;
      _weightless = weightless;
    }

    public void AddEdge(string from, string to, int value = 1)
    {
      var n0 = Nodes.GetIndexOrAdd(from);
      var n1 = Nodes.GetIndexOrAdd(to);

      _edges.EnsureCount(n0 + 1, () => new List<int>());
      _edges[n0].Add(n1);

      if (!_weightless)
      {
        _edgeValues.EnsureCount(n0 + 1, () => new Dictionary<int, int>());
        _edgeValues[n0][n1] = value;
      }

      if (_twoWay)
      {
        _edges.EnsureCount(n1 + 1, () => new List<int>());
        _edges[n1].Add(n0);

        if (!_weightless)
        {
          _edgeValues.EnsureCount(n1 + 1, () => new Dictionary<int, int>());
          _edgeValues[n1][n0] = value;
        }
      }
    }

    private (IEnumerable<string> route, int totalValue) ToNodeRoute((IEnumerable<int> route, int totalValue) value) => (value.route.Select(s => Nodes[s]), value.totalValue);

    /// <summary>
    /// Get all routes in the graph only visiting each node once.
    /// </summary>
    /// <returns>The nodes and the total value of the route.</returns>
    public IEnumerable<(IEnumerable<string> route, int totalValue)> GetRoutesVisitOnlyOnce()
    {
      var route = new List<int>(Nodes.Count);
      var added = new bool[Nodes.Count];
      foreach (var r in BuildRouteVisitOnlyOnce(route, added))
      {
        yield return ToNodeRoute(r);
      }
    }

    private IEnumerable<(List<int> route, int totalValue)> BuildRouteVisitOnlyOnce(List<int> route, bool[] added)
    {
      for (int i = 0; i < Nodes.Count; i++)
      {
        route[0] = i;
        foreach (var r in BuildRouteVisitOnlyOnce(route, i, 1, 0, added))
        {
          yield return r;
        }
      }
    }

    private IEnumerable<(List<int> route, int totalValue)> BuildRouteVisitOnlyOnce(List<int> route, int currentNode, int index, int totalValue, bool[] added)
    {
      if (index == Nodes.Count)
      {
        yield return (route, totalValue);
        yield break;
      }

      var isEnd = true;

      for (int i = 0; i < Nodes.Count; i++)
      {
        if (added[i]) continue;
        if (!_edges[currentNode].Contains(i)) continue;

        isEnd = false;

        added[i] = true;
        route.Add(i);
        var value = GetValue(currentNode, i);
        foreach (var r in BuildRouteVisitOnlyOnce(route, i, index + 1, totalValue + value, added))
        {
          yield return r;
        }
        route.RemoveAt(index);
        added[i] = false;
      }

      if (isEnd) yield return (route, totalValue);
    }

    private int GetValue(int from, int to)
    {
      return _weightless ? 1 : _edges[from][to];
    }

    private IEnumerable<(List<int> route, int totalValue)> BuildRouteVisitOnlyOnceTo(List<int> route, int currentNode, int index, int totalValue, bool[] added, int endIndex)
    {
      if (currentNode == endIndex || index == Nodes.Count)
      {
        yield return (route, totalValue);
        yield break;
      }

      var isEnd = true;

      for (int i = 0; i < Nodes.Count; i++)
      {
        if (added[i]) continue;
        if (!_edges[currentNode].Contains(i)) continue;

        isEnd = false;

        added[i] = true;
        route.Add(i);
        var value = GetValue(currentNode, i);
        foreach (var r in BuildRouteVisitOnlyOnceTo(route, i, index + 1, totalValue + value, added, endIndex))
        {
          yield return r;
        }
        route.RemoveAt(index);
        added[i] = false;
      }

      if (isEnd) yield return (route, totalValue);
    }

    /// <summary>
    /// Get all routes in the graph only visiting each node once.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    public IEnumerable<(IEnumerable<string> route, int totalValue)> GetRoutesVisitOnlyOnceFrom(string start)
    {
      foreach (var r in GetRoutesVisitOnlyOnceFromDeep(Nodes.IndexOf(start)))
      {
        yield return ToNodeRoute(r);
      }
    }

    private IEnumerable<(List<int> route, int totalValue)> GetRoutesVisitOnlyOnceFromDeep(int startIndex)
    {
      var route = new List<int>(Nodes.Count)
      {
        startIndex
      };
      var added = new bool[Nodes.Count];
      return BuildRouteVisitOnlyOnce(route, startIndex, 1, 0, added);
    }

    /// <summary>
    /// Get all routes in the graph only visiting each node once.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="end">The end node.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    public (IEnumerable<string> route, int totalValue)? GetShortestRouteFromToDeep(string start, string end)
    {
      var min = int.MaxValue;
      IEnumerable<int> best = null;

      foreach (var route in GetRoutesFromToDeep(Nodes.IndexOf(start), Nodes.IndexOf(end)))
      {
        if (route.totalValue < min)
        {
          min = route.totalValue;
          best = route.route;
        }
      }

      return best != null ? ToNodeRoute((best, min)) : null;
    }

    /// <summary>
    /// Get all routes in the graph only visiting each node once.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="end">The end node.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    public IEnumerable<(IEnumerable<string> route, int totalValue)> GetRoutesFromToWide(string start, string end)
    {
      var startIndex = Nodes.IndexOf(start);
      var endIndex = Nodes.IndexOf(end);

      var currentNodes = new List<(int, int[], bool[], int)>(_edges[startIndex].Select(s => (s, new int[] { startIndex }, new bool[Nodes.Count], GetValue(startIndex, s))));
      foreach (var node in currentNodes)
      {
        node.Item3[startIndex] = true;
      }
      var nextNodes = new List<(int, int[], bool[], int)>();

      while (currentNodes.Count > 0)
      {
        foreach (var (node, route, visited, totalValue) in currentNodes)
        {
          if (node == endIndex)
          {
            yield return ToNodeRoute((route.Append(node), totalValue));
          }
          else
          {
            var visitedNext = visited.Copy();
            visitedNext[node] = true;
            var routeNext = route.Append(node);

            nextNodes.AddRange(_edges[node].Where(w => !visitedNext[w]).Select(s => (s, routeNext, visitedNext, totalValue + GetValue(node, s))));
          }
        }

        (currentNodes, nextNodes) = (nextNodes, currentNodes);
        nextNodes.Clear();
      }
    }

    /// <summary>
    /// Get all routes in the graph only visiting each node once.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="end">The end node.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    private IEnumerable<(List<int> route, int totalValue)> GetRoutesFromToDeep(int startIndex, int endIndex)
    {
      var route = new List<int>(Nodes.Count)
      {
        startIndex
      };
      var added = new bool[Nodes.Count];

      return BuildRouteVisitOnlyOnceTo(route, startIndex, 1, 0, added, endIndex);
    }

    /// <summary>
    /// Get all routes in the graph until the total value does not exceed the limit.
    /// </summary>
    /// <param name="limit">The limit for the total value.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    public IEnumerable<(IEnumerable<string> route, int totalValue)> GetRoutes(int limit)
    {
      foreach (var node in Nodes)
      {
        foreach (var route in GetRoutesFrom(node, limit))
        {
          yield return route;
        }
      }
    }

    /// <summary>
    /// Get all routes in the graph until the total value does not exceed the limit.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="limit">The limit for the total value.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    public IEnumerable<(IEnumerable<string> route, int totalValue)> GetRoutesFrom(string start, int limit)
    {
      var startIndex = Nodes.IndexOf(start);

      return GetRoutesFrom(startIndex, limit, 0).Select(ToNodeRoute);
    }

    /// <summary>
    /// Get all routes in the graph until the total value does not exceed the limit.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="limit">The limit for the total value.</param>
    /// <returns>The nodes and the total value of the route.</returns>
    private IEnumerable<(IEnumerable<int> route, int totalValue)> GetRoutesFrom(int startIndex, int limit, int totalValue)
    {
      foreach (var next in _edges[startIndex])
      {
        var value = GetValue(startIndex, next);

        if (value == limit)
        {
          yield return (new[] { next }, totalValue + value);
        }
        else if (value < limit)
        {
          var hasAnySub = false;

          foreach (var sub in GetRoutesFrom(next, limit - value, totalValue + value))
          {
            hasAnySub = true;
            yield return (sub.route.Prepend(next), sub.totalValue);
          }

          if (!hasAnySub)
          {
            yield return (new[] { next }, totalValue + value);
          }
        }
      }
    }

  }
}
