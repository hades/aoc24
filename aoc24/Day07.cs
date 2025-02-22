using System.Collections.Immutable;

namespace aoc24;

[ForDay(7)]
public class Day07 : Solver {
  private ImmutableList<(long, ImmutableList<long>)> equations;

  public void Presolve(string input) {
    equations = input.Trim().Split("\n")
      .Select(line => line.Split(": "))
      .Select(split => (long.Parse(split[0]), split[1].Split(" ").Select(long.Parse).ToImmutableList()))
      .ToImmutableList();
  }

  private bool TrySolveWithConcat(long lhs, long head, ImmutableList<long> tail) {
    var lhs_string = lhs.ToString();
    var head_string = head.ToString();
    return lhs_string.Length > head_string.Length &&
      lhs_string.EndsWith(head_string) &&
      SolveEquation(long.Parse(lhs_string.Substring(0, lhs_string.Length - head_string.Length)), tail, true);
  }

  private bool SolveEquation(long lhs, ImmutableList<long> rhs, bool with_concat = false) {
    if (rhs.Count == 1) return lhs == rhs[0];
    long head = rhs[rhs.Count - 1];
    var tail = rhs.GetRange(0, rhs.Count - 1);
    return (SolveEquation(lhs - head, tail, with_concat))
      || (lhs % head == 0) && SolveEquation(lhs / head, tail, with_concat)
      || with_concat && TrySolveWithConcat(lhs, head, tail);
  }

  public string SolveFirst() => equations
    .Where(eq => SolveEquation(eq.Item1, eq.Item2))
    .Select(eq => eq.Item1)
    .Sum().ToString();
  public string SolveSecond() => equations
    .Where(eq => SolveEquation(eq.Item1, eq.Item2, true))
    .Select(eq => eq.Item1)
    .Sum().ToString();
}
