using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.KShortestPathRouting;
using DotnetGraph.Algorithms.KShortestPathRouting.Yen;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraphTest.Helper;

namespace DotnetGraphTest.Algorithms.KShortestPathRouting;

[TestClass]
public class YenAlgorithmTest : KShortestPathRoutingTest
{
    protected override IKShortestPathRoutingAlgorithm GetAlgorithm()
    {
        return new YenAlgorithm();
    }

    [TestMethod]
    public void MonkeyYen()
    {
        var algorithm = GetAlgorithm();
        var generator = new WeightedDirectedGraphGenerator();
        var weightGenerator = new UniformNumberGenerator();
        for (int i = 0; i < 10; i++)
        {
            var numberOfNodes = 10 + 100 * i;
            var density = 4d / (numberOfNodes + 1);
            var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
            var numberOfArcs = nodes.Sum(x => x.OutgoingArcs.Count);
            var k = i % 5 + 1;
            var dijkstraNodes = DijkstraAlgorithm.Convert<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
            var kShortestPaths = YenAlgorithm.GetKShortestPaths(dijkstraNodes, 1, nodes.Length - 1, k);
            kShortestPaths.Should().NotBeNull();
            kShortestPaths.Should().HaveCount(k);
            dijkstraNodes.Sum(x => x.OutgoingArcs.Count).Should().Be(numberOfArcs);
        }
    }
}
