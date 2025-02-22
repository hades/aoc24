using aoc24;

namespace tests;
public class TestDay13 {
  private const string data = @"Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279
";

  [Fact]
  public void TestFirstPart() {
    var solver = new Day13();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal("480", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart() {
    var solver = new Day13();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal("875318608908", solver.SolveSecond());
  }
}
