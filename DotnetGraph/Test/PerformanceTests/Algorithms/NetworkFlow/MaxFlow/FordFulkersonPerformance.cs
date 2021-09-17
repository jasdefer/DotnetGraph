using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.NetworkFlow.MaxFlow.FordFulkerson;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;
using System.Collections.Generic;

namespace PerformanceTests.Algorithms.NetworkFlow.MaxFlow
{
    public class FordFulkersonPerformance
    {
        private FlowDirectedGraphNode[] baseNodes;
        private IReadOnlyList<FordFulkersonNode> fordFulkersonNodes;
        private int originNodeId;
        private int destinationNodeId;

        [GlobalSetup]
        public void Setup()
        {
            var generator = new LineGraphDirectedGraphGenerator();
            var weightGenerator = new UniformNumberGenerator();
            baseNodes = generator.Generate(10000, 0.00025, weightGenerator);
            originNodeId = 1;
            destinationNodeId = baseNodes.Length;
            fordFulkersonNodes = FordFulkersonAlgorithm.Convert<FlowDirectedGraphNode, FlowDirectedGraphArc>(baseNodes);
            FordFulkersonAlgorithm.ValidateInput(fordFulkersonNodes, originNodeId, destinationNodeId);
        }

        [Benchmark]
        public double FordFulkersonRaw()
        {
            FordFulkersonAlgorithm.SetFlow(fordFulkersonNodes, originNodeId, destinationNodeId);
            return fordFulkersonNodes.Count;
        }

        [Benchmark]
        public double FordFulkersonWithConversion()
        {
            var algorithm = new FordFulkersonAlgorithm();
            algorithm.SetFlow<FlowDirectedGraphNode, FlowDirectedGraphArc>(baseNodes, originNodeId, destinationNodeId);
            return baseNodes.Length;
        }
    }
}