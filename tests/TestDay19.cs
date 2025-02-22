using aoc24;

namespace tests;
public class TestDay19 {
  private const string example1 = @"r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb
";

  [Theory]
  [InlineData(example1, "6")]
  public void TestFirstPart(string data, string answer) {
    var solver = new Day19();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example1, "16")]
  public void TestSecondPart(string data, string answer) {
    var solver = new Day19();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
