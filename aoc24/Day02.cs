using MathNet.Numerics.LinearAlgebra;
using System.Collections.Immutable;

namespace aoc24;

[ForDay(2)]
public class Day02 : Solver {
  private ImmutableList<Vector<Double>> data;

  public void Presolve(string input) {
    data = input.Trim().Split("\n").Select(line => Vector<Double>.Build.DenseOfEnumerable(line.Split(' ').Select(double.Parse))).ToImmutableList();
  }

  private bool IsReportSafe(Vector<Double> report) {
    Vector<Double> delta = report.SubVector(1, report.Count - 1).Subtract(report.SubVector(0, report.Count - 1));
    return (delta.ForAll(x => x > 0) || delta.ForAll(x => x < 0)) && Vector<Double>.Abs(delta).Max() <= 3;
  }

  private bool IsDampenedReportSafe(Vector<Double> report) {
    for (Double i = 0; i < report.Count; ++i) {
      var dampened = Vector<Double>.Build.DenseOfEnumerable(report.EnumerateIndexed().Where(item => item.Item1 != i).Select(item => item.Item2));
      if (IsReportSafe(dampened)) return true;
    }
    return false;
  }

  public string SolveFirst() => data.Where(IsReportSafe).Count().ToString();

  public string SolveSecond() => data.Where(IsDampenedReportSafe).Count().ToString();
}
