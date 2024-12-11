
namespace aoc24;

[ForDay(11)]
public class Day11 : Solver
{
  private long[] data;

  private class TreeNode(TreeNode? left, TreeNode? right, long value) {
    public TreeNode? Left = left;
    public TreeNode? Right = right;
    public long Value = value;
  }

  private Dictionary<(long, int), long> generation_length_cache = [];
  private Dictionary<long, TreeNode> subtree_pointers = [];

  public void Presolve(string input) {
    data = input.Trim().Split(" ").Select(long.Parse).ToArray();
    List<TreeNode> roots = data.Select(value => new TreeNode(null, null, value)).ToList();
    List<TreeNode> last_level = roots;
    subtree_pointers = roots.GroupBy(root => root.Value)
      .ToDictionary(grouping => grouping.Key, grouping => grouping.First());
    for (int i = 0; i < 75; i++) {
      List<TreeNode> next_level = [];
      foreach (var node in last_level) {
        long[] children = Transform(node.Value).ToArray();
        node.Left = new TreeNode(null, null, children[0]);
        if (subtree_pointers.TryAdd(node.Left.Value, node.Left)) {
          next_level.Add(node.Left);
        }
        if (children.Length <= 1) continue;
        node.Right = new TreeNode(null, null, children[1]);
        if (subtree_pointers.TryAdd(node.Right.Value, node.Right)) {
          next_level.Add(node.Right);
        }
      }
      last_level = next_level;
    }
  }

  public string SolveFirst() => data.Select(value => GetGenerationLength(value, 25)).Sum().ToString();
  public string SolveSecond() => data.Select(value => GetGenerationLength(value, 75)).Sum().ToString();

  private long GetGenerationLength(long value, int generation) {
    if (generation == 0) { return 1; }
    if (generation_length_cache.TryGetValue((value, generation), out var result)) return result;
    TreeNode cur = subtree_pointers[value];
    long sum = GetGenerationLength(cur.Left.Value, generation - 1);
    if (cur.Right is not null) {
      sum += GetGenerationLength(cur.Right.Value, generation - 1);
    }
    generation_length_cache[(value, generation)] = sum;
    return sum;
  }

  private IEnumerable<long> Transform(long arg) {
    if (arg == 0) return [1];
    if (arg.ToString() is { Length: var l } str && (l % 2) == 0) {
      return [int.Parse(str[..(l / 2)]), int.Parse(str[(l / 2)..])];
    }
    return [arg * 2024];
  }
}