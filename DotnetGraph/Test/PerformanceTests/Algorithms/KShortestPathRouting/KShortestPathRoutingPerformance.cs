using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.KShortestPathRouting.Yen;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace PerformanceTests.Algorithms.KShortestPathRouting;
public class KShortestPathRoutingPerformance
{
    private const int k = 10;
    private WeightedDirectedGraphNode[] baseNodes;
    private IReadOnlyList<DijkstraNode> dijkstraNodes;
    private int originNodeId;
    private int destinationNodeId;

    [GlobalSetup]
    public void Setup()
    {
        var generator = new LineWeightedDirectedGraphGenerator();
        var weightGenerator = new UniformNumberGenerator();
        baseNodes = generator.Generate(50000, 0.0000403, weightGenerator);
        originNodeId = 1;
        destinationNodeId = baseNodes.Length;
        dijkstraNodes = DijkstraAlgorithm.Convert<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes);
        DijkstraAlgorithm.ValidateInput<DijkstraNode, DijkstraArc>(dijkstraNodes, originNodeId, destinationNodeId);
    }

    [Benchmark]
    public double YenRaw()
    {
        var result = YenAlgorithm.GetKShortestPaths(dijkstraNodes, originNodeId, destinationNodeId, k);
        return result.Length;
    }

    [Benchmark]
    public double YenWithConversion()
    {
        var algorithm = new YenAlgorithm();
        var result = algorithm.GetKShortestPaths<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes, originNodeId, destinationNodeId, k);
        return result.SetOfPaths.Count;
    }
}
