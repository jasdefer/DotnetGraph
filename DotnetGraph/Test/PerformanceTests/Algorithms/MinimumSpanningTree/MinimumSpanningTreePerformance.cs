using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.ErdosRenyi;
using DotnetGraph.Algorithms.MinimumSpanningTree.Kruskal;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace PerformanceTests.Algorithms.MinimumSpanningTree
{
    public class MinimumSpanningTreePerformance
    {
        private WeightedUndirectedGraphNode[] nodes;

        [GlobalSetup]
        public void Setup()
        {
            var generator = new ErdosRenyiGenerator();
            var weightGenerator = new UniformWeightGenerator();
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
}