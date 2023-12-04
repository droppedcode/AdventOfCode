using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day12 : FileDay
  {
    public override string Task1()
    {
      var current = 0;
      var sum = 0;
      var isMinus = false;

      foreach (var c in ReadFile())
      {
        if (c == '-')
        {
          isMinus = true;
        }
        else if (char.IsDigit(c))
        {
          current = current * 10 + (c - '0');
        }
        else
        {
          sum += isMinus ? -current : current;
          current = 0;
          isMinus = false;
        }
      }

      return sum.ToString();
    }

    private int GetSum(JsonElement element)
    {
      switch (element.ValueKind)
      {
        case JsonValueKind.Object:
          {
            var sum = 0;

            foreach (var property in element.EnumerateObject())
            {
              if (property.Value.ValueKind == JsonValueKind.String && property.Value.GetString() == "red") return 0;

              sum += GetSum(property.Value);
            }

            return sum;
          }
        case JsonValueKind.Number:
          return element.GetInt32();
        case JsonValueKind.Array:
          {
            var sum = 0;

            foreach (var item in element.EnumerateArray())
            {
              sum += GetSum(item);
            }

            return sum;
          }
      }

      return 0;
    }

    public override string Task2()
    {
      var document = JsonDocument.Parse(ReadFile());

      return GetSum(document.RootElement).ToString();
    }
  }
}
