using aoc24;

namespace tests;
public class TestDay02
{
  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day02();
    solver.Presolve("7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\n8 6 4 4 1\n1 3 6 7 9");
    Assert.Equal("2", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day02();
    solver.Presolve("7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\n8 6 4 4 1\n1 3 6 7 9");
    Assert.Equal("4", solver.SolveSecond());
  }
}
