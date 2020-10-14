using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.ErdosRenyi;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using System.Collections.Generic;

namespace PerformanceTests.Algorithms.ShortestPath
{
    public class ShortestPathPerformance
    {
        private WeightedDirectedGraphNode[] baseNodes;
        private List<DijkstraNode> dijkstraNodes;
        private int originNodeId;
        private int destinationNodeId;

        [GlobalSetup]
        public void Setup()
        {
            var generator = new ErdosRenyiGenerator();
            var weightGenerator = new UniformWeightGenerator();
            baseNodes = generator.Generate(10000, 0.0004, weightGenerator);
            originNodeId = 1;
            destinationNodeId = 2;

            dijkstraNodes = DijkstraAlgorithm.Convert<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes);
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
            var shortestPathResult = algorithm.GetShortestPath<WeightedDirectedGraphArc, WeightedDirectedGraphNode>(baseNodes, originNodeId, destinationNodeId);
            return shortestPathResult.TotalWeight;
        }
    }
}