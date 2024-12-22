using aoc24;

namespace tests;
public class TestDay22
{
  private const string example1 = @"1
10
100
2024
";
  private const string example2 = @"1
2
3
2024
";

  [Theory]
  [InlineData(example1, "37327623")]
  [InlineData(example2, "37990510")]
  public void TestFirstPart(string data, string answer)
  {
    var solver = new Day22();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example1, "24")]
  [InlineData(example2, "23")]
  public void TestSecondPart(string data, string answer)
  {
    var solver = new Day22();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
