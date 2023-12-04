using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day02 : FileDay
  {
    public override string Task1()
    {
      var count = 0;
      var sum = 0;

      foreach (var line in ReadLines())
      {
        if (line == "") continue;

        var enemy = line[0];
        var me = line[2];

        count++;

        sum += GetPoint(enemy, me);
      }

      return sum.ToString() + " " + count;
    }

    private Symbol GetSymbol(char c) => c switch
    {
      'A' or 'X' => Symbol.Rock,
      'B' or 'Y' => Symbol.Paper,
      'C' or 'Z' => Symbol.Scissors,
      _ => throw new ArgumentOutOfRangeException(),
    };

    enum Symbol
    {
      Rock = 1,
      Paper = 2,
      Scissors = 3
    }

    private int GetPoint(char enemy, char me) => (GetSymbol(me), GetSymbol(enemy)) switch
    {
      (Symbol.Rock, Symbol.Rock) => 4,
      (Symbol.Paper, Symbol.Paper) => 5,
      (Symbol.Scissors, Symbol.Scissors) => 6,
      (Symbol.Rock, Symbol.Paper) => 1,
      (Symbol.Rock, Symbol.Scissors) => 7,
      (Symbol.Paper, Symbol.Rock) => 8,
      (Symbol.Paper, Symbol.Scissors) => 2,
      (Symbol.Scissors, Symbol.Rock) => 3,
      (Symbol.Scissors, Symbol.Paper) => 9,
      _ => throw new InvalidOperationException()
      };

    public override string Task2()
    {
      var sum = 0;

      foreach (var line in ReadLines())
      {
        if (line == "") continue;

        var enemy = GetSymbol(line[0]);
        var result = GetRoundResult(line[2]);

        var me = GetMine(enemy, result);

        sum += (int)me + (int)result;
      }

      return sum.ToString();
    }

    private Symbol GetMine(Symbol enemy, RoundResult result) => (enemy, result) switch {
      (Symbol.Rock, RoundResult.Win) => Symbol.Paper,
      (Symbol.Paper, RoundResult.Win) => Symbol.Scissors,
      (Symbol.Scissors, RoundResult.Win) => Symbol.Rock,

      (Symbol.Rock, RoundResult.Lose) => Symbol.Scissors,
      (Symbol.Paper, RoundResult.Lose) => Symbol.Rock,
      (Symbol.Scissors, RoundResult.Lose) => Symbol.Paper,

      (Symbol.Rock, RoundResult.Draw) => Symbol.Rock,
      (Symbol.Paper, RoundResult.Draw) => Symbol.Paper,
      (Symbol.Scissors, RoundResult.Draw) => Symbol.Scissors,

      _ => throw new InvalidOperationException()
    };

    private RoundResult GetRoundResult(char c) => c switch
    {
      'X' => RoundResult.Lose,
      'Y' => RoundResult.Draw,
      'Z' => RoundResult.Win,
      _ => throw new ArgumentOutOfRangeException(),
    };

    enum RoundResult
    {
      Win = 6,
      Draw = 3,
      Lose = 0
    }

  }
}
