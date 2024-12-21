using aoc24;

namespace tests;
public class TestDay21
{
  private const string example = @"029A
980A
179A
456A
379A
";

  [Theory]
  [InlineData(example, "126384")]
  public void TestFirstPart(string data, string answer)
  {
    var solver = new Day21();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example, "154115708116294")]
  public void TestSecondPart(string data, string answer)
  {
    var solver = new Day21();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
