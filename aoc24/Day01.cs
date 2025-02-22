using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace aoc24;

[ForDay(1)]
public class Day01 : Solver {
  private ImmutableArray<int> left;
  private ImmutableArray<int> right;

  public void Presolve(string input) {
    var pairs = input.Trim().Split("\n").Select(line => Regex.Split(line, @"\s+"));
    left = pairs.Select(item => int.Parse(item[0])).ToImmutableArray();
    right = pairs.Select(item => int.Parse(item[1])).ToImmutableArray();
  }

  public string SolveFirst() => left.Sort().Zip(right.Sort()).Select((pair) => int.Abs(pair.First - pair.Second)).Sum().ToString();

  public string SolveSecond() => left.Select((number) => number * right.Where(v => v == number).Count()).Sum().ToString();
}
