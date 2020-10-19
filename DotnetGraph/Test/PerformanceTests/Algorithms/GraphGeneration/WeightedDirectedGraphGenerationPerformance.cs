using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph;

namespace PerformanceTests.Algorithms.GraphGeneration
{
    public class WeightedDirectedGraphGenerationPerformance
    {
        [Params(100, 5000)]
        public int NumberOfNodes { get; set; }

        [Benchmark]
        public int ErdosRenyi()
        {
            var algorithm = new UndirectedToDirectedGraphGenerator();
            var weightGenerator = new UniformWeightGenerator();
            var density = 5d / (NumberOfNodes + 1);
            var nodes = algorithm.Generate(NumberOfNodes, density, weightGenerator);
            return nodes.Length;
        }
    }
}