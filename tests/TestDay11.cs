using aoc24;

namespace tests;
public class TestDay11 {
  private const string data = "64554 35 906 6 6960985 5755 975820 0";

  [Fact]
  public void TestFirstPart() {
    var solver = new Day11();
    solver.Presolve(data);
    Assert.Equal("175006", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart() {
    var solver = new Day11();
    solver.Presolve(data);
    Assert.Equal("207961583799296", solver.SolveSecond());
  }
}
