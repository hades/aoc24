﻿using aoc24;

namespace tests;
public class TestDay08
{
  private static readonly string data = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
".Replace("\r\n", "\n");

  [Fact]
  public void TestFirstPart()
  {
    var solver = new Day08();
    solver.Presolve(data);
    Assert.Equal("14", solver.SolveFirst());
  }

  [Fact]
  public void TestSecondPart()
  {
    var solver = new Day08();
    solver.Presolve(data);
    Assert.Equal("34", solver.SolveSecond());
  }
}
