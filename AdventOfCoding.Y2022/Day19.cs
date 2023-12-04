using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCoding.Y2022
{
  internal class Day19 : FileDay
  {
    public override string Task1()
    {
      var sum = 0;

      Parallel.ForEach(ReadLines(), new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, line =>
      {
        var watch = Stopwatch.StartNew();
        var blueprint = new Blueprint(24, line);
        var geode = blueprint.GetBestGeodeCount();
        watch.Stop();
        Console.WriteLine($"{blueprint.Id}: {geode} geode, {watch.ElapsedMilliseconds}ms, {blueprint.Count}");
        Interlocked.Add(ref sum, blueprint.Id * geode);
      });

      return sum.ToString();
    }

    public override string Task2()
    {
      var result = 1;

      foreach (var line in ReadLines().Take(3))
      {
        var blueprint = new Blueprint(32, line);
        var geode = blueprint.GetBestGeodeCount();
        result *= geode;
      }

      return result.ToString();
    }

    private class Blueprint
    {
      private readonly int _time;

      public int Id { get; }
      public int OreRobotOre { get; }
      public int ClayRobotOre { get; }
      public int ObsidianRobotOre { get; }
      public int ObsidianRobotClay { get; }
      public int GeodeRobotOre { get; }
      public int GeodeRobotObsidian { get; }
      public ulong Count { get; private set; }

      public Blueprint(int time, string text)
      {
        _time = time;
        Id = GetInt(text, out var index, 10, ':');
        OreRobotOre = GetInt(text, out index, index + 22);
        ClayRobotOre = GetInt(text, out index, index + 27);
        ObsidianRobotOre = GetInt(text, out index, index + 31);
        ObsidianRobotClay = GetInt(text, out index, index + 8);
        GeodeRobotOre = GetInt(text, out index, index + 29);
        GeodeRobotObsidian = GetInt(text, out index, index + 8);
      }

      public int GetBestGeodeCount()
      {
        Count = 0;

        //var possibleOre = PossibleOreMining(_time / 2, 1, 0);
        //var possibleClay = PossibleClayMining(_time / 2, 0, 0, possibleOre);
        //var possibleObisidan = PossibleObsidianMining(_time / 4, 0, 0, 0, possibleOre, possibleClay);
        //var possibeGeode = PossibleGeodeMining(_time / 8, 0, 0, 0, possibleOre, possibleObisidan);
        //if (possibeGeode == 0) return 0;

        var max = 0;

        return GetGeodeCount(_time, Action.DoNothing, 1, 0, 0, 0, 0, 0, 0, 0, ref max);
      }

      private int GetGeodeCount(int time, Action action, int oreRobot, int clayRobot, int obsidianRobot, int geodeRobot, int ore, int clay, int obsidian, int geode, ref int max)
      {
        if (time == 0)
        {
          if (max < geode)
          {
            max = geode;
          }

          return geode;
        }

        if (max > 0)
        {
          if (max >= geode + PossibleGeodeMining(time, oreRobot, obsidianRobot, geodeRobot, ore, obsidian)) return 0;
        }

        Count++;

        var or = oreRobot;
        var cr = clayRobot;
        var obr = obsidianRobot;
        var gr = geodeRobot;

        switch (action)
        {
          case Action.DoNothing:
            break;
          case Action.OreRobot:
            if (ore < OreRobotOre) return 0;

            ore -= OreRobotOre;
            or++;
            break;
          case Action.ClayRobot:
            if (ore < ClayRobotOre) return 0;

            ore -= ClayRobotOre;
            cr++;
            break;
          case Action.ObsidianRobot:
            if (ore < ObsidianRobotOre || clay < ObsidianRobotClay) return 0;

            ore -= ObsidianRobotOre;
            clay -= ObsidianRobotClay;
            obr++;
            break;
          case Action.GeodeRobot:
            if (ore < GeodeRobotOre || obsidian < GeodeRobotObsidian) return 0;

            ore -= GeodeRobotOre;
            obsidian -= GeodeRobotObsidian;
            gr++;
            break;
        }

        // Mining
        ore += oreRobot;
        clay += clayRobot;
        obsidian += obsidianRobot;
        geode += geodeRobot;

        return Math.Max(
          Math.Max(
            Math.Max(
              GetGeodeCount(time - 1, Action.GeodeRobot, or, cr, obr, gr, ore, clay, obsidian, geode, ref max),
              GetGeodeCount(time - 1, Action.ObsidianRobot, or, cr, obr, gr, ore, clay, obsidian, geode, ref max)
            ),
            Math.Max(
              GetGeodeCount(time - 1, Action.ClayRobot, or, cr, obr, gr, ore, clay, obsidian, geode, ref max),
              GetGeodeCount(time - 1, Action.OreRobot, or, cr, obr, gr, ore, clay, obsidian, geode, ref max)
            )
          ),
          GetGeodeCount(time - 1, Action.DoNothing, or, cr, obr, gr, ore, clay, obsidian, geode, ref max)
        );
      }

      private int PossibleOreMining(int time, int oreRobot, int ore)
      {
        var timeMiningMax = time * (time / 2);
        var oreMiningMax = ore + timeMiningMax + time * oreRobot;
        var result = time * (oreMiningMax / OreRobotOre / 2 + oreRobot);
        return result;
      }

      private int PossibleClayMining(int time, int oreRobot, int clayRobot, int ore)
      {
        var timeMiningMax = time * (time / 2);
        var oreMiningMax = ore + timeMiningMax + time * oreRobot;
        var result = time * (oreMiningMax / ClayRobotOre / 2 + clayRobot);
        return result;
      }

      private int PossibleObsidianMining(int time, int oreRobot, int clayRobot, int obsidianRobot, int ore, int clay)
      {
        var timeMiningMax = time * (time / 2);
        var oreMiningMax = ore + timeMiningMax + time * oreRobot;
        var clayMiningMax = clay + timeMiningMax + time * clayRobot;
        var result = time * (Math.Min(oreMiningMax / ObsidianRobotOre, clayMiningMax / ObsidianRobotClay) / 2 + obsidianRobot);
        return result;
      }

      private int PossibleGeodeMining(int time, int oreRobot, int obsidianRobot, int geodeRobot, int ore, int obsidian)
      {
        var timeMiningMax = time * (time / 2);
        var oreMiningMax = ore + timeMiningMax + time * oreRobot;
        var obsidianMiningMax = obsidian + timeMiningMax + time * obsidianRobot;
        var result = time * (Math.Min(oreMiningMax / GeodeRobotOre, obsidianMiningMax / GeodeRobotObsidian) / 2 + geodeRobot);
        return result;
      }

      private enum Action
      {
        DoNothing,
        OreRobot,
        ClayRobot,
        ObsidianRobot,
        GeodeRobot
      }
    }
  }
}
