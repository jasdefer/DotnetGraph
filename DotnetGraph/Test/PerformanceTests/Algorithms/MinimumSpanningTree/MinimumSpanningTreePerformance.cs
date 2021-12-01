using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using DotnetGraph.Algorithms.MinimumSpanningTree.Kruskal;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace PerformanceTests.Algorithms.MinimumSpanningTree;

public class MinimumSpanningTreePerformance
{
    private WeightedUndirectedGraphNode[] nodes;

    [GlobalSetup]
    public void Setup()
    {
        var generator = new WeightedUndirectedGraphGenerator();
        var weightGenerator = new UniformNumberGenerator();
        nodes = generator.Generate(10000, 0.004, weightGenerator);
    }

    [Benchmark]
    public double Kruskal()
    {
        var algorithm = new KruskalAlgorithm();
        var result = algorithm.GetMinimumSpanningTree<WeightedUndirectedGraphNode, WeightedUndirectedGraphEdge>(nodes);
        return result.TotalWeight;
    }
}
