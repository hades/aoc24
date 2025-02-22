using QuickGraph;
using System.Numerics;
using System.Text.RegularExpressions;
using Point = (int, int);

namespace aoc24;

[ForDay(13)]
public partial class Day13 : Solver {
  private record struct Button(int X, int Y);
  private record struct Machine(int X, int Y, Button A, Button B);
  private List<Machine> machines = [];

  [GeneratedRegex(@"^Button (A|B): X\+(\d+), Y\+(\d+)$")]
  private static partial Regex ButtonSpec();

  [GeneratedRegex(@"^Prize: X=(\d+), Y=(\d+)$")]
  private static partial Regex PrizeSpec();

  public void Presolve(string input) {
    var machine_specs = input.Trim().Split("\n\n").ToList();
    foreach (var spec in machine_specs) {
      var lines = spec.Split("\n").ToList();
      if (ButtonSpec().Match(lines[0]) is not { Success: true } button_a_match
        || ButtonSpec().Match(lines[1]) is not { Success: true } button_b_match
        || PrizeSpec().Match(lines[2]) is not { Success: true } prize_match) {
        throw new InvalidDataException($"parse error: ${lines}");
      }
      machines.Add(new Machine(
        int.Parse(prize_match.Groups[1].Value),
        int.Parse(prize_match.Groups[2].Value),
        new Button(int.Parse(button_a_match.Groups[2].Value), int.Parse(button_a_match.Groups[3].Value)),
        new Button(int.Parse(button_b_match.Groups[2].Value), int.Parse(button_b_match.Groups[3].Value))
        ));
    }
  }

  private string Solve(bool unit_conversion) {
    BigInteger total_cost = 0;
    foreach (var machine in machines) {
      long prize_x = machine.X + (unit_conversion ? 10000000000000 : 0);
      long prize_y = machine.Y + (unit_conversion ? 10000000000000 : 0);
      BigInteger det = machine.A.X * machine.B.Y - machine.B.X * machine.A.Y;
      if (det == 0) continue;
      BigInteger det_a = prize_x * machine.B.Y - machine.B.X * prize_y;
      BigInteger det_b = prize_y * machine.A.X - machine.A.Y * prize_x;
      var (a, a_rem) = BigInteger.DivRem(det_a, det);
      var (b, b_rem) = BigInteger.DivRem(det_b, det);
      if (a_rem != 0 || b_rem != 0) continue;
      total_cost += a * 3 + b;
    }
    return total_cost.ToString();
  }

  public string SolveFirst() => Solve(false);
  public string SolveSecond() => Solve(true);
}
