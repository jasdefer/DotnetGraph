using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using System.Collections.Generic;

namespace PerformanceTests.Algorithms.ShortestPath
{
    public class ShortestPathPerformance
    {
        private WeightedDirectedGraphNode[] baseNodes;
        private IReadOnlyList<DijkstraNode> dijkstraNodes;
        private int originNodeId;
        private int destinationNodeId;

        [GlobalSetup]
        public void Setup()
        {
            var generator = new UndirectedToDirectedGraphGenerator();
            var weightGenerator = new UniformNumberGenerator();
            baseNodes = generator.Generate(50000, 0.0000403, weightGenerator);
            originNodeId = 1;
            destinationNodeId = baseNodes.Length;
            dijkstraNodes = DijkstraAlgorithm.Convert<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes);
            DijkstraAlgorithm.ValidateInput(dijkstraNodes, originNodeId, destinationNodeId);
        }

        [Benchmark]
        public double DijkstraRaw()
        {
            var shortestPathResult = DijkstraAlgorithm.GetShortestPath(dijkstraNodes, originNodeId, destinationNodeId);
            return shortestPathResult.TotalWeight;
        }

        [Benchmark]
        public double DijkstraWithConversion()
        {
            var algorithm = new DijkstraAlgorithm();
            var shortestPathResult = algorithm.GetShortestPath<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes, originNodeId, destinationNodeId);
            return shortestPathResult.TotalWeight;
        }
    }
}