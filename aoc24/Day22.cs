namespace aoc24;

[ForDay(22)]
public class Day22 : Solver {
  private long[] numbers;

  public void Presolve(string input) {
    numbers = input.Trim().Split("\n").Select(long.Parse).ToArray();
  }

  private long Advance(long number) {
    number = ((number << 6) ^ number) & 0xffffff;
    number = ((number >> 5) ^ number) & 0xffffff;
    number = ((number << 11) ^ number) & 0xffffff;
    return number;
  }

  private long Advance(long number, int steps) {
    for (int i = 0; i < steps; ++i) number = Advance(number);
    return number;
  }

  public string SolveFirst() => numbers.Select(n => Advance(n, 2000)).Sum().ToString();

  public string SolveSecond() {
    Dictionary<(long, long, long, long), int> results = [];
    foreach (var number in numbers) {
      HashSet<(int, int, int, int)> seen = [];
      long current_number = Advance(number);
      List<int> change_sequence = [(int)(current_number % 10 - number % 10)];
      for (int i = 0; i < 1999; ++i) {
        long next_number = Advance(current_number);
        change_sequence.Add((int)(next_number % 10 - current_number % 10));
        if (change_sequence.Count >= 4) {
          var changes = (change_sequence[^4], change_sequence[^3], change_sequence[^2], change_sequence[^1]);
          if (!seen.Contains(changes)) {
            results.TryAdd(changes, 0);
            results[changes] += (int)(next_number % 10);
          }
          seen.Add(changes);
        }
        current_number = next_number;
      }
    }
    return results.Values.Max().ToString();
  }
}
