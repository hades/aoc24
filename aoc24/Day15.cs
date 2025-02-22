namespace aoc24;

[ForDay(15)]
public class Day15 : Solver {
  private char[][] map;
  private int width, height;
  private string movements;

  public void Presolve(string input) {
    var blocks = input.Trim().Split("\n\n").ToList();
    map = blocks[0].Split("\n").Select(line => line.ToArray()).ToArray();
    width = map[0].Length;
    height = map.Length;
    movements = blocks[1];
  }

  public string SolveFirst() {
    var data = map.Select(row => row.ToArray()).ToArray();
    int robot_x = -1, robot_y = -1;
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] == '@') {
          robot_x = i;
          robot_y = j;
          data[j][i] = '.';
          break;
        }
      }
    }
    foreach (var m in movements) {
      var (dx, dy) = m switch {
        '^' => (0, -1),
        '>' => (1, 0),
        'v' => (0, 1),
        '<' => (-1, 0),
        _ => (0, 0)
      };
      if ((dx, dy) == (0, 0)) continue;
      var (x, y) = (robot_x + dx, robot_y + dy);
      if (data[y][x] == '#') continue;
      if (data[y][x] == '.') {
        (robot_x, robot_y) = (x, y);
        continue;
      }
      var (end_of_block_x, end_of_block_y) = (x + dx, y + dy);
      while (data[end_of_block_y][end_of_block_x] == 'O') {
        (end_of_block_x, end_of_block_y) = (end_of_block_x + dx, end_of_block_y + dy);
      }
      if (data[end_of_block_y][end_of_block_x] == '.') {
        data[end_of_block_y][end_of_block_x] = 'O';
        data[y][x] = '.';
        (robot_x, robot_y) = (x, y);
      }
    }
    long answer = 0;
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] == 'O') {
          answer += i + 100 * j;
        }
      }
    }
    return answer.ToString();
  }

  public string SolveSecond() {
    var expanded_data = map.Select(row => row.SelectMany<char, char>(ch => ch switch {
      '#' => ['#', '#'],
      'O' => ['[', ']'],
      '.' => ['.', '.'],
      '@' => ['@', '.']
    }).ToArray()).ToArray();
    int robot_x = -1, robot_y = -1;
    for (int i = 0; i < width * 2; i++) {
      for (int j = 0; j < height; j++) {
        if (expanded_data[j][i] == '@') {
          robot_x = i;
          robot_y = j;
          expanded_data[j][i] = '.';
          break;
        }
      }
    }
    if (robot_x < 0) throw new InvalidDataException();
    foreach (var m in movements) {
      var (dx, dy) = m switch {
        '^' => (0, -1),
        '>' => (1, 0),
        'v' => (0, 1),
        '<' => (-1, 0),
        _ => (0, 0)
      };
      if ((dx, dy) == (0, 0)) continue;
      var (x, y) = (robot_x + dx, robot_y + dy);
      if (expanded_data[y][x] == '#') continue;
      if (expanded_data[y][x] == '.') {
        (robot_x, robot_y) = (x, y);
        continue;
      }
      if (dy == 0) {
        var (end_of_block_x, end_of_block_y) = (x + dx * 2, y);
        while (expanded_data[end_of_block_y][end_of_block_x] == '[' ||
               expanded_data[end_of_block_y][end_of_block_x] == ']') {
          (end_of_block_x, end_of_block_y) = (end_of_block_x + dx, end_of_block_y);
        }
        if (expanded_data[end_of_block_y][end_of_block_x] == '.') {
          var (fill_start, fill_end) = dx > 0 ? (x + 1, end_of_block_x) : (end_of_block_x, x);
          for (int fill_x = fill_start; fill_x < fill_end; fill_x += 2) {
            expanded_data[y][fill_x] = '[';
            expanded_data[y][fill_x + 1] = ']';
          }
          expanded_data[y][x] = '.';
          (robot_x, robot_y) = (x, y);
        }
        continue;
      }
      List<(int, int)> boxes_to_move = [(x, y)];
      if (expanded_data[y][x] == ']') {
        boxes_to_move.Add((x - 1, y));
      } else {
        boxes_to_move.Add((x + 1, y));
      }
      List<(int, int)> boxes_move_ordered = [];
      bool impossible = false;
      while (boxes_to_move.Count > 0) {
        HashSet<(int, int)> next_boxes = [];
        foreach (var (box_x, box_y) in boxes_to_move) {
          boxes_move_ordered.Add((box_x, box_y));
          if (expanded_data[box_y + dy][box_x] == '.') continue;
          if (expanded_data[box_y + dy][box_x] == '#') {
            impossible = true;
            break;
          }
          next_boxes.Add((box_x, box_y + dy));
          if (expanded_data[box_y + dy][box_x] == ']') {
            next_boxes.Add((box_x - 1, box_y + dy));
          } else {
            next_boxes.Add((box_x + 1, box_y + dy));
          }
        }
        if (impossible) break;
        boxes_to_move = [.. next_boxes];
      }
      if (impossible) continue;
      boxes_move_ordered.Reverse();
      foreach (var (box_x, box_y) in boxes_move_ordered) {
        expanded_data[box_y + dy][box_x] = expanded_data[box_y][box_x];
        expanded_data[box_y][box_x] = '.';
      }
      (robot_x, robot_y) = (x, y);
    }
    long answer = 0;
    for (int i = 0; i < width * 2; i++) {
      for (int j = 0; j < height; j++) {
        if (expanded_data[j][i] == '[') {
          answer += i + 100 * j;
        }
      }
    }
    return answer.ToString();
  }
}
