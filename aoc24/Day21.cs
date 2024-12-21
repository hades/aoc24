namespace aoc24;

[ForDay(21)]
public class Day21 : Solver {
  private static readonly Dictionary<char, (int, int)> numeric_keypad = new() {
    ['A']= (0, 0),
    ['0']= (-1, 0),
    ['1']= (-2, -1),
    ['2']= (-1, -1),
    ['3']= (0, -1),
    ['4']= (-2, -2),
    ['5']= (-1, -2),
    ['6']= (0, -2),
    ['7']= (-2, -3),
    ['8']= (-1, -3),
    ['9']= (0, -3),
  };
  private static readonly Dictionary<char, (int, int)> directional_keypad = new() {
    ['A']= (0, 0),
    ['^']= (-1, 0),
    ['<']= (-2, 1),
    ['v']= (-1, 1),
    ['>']= (0, 1),
  };

  private string[] data;

  public void Presolve(string input) {
    data = input.Trim().Split("\n");
  }

  private long TypeNumber(string input, int depth) {
    char cur = 'A';
    long result = 0;
    foreach (char ch in input) {
      result += EncodeDigit(ch, cur, depth);
      cur = ch;
    }
    return result;
  }

  private long TypeArrows(string input, int depth) {
    if (depth == 0) return input.Length;
    char cur = 'A';
    long result = 0;
    foreach (char ch in input) {
      result += EncodeArrow(ch, cur, depth - 1);
      cur = ch;
    }
    return result;
  }

  private long EncodeDigit(char ch, char cur, int depth) {
    return EnumerateNumericalEncodings(numeric_keypad[cur], numeric_keypad[ch])
      .Select(encoding => TypeArrows(encoding + "A", depth)).Min();
  }

  private IEnumerable<string> EnumerateNumericalEncodings((int, int) pos1, (int, int) pos2) {
    if (pos1 == pos2) yield return "";
    if (pos1.Item1 > pos2.Item1 && (pos1.Item1 != -1 || pos1.Item2 != 0)) {
      foreach (var encoding in EnumerateNumericalEncodings((pos1.Item1 - 1, pos1.Item2), pos2)) {
        yield return "<" + encoding;
      }
    }
    if (pos1.Item1 < pos2.Item1) {
      foreach (var encoding in EnumerateNumericalEncodings((pos1.Item1 + 1, pos1.Item2), pos2)) {
        yield return ">" + encoding;
      }
    }
    if (pos1.Item2 > pos2.Item2) {
      foreach (var encoding in EnumerateNumericalEncodings((pos1.Item1, pos1.Item2 - 1), pos2)) {
        yield return "^" + encoding;
      }
    }
    if (pos1.Item2 < pos2.Item2 && (pos1.Item1 != -2 || pos1.Item2 != -1)) {
      foreach (var encoding in EnumerateNumericalEncodings((pos1.Item1, pos1.Item2 + 1), pos2)) {
        yield return "v" + encoding;
      }
    }
  }

  private Dictionary<(char, char, int), long> arrow_encoding_cache = [];
  private long EncodeArrow(char arrow, char previous, int depth) {
    if (arrow_encoding_cache.TryGetValue((arrow, previous, depth), out var encoding)) return encoding;
    long result = EnumerateArrowEncodings(directional_keypad[previous], directional_keypad[arrow])
      .Select(encoding => TypeArrows(encoding + "A", depth)).Min();
    arrow_encoding_cache[(arrow, previous, depth)] = result;
    return result;
  }

  private IEnumerable<string> EnumerateArrowEncodings((int, int) pos1, (int, int) pos2) {
    if (pos1 == pos2) yield return "";
    if (pos1.Item1 > pos2.Item1 && (pos1.Item1 != -1 || pos1.Item2 != 0)) {
      foreach (var encoding in EnumerateArrowEncodings((pos1.Item1 - 1, pos1.Item2), pos2)) {
        yield return "<" + encoding;
      }
    }
    if (pos1.Item1 < pos2.Item1) {
      foreach (var encoding in EnumerateArrowEncodings((pos1.Item1 + 1, pos1.Item2), pos2)) {
        yield return ">" + encoding;
      }
    }
    if (pos1.Item2 > pos2.Item2 && (pos1.Item1 != -2 || pos1.Item2 != 1)) {
      foreach (var encoding in EnumerateArrowEncodings((pos1.Item1, pos1.Item2 - 1), pos2)) {
        yield return "^" + encoding;
      }
    }
    if (pos1.Item2 < pos2.Item2 ) {
      foreach (var encoding in EnumerateArrowEncodings((pos1.Item1, pos1.Item2 + 1), pos2)) {
        yield return "v" + encoding;
      }
    }
  }

  public string SolveFirst() => data.Select(str => long.Parse(str[..^1]) * TypeNumber(str, 2))
    .Sum().ToString();
  public string SolveSecond() => data.Select(str => long.Parse(str[..^1]) * TypeNumber(str, 25))
    .Sum().ToString();
}