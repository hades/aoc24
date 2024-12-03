using aoc24;

namespace tests;
public class TestDay03
{
  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day03();
    solver.Presolve("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))");
    Assert.Equal("161", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day03();
    solver.Presolve("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))");
    Assert.Equal("48", solver.SolveSecond());
  }
}
