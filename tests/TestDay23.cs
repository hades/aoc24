using aoc24;

namespace tests;
public class TestDay23
{
  private const string example1 = @"kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn
";

  [Theory]
  [InlineData(example1, "7")]
  public void TestFirstPart(string data, string answer)
  {
    var solver = new Day23();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveFirst());
  }

  [Theory]
  [InlineData(example1, "co,de,ka,ta")]
  public void TestSecondPart(string data, string answer)
  {
    var solver = new Day23();
    solver.Presolve(data.Replace("\r\n", "\n"));
    Assert.Equal(answer, solver.SolveSecond());
  }
}
