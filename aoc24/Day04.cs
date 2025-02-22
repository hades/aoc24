namespace aoc24;

[ForDay(4)]
public class Day04 : Solver {
  private int width, height;
  private char[,] data;

  public void Presolve(string input) {
    var lines = input.Trim().Split("\n").ToList();
    height = lines.Count;
    width = lines[0].Length;
    data = new char[height, width];
    for (int i = 0; i < height; i++) {
      for (int j = 0; j < width; j++) {
        data[i, j] = lines[i][j];
      }
    }
  }

  private static readonly string word = "XMAS";

  public string SolveFirst() {
    int counter = 0;
    for (int start_i = 0; start_i < height; start_i++) {
      for (int start_j = 0; start_j < width; start_j++) {
        if (data[start_i, start_j] != word[0]) continue;
        for (int di = -1; di <= 1; di++) {
          for (int dj = -1; dj <= 1; dj++) {
            if (di == 0 && dj == 0) continue;
            int end_i = start_i + di * (word.Length - 1);
            int end_j = start_j + dj * (word.Length - 1);
            if (end_i < 0 || end_j < 0 || end_i >= height || end_j >= width) continue;
            for (int k = 1; k < word.Length; k++) {
              if (data[start_i + di * k, start_j + dj * k] != word[k]) break;
              if (k == word.Length - 1) counter++;
            }
          }
        }
      }
    }
    return counter.ToString();
  }

  public string SolveSecond() {
    int counter = 0;
    for (int start_i = 1; start_i < height - 1; start_i++) {
      for (int start_j = 1; start_j < width - 1; start_j++) {
        if (data[start_i, start_j] != 'A') continue;
        int even_mas_starts = 0;
        for (int di = -1; di <= 1; di++) {
          for (int dj = -1; dj <= 1; dj++) {
            if (di == 0 && dj == 0) continue;
            if ((di + dj) % 2 != 0) continue;
            if (data[start_i + di, start_j + dj] != 'M') continue;
            if (data[start_i - di, start_j - dj] != 'S') continue;
            even_mas_starts++;
          }
        }
        if (even_mas_starts == 2) counter++;
      }
    }
    return counter.ToString();
  }
}
