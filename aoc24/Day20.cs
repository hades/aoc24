using QuickGraph;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.ShortestPath;
using System.Xml.Schema;

namespace aoc24;

[ForDay(20)]
public class Day20 : Solver {
  private int width, height;
  private int end_x, end_y;
  private record class Edge((int, int) Source, (int, int) Target) : IEdge<(int, int)>;
  private DelegateVertexAndEdgeListGraph<(int, int), Edge> MakeGraph() => new(GetAllVertices(), GetOutEdges);

  private readonly (int, int)[] directions = [(-1, 0), (0, 1), (1, 0), (0, -1)];

  private bool GetOutEdges((int, int) arg, out IEnumerable<Edge> result_enumerable) {
    List<Edge> result = [];
    foreach (var (dx, dy) in directions) {
      var (nx, ny) = (arg.Item1 + dx, arg.Item2 + dy);
      if (nx < 0 || ny < 0 || nx >= width || ny >= height) continue;
      if (data[ny][nx] == '#') continue;
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

  private string[] data;
  private DelegateVertexAndEdgeListGraph<(int, int), Edge> graph;
  private Dictionary<(int, int), long> distances;

  public void Presolve(string input) {
    data = input.Trim().Split("\n");
    width = data[0].Length;
    height = data.Length;
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] == 'E') {
          end_x = i;
          end_y = j;
        }
      }
    }
    graph = MakeGraph();
    var search = new BreadthFirstSearchAlgorithm<(int, int), Edge>(graph);
    distances = new Dictionary<(int, int), long> {
      [(end_x, end_y)] = 0
    };
    search.SetRootVertex((end_x, end_y));
    search.TreeEdge += edge => {
      if (distances.ContainsKey(edge.Target)) throw new Exception();
      distances[edge.Target] = distances[edge.Source] + 1;
    };
    search.Compute();
  }

  public string Solve(int max_jump_distance) {
    int cheats = 0;
    foreach (var (x0, y0) in distances.Keys) {
      for (int x1 = x0 - 20; x1 <= x0 + 20; x1++) {
        for (int y1 = y0 - 20; y1 <= y0 + 20; y1++) {
          var jump_distance = Math.Abs(x1 - x0) + Math.Abs(y1 - y0);
          if (jump_distance > max_jump_distance) continue;
          if (!distances.TryGetValue((x1, y1), out var distance1)) continue;
          var saving = distance1 - distances[(x0, y0)] - jump_distance;
          if (saving >= 100) cheats++;
        }
      }
    }
    return cheats.ToString();
  }

  public string SolveFirst() => Solve(2);
  public string SolveSecond() => Solve(20);
}
