using System.Text.RegularExpressions;

namespace aoc24;

[ForDay(14)]
public partial class Day14 : Solver {
  [GeneratedRegex(@"^p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)$")]
  private static partial Regex LineRe();

  private List<(int X, int Y, int Vx, int Vy)> robots = [];

  private int width = 101, height = 103;

  public void Presolve(string input) {
    var data = input.Trim();
    foreach (var line in data.Split("\n")) {
      if (LineRe().Match(line) is not { Success: true } match) {
        throw new InvalidDataException($"parse error: ${line}");
      }
      robots.Add((
        int.Parse(match.Groups[1].Value),
        int.Parse(match.Groups[2].Value),
        int.Parse(match.Groups[3].Value),
        int.Parse(match.Groups[4].Value)
        ));
    }
  }

  public string SolveFirst() {
    Dictionary<(bool, bool), int> quadrants = [];
    foreach (var robot in robots) {
      int x = (robot.X + 100 * (robot.Vx > 0 ? robot.Vx : robot.Vx + width)) % width;
      int y = (robot.Y + 100 * (robot.Vy > 0 ? robot.Vy : robot.Vy + height)) % height;
      if (x == width / 2 || y == height / 2) continue;
      var q = (x < width / 2, y < height / 2);
      quadrants[q] = quadrants.GetValueOrDefault(q, 0) + 1;
    }
    return quadrants.Values.Aggregate((a, b) => a * b).ToString();
  }

  private int CountAdjacentRobots(HashSet<(int, int)> all_robots, (int, int) this_robot) {
    var (x, y) = this_robot;
    int count = 0;
    for (int ax = x - 1; all_robots.Contains((ax, y)); ax--) count++;
    for (int ax = x + 1; all_robots.Contains((ax, y)); ax++) count++;
    for (int ay = y - 1; all_robots.Contains((x, ay)); ay--) count++;
    for (int ay = y + 1; all_robots.Contains((x, ay)); ay++) count++;
    return count;
  }

  public string SolveSecond() {
    for (int i = 0; i < int.MaxValue; ++i) {
      HashSet<(int, int)> end_positions = [];
      foreach (var robot in robots) {
        int x = (robot.X + i * (robot.Vx > 0 ? robot.Vx : robot.Vx + width)) % width;
        int y = (robot.Y + i * (robot.Vy > 0 ? robot.Vy : robot.Vy + height)) % height;
        end_positions.Add((x, y));
      }
      if (end_positions.Select(r => CountAdjacentRobots(end_positions, r)).Max() > 10) {
        return i.ToString();
      }
    }
    throw new ArgumentException();
  }

  public void SetTestMode() {
    width = 11;
    height = 7;
  }
}
