using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;

namespace aoc24;

[ForDay(16)]
public class Day16 : Solver {
  private string[] data;
  private int width, height;
  private int start_x, start_y;
  private int end_x, end_y;

  private readonly (int, int)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];
  private record class Edge((int, int, int) Source, (int, int, int) Target) : IEdge<(int, int, int)>;

  private DelegateVertexAndEdgeListGraph<(int, int, int), Edge> graph;
  private AStarShortestPathAlgorithm<(int, int, int), Edge> search;

  private long min_distance;
  private List<(int, int, int)> min_distance_targets;

  public void Presolve(string input) {
    data = input.Trim().Split("\n");
    width = data[0].Length;
    height = data.Length;
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] == 'S') {
          start_x = i;
          start_y = j;
        } else if (data[j][i] == 'E') {
          end_x = i;
          end_y = j;
        }
      }
    }
    graph = MakeGraph();
    var start = (start_x, start_y, 0);
    search = new AStarShortestPathAlgorithm<(int, int, int), Edge>(
      graph,
      edge => edge.Source.Item3 == edge.Target.Item3 ? 1 : 1000,
      vertex => Math.Abs(vertex.Item1 - start_x) + Math.Abs(vertex.Item2 - start_y) + 1000 *
          Math.Min(vertex.Item3, 4 - vertex.Item3)
      );
    Dictionary<(int, int, int), long> distances = [];
    search.SetRootVertex(start);
    search.ExamineVertex += vertex => {
      if (vertex.Item1 == end_x && vertex.Item2 == end_y) {
        distances[vertex] = (long)search.Distances[vertex];
      }
    };
    search.Compute();
    min_distance = distances.Values.Min();
    min_distance_targets = distances.Keys.Where(v => distances[v] == min_distance).ToList();
  }

  private DelegateVertexAndEdgeListGraph<(int, int, int), Edge> MakeGraph() => new(GetAllVertices(), GetOutEdges);

  private bool GetOutEdges((int, int, int) arg, out IEnumerable<Edge> result_enumerable) {
    List<Edge> result = [];
    var (x, y, dir) = arg;
    result.Add(new Edge(arg, (x, y, (dir + 1) % 4)));
    result.Add(new Edge(arg, (x, y, (dir + 3) % 4)));
    var (tx, ty) = (x + directions[dir].Item1, y + directions[dir].Item2);
    if (data[ty][tx] != '#') result.Add(new Edge(arg, (tx, ty, dir)));
    result_enumerable = result;
    return true;
  }

  private IEnumerable<(int, int, int)> GetAllVertices() {
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] == '#') continue;
        yield return (i, j, 0);
        yield return (i, j, 1);
        yield return (i, j, 2);
        yield return (i, j, 3);
      }
    }
  }

  private HashSet<(int, int, int)> GetMinimumPathNodesTo((int, int, int) vertex) {
    var (x, y, dir) = vertex;
    if (x == start_x && y == start_y && dir == 0) return [vertex];
    if (!search.Distances.TryGetValue(vertex, out var distance_to_me)) return [];
    List<(int, int, int)> candidates = [
          (x, y, (dir + 1) % 4),
      (x, y, (dir + 3) % 4),
      (x - directions[dir].Item1, y - directions[dir].Item2, dir),
    ];
    HashSet<(int, int, int)> result = [vertex];
    foreach (var (cx, cy, cdir) in candidates) {
      if (!search.Distances.TryGetValue((cx, cy, cdir), out var distance_to_candidate)) continue;
      if (distance_to_candidate > distance_to_me - (dir == cdir ? 1 : 1000)) continue;
      result = result.Union(GetMinimumPathNodesTo((cx, cy, cdir))).ToHashSet();
    }
    return result;
  }

  public string SolveFirst() => min_distance.ToString();

  public string SolveSecond() => min_distance_targets
    .SelectMany(v => GetMinimumPathNodesTo(v))
    .Select(vertex => (vertex.Item1, vertex.Item2))
    .ToHashSet()
    .Count
    .ToString();
}
