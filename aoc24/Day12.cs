using QuickGraph;
using QuickGraph.Algorithms.ConnectedComponents;
using QuickGraph.Algorithms.Search;
using Point = (int, int);

namespace aoc24;

[ForDay(12)]
public class Day12 : Solver {
  private string[] data;
  private int width, height;
  private Dictionary<int, long> perimeters = [];
  private Dictionary<int, long> areas = [];
  private Dictionary<int, long> sides = [];
  private int region_count;

  public void Presolve(string input) {
    data = input.Trim().Split("\n").ToArray();
    height = data.Length;
    width = data[0].Length;
    var graph_cc = MakeGraph(false);
    var cc = new ConnectedComponentsAlgorithm<Point, PointEdge>(graph_cc);
    cc.Compute();
    var graph_all = MakeGraph(true);
    Dictionary<(int Component, int Y), List<int>> x_sides = [];
    Dictionary<(int Component, int X), List<int>> y_sides = [];
    var search = new UndirectedBreadthFirstSearchAlgorithm<Point, PointEdge>(graph_all);
    search.SetRootVertex((0, 0));
    search.FinishVertex += vertex => {
      if (IsWithinBounds(vertex.Item1, vertex.Item2)) {
        int component = cc.Components[vertex];
        areas.TryAdd(component, 0L);
        areas[component] += 1;
      }
    };
    search.ExamineEdge += edge => {
      var (si, ti) = (IsWithinBounds(edge.Source), IsWithinBounds(edge.Target));
      bool border = si != ti || cc.Components[edge.Source] != cc.Components[edge.Target];
      if (si && border) {
        int component = cc.Components[edge.Source];
        perimeters.TryAdd(component, 0L);
        perimeters[component] += 1;
        if (edge.Source.Item1 == edge.Target.Item1) {
          int y = Math.Min(edge.Source.Item2, edge.Target.Item2);
          x_sides.TryAdd((component, y), []);
          x_sides[(component, y)].Add(edge.Source.Item2 > edge.Target.Item2 ? edge.Source.Item1 : -edge.Source.Item1 - 5);
        } else {
          int x = Math.Min(edge.Source.Item1, edge.Target.Item1);
          y_sides.TryAdd((component, x), []);
          y_sides[(component, x)].Add(edge.Source.Item1 > edge.Target.Item1 ? edge.Source.Item2 : -edge.Source.Item2 - 5);
        }
      }
    };
    search.Compute();
    region_count = cc.ComponentCount;
    foreach (var side_projection in x_sides) {
      side_projection.Value.Sort();
      sides.TryAdd(side_projection.Key.Component, 0);
      int last_x = int.MinValue;
      foreach (var x in side_projection.Value) {
        if (x != (last_x + 1)) sides[side_projection.Key.Component] += 1;
        last_x = x;
      }
    }
    foreach (var side_projection in y_sides) {
      side_projection.Value.Sort();
      sides.TryAdd(side_projection.Key.Component, 0);
      int last_y = int.MinValue;
      foreach (var y in side_projection.Value) {
        if (y != (last_y + 1)) sides[side_projection.Key.Component] += 1;
        last_y = y;
      }
    }
    foreach (var component in Enumerable.Range(0, region_count)) {
      if (!areas.ContainsKey(component)) continue;
    }
  }

  public string SolveFirst() =>
    Enumerable.Range(0, region_count)
      .Where(component => areas.ContainsKey(component))
      .Select(component => areas[component] * perimeters[component]).Sum().ToString();

  public string SolveSecond() =>
    Enumerable.Range(0, region_count)
      .Where(component => areas.ContainsKey(component))
      .Select(component => areas[component] * sides[component]).Sum().ToString();

  private record struct PointEdge(Point Source, Point Target) : IEdge<Point>;

  private IUndirectedGraph<Point, PointEdge> MakeGraph(bool with_edges_between_plots) =>
    new DelegateUndirectedGraph<Point, PointEdge>(GetVertices(), with_edges_between_plots ? GetAllEdges : GetEdgesWithoutBorders, false);

  private bool IsWithinBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;
  private bool IsWithinBounds(Point p) => IsWithinBounds(p.Item1, p.Item2);

  private readonly (int, int)[] directions = [(-1, 0), (0, -1), (1, 0), (0, 1)];

  private bool GetEdgesWithoutBorders(Point arg, out IEnumerable<PointEdge> result) {
    List<PointEdge> result_list = [];
    var (x, y) = arg;
    bool inside = IsWithinBounds(x, y);
    foreach (var (dx, dy) in directions) {
      var (ox, oy) = (x + dx, y + dy);
      if (!inside || !IsWithinBounds(ox, oy)) continue;
      if (data[y][x] == data[oy][ox]) result_list.Add(new(arg, (ox, oy)));
    }
    result = result_list;
    return true;
  }

  private bool GetAllEdges(Point arg, out IEnumerable<PointEdge> result) {
    List<PointEdge> result_list = [];
    var (x, y) = arg;
    foreach (var (dx, dy) in directions) {
      var (ox, oy) = (x + dx, y + dy);
      if (ox >= -1 && ox <= width && oy >= -1 && oy <= height) result_list.Add(new(arg, (ox, oy)));
    }
    result = result_list;
    return true;
  }

  private IEnumerable<(int, int)> GetVertices() => Enumerable.Range(-1, width + 2).SelectMany(x => Enumerable.Range(-1, height + 2).Select(y => (x, y)));
}
