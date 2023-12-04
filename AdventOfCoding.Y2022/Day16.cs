using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using AdventOfCoding.Core.Helpers;
using AdventOfCoding.Core.Structure;

namespace AdventOfCoding.Y2022
{
  internal class Day16 : FileDay
  {
    public override string Task1()
    {
      var graph = new Graph(twoWay: false, weightless: true);
      var valveFlows = new Dictionary<int, int>();

      foreach (var line in ReadLines())
      {
        var node = line[6..8];
        var flow = GetInt(line, out var index, 23, ';');

        if (line[index + 22] == ' ')
        {
          index--;
        }

        for (var i = index + 24; i < line.Length; i += 4)
        {
          graph.AddEdge(node, line[i..(i + 2)]);
        }

        valveFlows.Add(graph.Nodes.IndexOf(node), flow);
      }

      var valveStates = new bool[graph.Nodes.Count];

      foreach (var flow in valveFlows)
      {
        if (flow.Value == 0)
        {
          valveStates[flow.Key] = true;
        }
      }

      var routes = new int[valveFlows.Count, valveFlows.Count];

      for (var f = 0; f < graph.Nodes.Count; f++)
      {
        for (var t = 0; t < graph.Nodes.Count; t++)
        {
          if (f == t) continue;

          var route = graph.GetRoutesFromToWide(graph.Nodes[f], graph.Nodes[t]).FirstOrDefault();

          routes[f, t] = route.totalValue;
        }
      }

      var importantNodes = new int[valveStates.Count(c => !c)];

      var ii = 0;
      for (var i = 0; i < graph.Nodes.Count; i++)
      {
        if (!valveStates[i])
        {
          importantNodes[ii] = i;
          ii++;
        }
      }

      var startNode = graph.Nodes.IndexOf("AA");

      int get(int[] nodes)
      {
        var time = 29;
        var total = 0;
        var current = 0;

        while (time > 0)
        {
          var currentNode = nodes[current];

          var flow = time * valveFlows[currentNode];

          if (flow > 0)
          {
            total += flow;
            time--;
          }

          current++;

          if (current < nodes.Length)
          {
            time -= routes[currentNode, nodes[current]];
          }
          else
          {
            break;
          }
        }

        return total;
      }

      var max = 0;

      var count = 0;

      var maxNode = 5;
      for (; maxNode < Math.Min(14, importantNodes.Length); maxNode++)
      {
        var prevMax = max;

        foreach (var combination in Combinatorics.GetCombinations(importantNodes.Except(startNode).ToArray(), maxNode))
        {
          var temp = combination.ToArray();

          foreach (var permutations in Combinatorics.Permute(temp))
          {
            var nodes = permutations.Prepend(startNode);

            var value = get(nodes);
            if (max < value)
            {
              max = value;
            }

            count++;
          }
        }

        if (prevMax == max)
        {
          break;
        }
      }

      return $"{max} (max node: {maxNode})".ToString();
    }

    public override string Task2()
    {
      throw new NotImplementedException();
    }
  }
}
