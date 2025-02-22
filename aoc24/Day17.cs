using System.Text.RegularExpressions;

namespace aoc24;

[ForDay(17)]
public class Day17 : Solver {
  private List<int> program;
  private long a, b, c;

  public void Presolve(string input) {
    var data = input.Trim().Split("\n");
    a = long.Parse(Regex.Match(data[0], @"\d+").Value);
    b = long.Parse(Regex.Match(data[1], @"\d+").Value);
    c = long.Parse(Regex.Match(data[2], @"\d+").Value);
    program = data[4].Split(" ")[1].Split(",").Select(int.Parse).ToList();
  }

  public string SolveFirst() => String.Join(',', Execute(a, b, c));

  private List<int> Execute(long a, long b, long c) {
    List<int> output = [];
    Func<long, long> combo = operand => operand switch {
      <= 3 => operand,
      4 => a,
      5 => b,
      6 => c,
      _ => throw new InvalidDataException(),
    };
    int ip = 0;
    while (ip < program.Count - 1) {
      switch (program[ip]) {
        case 0:
          a = a >> (int)combo(program[ip + 1]);
          break;
        case 1:
          b = b ^ program[ip + 1];
          break;
        case 2:
          b = combo(program[ip + 1]) % 8;
          break;
        case 3:
          if (a != 0) {
            ip = program[ip + 1];
            continue;
          }
          break;
        case 4:
          b = b ^ c;
          break;
        case 5:
          output.Add((int)(combo(program[ip + 1]) % 8));
          break;
        case 6:
          b = a >> (int)combo(program[ip + 1]);
          break;
        case 7:
          c = a >> (int)combo(program[ip + 1]);
          break;
      }
      ip += 2;
    }
    return output;
  }

  public string SolveSecond() {
    Dictionary<int, List<(int, int, int)>> mapping = [];
    for (int start_a = 0; start_a < 512; start_a++) {
      var output = Execute(start_a, b, c);
      mapping.TryAdd(output[0], []);
      mapping[output[0]].Add((start_a / 64, start_a / 8 % 8, start_a % 8));
    }
    List<List<(int, int, int)>> possible_codes = [.. program.Select(b => mapping[b])];
    possible_codes.Reverse();
    List<int>? solution = SolvePossibleCodes(possible_codes, null);
    solution?.Reverse();
    long result = 0;
    foreach (var code in solution!) {
      result = (result << 3) | code;
    }
    return result.ToString();
  }

  private List<int>? SolvePossibleCodes(List<List<(int, int, int)>> possible_codes, (int, int)? allowed_start) {
    if (possible_codes.Count == 0) return [];
    foreach (var (high, mid, low) in possible_codes[0]) {
      if (allowed_start is null || allowed_start.Value.Item1 == high && allowed_start.Value.Item2 == mid) {
        var tail = SolvePossibleCodes(possible_codes[1..], (mid, low));
        if (tail is null) continue;
        tail.Add(low);
        if (allowed_start is null) {
          tail.Add(mid);
          tail.Add(high);
        }
        return tail;
      }
    }
    return null;
  }
}
