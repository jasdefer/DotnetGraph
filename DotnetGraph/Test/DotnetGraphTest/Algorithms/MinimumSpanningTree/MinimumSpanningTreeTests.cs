using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using DotnetGraph.Algorithms.MinimumSpanningTree;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;
using DotnetGraphTest.Helper;

namespace DotnetGraphTest.Algorithms.MinimumSpanningTree;

[TestClass]
public abstract class MinimumSpanningTreeTests
{
    protected abstract IMinimumSpanningTreeAlgorithm GetAlgorithm();

    [TestMethod]
    public void SmallGraph()
    {
        var nodes = GraphLibrary.SmallWeightedUndirectedGraph();
        var algorithm = GetAlgorithm();
        var minimumSpanningTreeResult = algorithm.GetMinimumSpanningTree<WeightedUndirectedGraphNode, WeightedUndirectedGraphEdge>(nodes);
        Assert.IsNotNull(minimumSpanningTreeResult);
        Assert.AreEqual(6, minimumSpanningTreeResult.Tree.Count);
        Assert.AreEqual(39, minimumSpanningTreeResult.TotalWeight);
    }

    [TestMethod]
    public void Monkey()
    {
        var generator = new WeightedUndirectedGraphGenerator();
        var weightGenerator = new UniformNumberGenerator();
        var algorithm = GetAlgorithm();
        for (int i = 0; i < 10; i++)
        {
            var numberOfNodes = 10 + i * 100;
            var density = 4.5 / numberOfNodes;
            var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
            var result = algorithm.GetMinimumSpanningTree<WeightedUndirectedGraphNode, WeightedUndirectedGraphEdge>(nodes);
            Assert.IsNotNull(result);
            Assert.AreEqual(numberOfNodes - 1, result.Tree.Count);
        }
    }
}
