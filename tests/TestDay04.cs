using aoc24;

namespace tests;
public class TestDay04
{
  private static readonly string data = "MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX\n";

  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day04();
    solver.Presolve(data);
    Assert.Equal("18", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day04();
    solver.Presolve(data);
    Assert.Equal("9", solver.SolveSecond());
  }
}
