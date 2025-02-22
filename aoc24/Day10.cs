using QuickGraph;
using QuickGraph.Algorithms.Search;
using Point = (int, int);

namespace aoc24;

[ForDay(10)]
public class Day10 : Solver {
  private int[][] data;
  private int width, height;
  private List<int> destinations_counts = [], paths_counts = [];

  private record PointEdge(Point Source, Point Target) : IEdge<Point>;

  private DelegateVertexAndEdgeListGraph<Point, PointEdge> MakeGraph() => new(AllPoints(), GetNeighbours);

  private static readonly List<Point> directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

  private bool GetNeighbours(Point from, out IEnumerable<PointEdge> result) {
    List<PointEdge> neighbours = [];
    int next_value = data[from.Item2][from.Item1] + 1;
    foreach (var (dx, dy) in directions) {
      int x = from.Item1 + dx, y = from.Item2 + dy;
      if (x < 0 || y < 0 || x >= width || y >= height) continue;
      if (data[y][x] != next_value) continue;
      neighbours.Add(new(from, (x, y)));
    }
    result = neighbours;
    return true;
  }

  private IEnumerable<Point> AllPoints() => Enumerable.Range(0, width).SelectMany(x => Enumerable.Range(0, height).Select(y => (x, y)));

  public void Presolve(string input) {
    data = input.Trim().Split("\n").Select(s => s.Select(ch => ch - '0').ToArray()).ToArray();
    width = data[0].Length;
    height = data.Length;
    var graph = MakeGraph();
    for (int i = 0; i < width; i++) {
      for (int j = 0; j < height; j++) {
        if (data[j][i] != 0) continue;
        var search = new BreadthFirstSearchAlgorithm<Point, PointEdge>(graph);
        Point start = (i, j);
        Dictionary<Point, int> paths_into = [];
        paths_into[start] = 1;
        var destinations = 0;
        var paths = 0;
        search.ExamineEdge += edge => {
          paths_into.TryAdd(edge.Target, 0);
          paths_into[edge.Target] += paths_into[edge.Source];
        };
        search.FinishVertex += vertex => {
          if (data[vertex.Item2][vertex.Item1] == 9) {
            paths += paths_into[vertex];
            destinations += 1;
          }
        };
        search.SetRootVertex(start);
        search.Compute();
        destinations_counts.Add(destinations);
        paths_counts.Add(paths);
      }
    }
  }

  public string SolveFirst() => destinations_counts.Sum().ToString();
  public string SolveSecond() => paths_counts.Sum().ToString();
}
