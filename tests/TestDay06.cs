using aoc24;

namespace tests;
public class TestDay06
{
  private static readonly string data = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...
".Replace("\r\n", "\n");

  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day06();
    solver.Presolve(data);
    Assert.Equal("41", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day06();
    solver.Presolve(data);
    Assert.Equal("6", solver.SolveSecond());
  }
}
