using System.Collections.Immutable;

namespace aoc24;

[ForDay(8)]
public class Day08 : Solver
{
  private ImmutableArray<string> data;
  private int width, height;

  public void Presolve(string input) {
    data = input.Trim().Split("\n").ToImmutableArray();
    width = data[0].Length;
    height = data.Length;
  }

  public string SolveFirst() {
    Dictionary<char, List<(int, int)>> antennae = [];
    HashSet<(int, int)> antinodes = [];
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if ('.' == data[j][i]) continue;
        antennae.TryAdd(data[j][i], []);
        foreach (var (oi, oj) in antennae[data[j][i]]) {
          int di = i - oi;
          int dj = j - oj;
          int ai = i + di;
          int aj = j + dj;
          if (ai >= 0 && aj >= 0 && ai < width && aj < height) {
            antinodes.Add((ai, aj));
          }
          ai = oi - di;
          aj = oj - dj;
          if (ai >= 0 && aj >= 0 && ai < width && aj < height) {
            antinodes.Add((ai, aj));
          }
        }
        antennae[data[j][i]].Add((i, j));
      }
    }
    return antinodes.Count.ToString();
  }

  public string SolveSecond() {
    Dictionary<char, List<(int, int)>> antennae = [];
    HashSet<(int, int)> antinodes = [];
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if ('.' == data[j][i]) continue;
        antennae.TryAdd(data[j][i], []);
        foreach (var (oi, oj) in antennae[data[j][i]]) {
          int di = i - oi;
          int dj = j - oj;
          for (int ai = i, aj = j;
               ai >= 0 && aj >= 0 && ai < width && aj < height; 
               ai += di, aj +=dj) {
            antinodes.Add((ai, aj));
          }
          for (int ai = oi, aj = oj;
               ai >= 0 && aj >= 0 && ai < width && aj < height; 
               ai -= di, aj -=dj) {
            antinodes.Add((ai, aj));
          }
        }
        antennae[data[j][i]].Add((i, j));
      }
    }
    return antinodes.Count.ToString();
  }
}