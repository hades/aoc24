namespace aoc24;

[ForDay(19)]
public class Day19 : Solver {
  private string[] designs;

  private class Node {
    public Dictionary<char, Node> Children = [];
    public bool Terminal = false;
  }

  private Node root;

  public void Presolve(string input) {
    List<string> lines = [.. input.Trim().Split("\n")];
    designs = lines[2..].ToArray();
    root = new();
    foreach (var pattern in lines[0].Split(", ")) {
      Node cur = root;
      foreach (char ch in pattern) {
        cur.Children.TryAdd(ch, new());
        cur = cur.Children[ch];
      }
      cur.Terminal = true;
    }
  }

  private long CountMatches(Node cur, Node root, string d) {
    if (d.Length == 0) return cur.Terminal ? 1 : 0;
    if (!cur.Children.TryGetValue(d[0], out var child)) return 0;
    return CountMatches(child, root, d[1..]) + (child.Terminal ? CountMatches(root, d[1..]) : 0);
  }

  private readonly Dictionary<string, long> cache = [];
  private long CountMatches(Node root, string d) {
    if (cache.TryGetValue(d, out var cached_match)) return cached_match;
    long match = CountMatches(root, root, d);
    cache[d] = match;
    return match;
  }

  public string SolveFirst() => designs.Where(d => CountMatches(root, d) > 0).Count().ToString();

  public string SolveSecond() => designs.Select(d => CountMatches(root, d)).Sum().ToString();
}
