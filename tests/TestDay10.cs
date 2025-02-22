using aoc24;

namespace tests;
public class TestDay10 {
  private const string data = "89010123\n78121874\n87430965\n96549874\n45678903\n32019012\n01329801\n10456732\n";

  [Fact]
  public void TestFirstPart() {
    var solver = new Day10();
    solver.Presolve(data);
    Assert.Equal("36", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart() {
    var solver = new Day10();
    solver.Presolve(data);
    Assert.Equal("81", solver.SolveSecond());
  }
}
