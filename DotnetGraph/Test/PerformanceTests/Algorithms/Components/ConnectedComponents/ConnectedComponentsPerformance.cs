using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.ErdosRenyi;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace PerformanceTests.Algorithms.Components.ConnectedComponents
{
    public class ConnectedComponentsPerformance
    {
        private WeightedUndirectedGraphNode[] nodes;

        [GlobalSetup]
        public void Setup()
        {
            var algorithm = new ErdosRenyiGenerator()
            {
                ConnectComponents = false
            };
            var weightGenerator = new UniformNumberGenerator();
            nodes = algorithm.Generate(5000, 0.001, weightGenerator);
        }

        [Benchmark]
        public int SimpleConnectedComponentAlgorithm()
        {
            var algorithm = new SimpleConnectedComponentAlgorithm();
            var result = algorithm.GetComponents<WeightedUndirectedGraphNode, WeightedUndirectedGraphEdge>(nodes);
            return result.NumberOfComponents;
        }
    }
}