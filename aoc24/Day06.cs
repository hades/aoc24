using System.Collections.Immutable;

namespace aoc24;

[ForDay(6)]
public class Day06 : Solver {
  private readonly (int, int)[] directions = [
    (0, -1),
    (1, 0),
    (0, 1),
    (-1, 0)
    ];

  private ImmutableArray<string> data;
  private int width, height;
  private ImmutableHashSet<(int, int)> guard_path;
  private int start_x, start_y;

  public void Presolve(string input) {
    data = input.Trim().Split("\n").ToImmutableArray();
    width = data[0].Length;
    height = data.Length;
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] == '^') {
          start_x = i;
          start_y = j;
          break;
        }
      }
    }
    guard_path = Walk().Path.ToImmutableHashSet();
  }

  private bool IsWithinBounds(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;

  private (HashSet<(int, int)> Path, bool IsLoop) Walk((int, int)? obstacle = null) {
    int obstacle_x = obstacle?.Item1 ?? -1;
    int obstacle_y = obstacle?.Item2 ?? -1;
    int direction = 0;
    int x = start_x;
    int y = start_y;
    bool loop = false;
    HashSet<(int, int, int)> positions = new();
    while (IsWithinBounds(x, y)) {
      if (positions.Contains((x, y, direction))) {
        loop = true;
        break;
      }
      positions.Add((x, y, direction));
      int nx = x + directions[direction].Item1;
      int ny = y + directions[direction].Item2;

      while (IsWithinBounds(nx, ny) && (data[ny][nx] == '#' || (nx == obstacle_x && ny == obstacle_y))) {
        direction = (direction + 1) % 4;
        nx = x + directions[direction].Item1;
        ny = y + directions[direction].Item2;
      }
      x = nx;
      y = ny;
    }
    return (positions.Select(position => (position.Item1, position.Item2)).ToHashSet(), loop);
  }

  public string SolveFirst() => guard_path.Count.ToString();
  public string SolveSecond() => guard_path
    .Where(position => position != (start_x, start_y))
    .Where(position => Walk(position).IsLoop)
    .Count().ToString();
}
