using aoc24;

namespace tests;
public class TestDay07
{
  private static readonly string data = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
".Replace("\r\n", "\n");

  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day07();
    solver.Presolve(data);
    Assert.Equal("3749", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day07();
    solver.Presolve(data);
    Assert.Equal("11387", solver.SolveSecond());
  }
}
