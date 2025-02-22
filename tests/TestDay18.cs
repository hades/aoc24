using aoc24;

namespace tests;
public class TestDay18 {
  private const string example1 = @"5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0
";

  [Theory]
  [InlineData(example1, "22")]
  public void TestFirstPart(string data, string answer) {
    var solver = new Day18();
    solver.SetTestData();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example1, "6,1")]
  public void TestSecondPart(string data, string answer) {
    var solver = new Day18();
    solver.SetTestData();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
