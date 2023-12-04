using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day16 : FileDay
  {
    private static readonly Regex _regex = new(@"(\w+): (\d+)", RegexOptions.Compiled);

    private class Aunt
    {
      public int? children { get; set; }
      public int? cats { get; set; }
      public int? samoyeds { get; set; }
      public int? pomeranians { get; set; }
      public int? akitas { get; set; }
      public int? vizslas { get; set; }
      public int? goldfish { get; set; }
      public int? trees { get; set; }
      public int? cars { get; set; }
      public int? perfumes { get; set; }
    }

    public override string Task1()
    {
      var aunts = new List<Aunt>();

      foreach (var line in ReadLines())
      {
        var a = new Aunt();

        foreach (Match match in _regex.Matches(line))
        {
          switch (match.Groups[1].Value)
          {
            case nameof(Aunt.children):
              a.children = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.cats):
              a.cats = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.samoyeds):
              a.samoyeds = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.pomeranians):
              a.pomeranians = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.akitas):
              a.akitas = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.vizslas):
              a.vizslas = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.goldfish):
              a.goldfish = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.trees):
              a.trees = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.cars):
              a.cars = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.perfumes):
              a.perfumes = GetInt(match.Groups[2].Value);
              break;
          }
        }

        aunts.Add(a);
      }

      for (var i = 0; i < aunts.Count; i++)
      {
        if (aunts[i] is
          {
            children: 3 or null,
            cats: 7 or null,
            samoyeds: 2 or null,
            pomeranians: 3 or null,
            akitas: 0 or null,
            vizslas: 0 or null,
            goldfish: 5 or null,
            trees: 3 or null,
            cars: 2 or null,
            perfumes: 1 or null
          }) return (i + 1).ToString();
      }

      return null;
    }

    public override string Task2()
    {
      var aunts = new List<Aunt>();

      foreach (var line in ReadLines())
      {
        var a = new Aunt();

        foreach (Match match in _regex.Matches(line))
        {
          switch (match.Groups[1].Value)
          {
            case nameof(Aunt.children):
              a.children = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.cats):
              a.cats = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.samoyeds):
              a.samoyeds = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.pomeranians):
              a.pomeranians = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.akitas):
              a.akitas = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.vizslas):
              a.vizslas = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.goldfish):
              a.goldfish = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.trees):
              a.trees = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.cars):
              a.cars = GetInt(match.Groups[2].Value);
              break;
            case nameof(Aunt.perfumes):
              a.perfumes = GetInt(match.Groups[2].Value);
              break;
          }
        }

        aunts.Add(a);
      }

      for (var i = 0; i < aunts.Count; i++)
      {
        if (aunts[i] is
          {
            children: 3 or null,
            cats: > 7 or null,
            samoyeds: 2 or null,
            pomeranians: < 3 or null,
            akitas: 0 or null,
            vizslas: 0 or null,
            goldfish: < 5 or null,
            trees: > 3 or null,
            cars: 2 or null,
            perfumes: 1 or null
          }) return (i + 1).ToString();
      }

      return null;
    }
  }
}
