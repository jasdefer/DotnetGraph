using DotnetGraph.Algorithms.MinimumSpanningTree;
using DotnetGraph.Algorithms.MinimumSpanningTree.Kruskal;

namespace DotnetGraphTest.Algorithms.MinimumSpanningTree;

[TestClass]
public class KruskalAlgorithmTests : MinimumSpanningTreeTests
{
    protected override IMinimumSpanningTreeAlgorithm GetAlgorithm()
    {
        return new KruskalAlgorithm();
    }
}
