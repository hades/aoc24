using aoc24;

namespace tests;
public class TestDay01
{
  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day01();
    solver.Presolve("3   4\n4   3\n2   5\n1   3\n3   9\n3   3\n");
    Assert.Equal("11", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day01();
    solver.Presolve("3   4\n4   3\n2   5\n1   3\n3   9\n3   3\n");
    Assert.Equal("31", solver.SolveSecond());
  }
}
