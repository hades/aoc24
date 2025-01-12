using System.Collections.Immutable;

namespace aoc24;

[ForDay(23)]
public class Day23 : Solver {
  HashSet<string> vertices = [];
  Dictionary<string, List<string>> connections = [];

  public void Presolve(string input) {
    var pairs = input.Trim().Split("\n").Select(line => line.Split("-")).ToArray();
    foreach (var pair in pairs) {
      vertices.Add(pair[0]);
      vertices.Add(pair[1]);
      connections.TryAdd(pair[0], []);
      connections[pair[0]].Add(pair[1]);
      connections.TryAdd(pair[1], []);
      connections[pair[1]].Add(pair[0]);
    }
  }

  public string SolveFirst() => vertices
    .SelectMany(v1 => vertices.Where(v2 => String.Compare(v2, v1) > 0 && connections[v2].Contains(v1)).Select(v2 => (v1, v2)))
    .SelectMany(pair => vertices.Where(v3 => String.Compare(v3, pair.v2) > 0 && connections[v3].Contains(pair.v1) && connections[v3].Contains(pair.v2)).Select(v3 => (pair.v1, pair.v2, v3)))
    .Where(triplet => triplet.v1.StartsWith('t') || triplet.v2.StartsWith('t') || triplet.v3.StartsWith('t'))
    .Count().ToString();

  public string SolveSecond() {
    List<ImmutableSortedSet<string>> maximal_cliques = [];
    BronKerbosch([], [.. vertices], [], maximal_cliques);
    return String.Join(',', maximal_cliques.MaxBy(clique => clique.Count));
  }

  private void BronKerbosch(
    ImmutableSortedSet<string> current_clique,
    ImmutableSortedSet<string> vertices_to_add,
    ImmutableSortedSet<string> vertices_to_ignore,
    List<ImmutableSortedSet<string>> result) {
    if (vertices_to_add.Count == 0 && vertices_to_ignore.Count == 0) result.Add(current_clique);
    foreach (var v in vertices_to_add) {
      var nset = connections[v];
      BronKerbosch(current_clique.Add(v), vertices_to_add.Intersect(nset), vertices_to_ignore.Intersect(nset), result);
      vertices_to_add = vertices_to_add.Remove(v);
      vertices_to_ignore = vertices_to_ignore.Add(v);
    }
  }
}