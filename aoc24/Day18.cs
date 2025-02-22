using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;

namespace aoc24;

[ForDay(18)]
public class Day18 : Solver {
  private int width = 71, height = 71, bytes = 1024;
  private HashSet<(int, int)> fallen_bytes;
  private List<(int, int)> fallen_bytes_in_order;
  private record class Edge((int, int) Source, (int, int) Target) : IEdge<(int, int)>;
  private DelegateVertexAndEdgeListGraph<(int, int), Edge> MakeGraph() => new(GetAllVertices(), GetOutEdges);

  private readonly (int, int)[] directions = [(-1, 0), (0, 1), (1, 0), (0, -1)];

  private bool GetOutEdges((int, int) arg, out IEnumerable<Edge> result_enumerable) {
    List<Edge> result = [];
    foreach (var (dx, dy) in directions) {
      var (nx, ny) = (arg.Item1 + dx, arg.Item2 + dy);
      if (nx < 0 || ny < 0 || nx >= width || ny >= height) continue;
      if (fallen_bytes.Contains((nx, ny))) continue;
      result.Add(new(arg, (nx, ny)));
    }
    result_enumerable = result;
    return true;
  }

  private IEnumerable<(int, int)> GetAllVertices() {
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        yield return (i, j);
      }
    }
  }

  public void SetTestData() {
    width = 7;
    height = 7;
    bytes = 12;
  }

  public void Presolve(string input) {
    fallen_bytes_in_order = [.. input.Trim().Split("\n")
      .Select(line => line.Split(","))
      .Select(pair => (int.Parse(pair[0]), int.Parse(pair[1])))];
    fallen_bytes = [.. fallen_bytes_in_order.Take(bytes)];
  }

  private double Solve() {
    var graph = MakeGraph();
    var search = new AStarShortestPathAlgorithm<(int, int), Edge>(graph, _ => 1, vtx => vtx.Item1 + vtx.Item2);
    search.SetRootVertex((0, 0));
    search.ExamineVertex += vertex => {
      if (vertex.Item1 == width - 1 && vertex.Item2 == width - 1) search.Abort();
    };
    search.Compute();
    return search.Distances[(width - 1, height - 1)];
  }

  public string SolveFirst() => Solve().ToString();

  public string SolveSecond() {
    foreach (var b in fallen_bytes_in_order[bytes..]) {
      fallen_bytes.Add(b);
      if (Solve() > width * height) return $"{b.Item1},{b.Item2}";
    }
    throw new Exception("solution not found");
  }
}
