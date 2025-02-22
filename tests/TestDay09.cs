using aoc24;

namespace tests;
public class TestDay09 {
  private static readonly string data = "2333133121414131402";

  [Fact]
  public void TestFirstPart() {
    var solver = new Day09();
    solver.Presolve(data);
    Assert.Equal("1928", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart() {
    var solver = new Day09();
    solver.Presolve(data);
    Assert.Equal("2858", solver.SolveSecond());
  }
}
