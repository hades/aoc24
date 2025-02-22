using MathNet.Numerics;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace aoc24;

[ForDay(3)]
public partial class Day03 : Solver {
  [GeneratedRegex(@"mul[(](\d{1,3}),(\d{1,3})[)]")]
  private partial Regex mulRegex();

  [GeneratedRegex(@"(do)[(][)]|(don't)[(][)]|(mul)[(](\d{1,3}),(\d{1,3})[)]")]
  private partial Regex fullRegex();

  private string input;

  public void Presolve(string input) {
    this.input = input.Trim();
  }

  public string SolveFirst() => mulRegex().Matches(input)
      .Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))
      .Sum().ToString();

  public string SolveSecond() {
    bool enabled = true;
    int sum = 0;
    foreach (Match match in fullRegex().Matches(input)) {
      if (match.Groups[1].Length > 0) {
        enabled = true;
      } else if (match.Groups[2].Length > 0) {
        enabled = false;
      } else if (enabled) {
        sum += int.Parse(match.Groups[4].Value) * int.Parse(match.Groups[5].Value);
      }
    }
    return sum.ToString();
  }
}
