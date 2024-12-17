using aoc24;

namespace tests;
public class TestDay17
{
  private const string example1 = @"Register A: 51571418
Register B: 0
Register C: 0

Program: 2,4,1,1,7,5,0,3,1,4,4,5,5,5,3,0
";

  [Theory]
  [InlineData(example1, "4,0,4,7,1,2,7,1,6")]
  public void TestFirstPart(string data, string answer)
  {
    var solver = new Day17();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example1, "202322348616234")]
  public void TestSecondPart(string data, string answer)
  {
    var solver = new Day17();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
