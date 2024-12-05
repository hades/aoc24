using QuickGraph;
using QuickGraph.Algorithms.TopologicalSort;

namespace aoc24;

[ForDay(5)]
public class Day05 : Solver
{
  private List<int[]> updates;
  private List<int[]> updates_ordered;

  public void Presolve(string input) {
    var blocks = input.Trim().Split("\n\n");
    List<(int, int)> rules = new();
    foreach (var line in blocks[0].Split("\n")) {
      var pair = line.Split('|');
      rules.Add((int.Parse(pair[0]), int.Parse(pair[1])));
    }
    updates = new();
    updates_ordered = new();
    foreach (var line in input.Trim().Split("\n\n")[1].Split("\n")) {
      var update = line.Split(',').Select(int.Parse).ToArray();
      updates.Add(update);

      var graph = new AdjacencyGraph<int, Edge<int>>();
      graph.AddVertexRange(update);
      graph.AddEdgeRange(rules
        .Where(rule => update.Contains(rule.Item1) && update.Contains(rule.Item2))
        .Select(rule => new Edge<int>(rule.Item1, rule.Item2)));
      List<int> ordered_update = [];
      new TopologicalSortAlgorithm<int, Edge<int>>(graph).Compute(ordered_update);
      updates_ordered.Add(ordered_update.ToArray());
    }
  }

  public string SolveFirst() => updates.Zip(updates_ordered)
    .Where(unordered_ordered => unordered_ordered.First.SequenceEqual(unordered_ordered.Second))
    .Select(unordered_ordered => unordered_ordered.First)
    .Select(update => update[update.Length / 2])
    .Sum().ToString();

  public string SolveSecond() => updates.Zip(updates_ordered)
    .Where(unordered_ordered => !unordered_ordered.First.SequenceEqual(unordered_ordered.Second))
    .Select(unordered_ordered => unordered_ordered.Second)
    .Select(update => update[update.Length / 2])
    .Sum().ToString();
}