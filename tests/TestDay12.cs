using aoc24;

namespace tests;
public class TestDay12 {
  private const string data = @"RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE
";

  [Fact]
  public void TestFirstPart() {
    var solver = new Day12();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal("1930", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart() {
    var solver = new Day12();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal("1206", solver.SolveSecond());
  }
}
