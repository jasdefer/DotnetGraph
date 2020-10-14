using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectGraphGeneration.ErdosRenyi;

namespace PerformanceTests.Algorithms.GraphGeneration
{
    public class WeightedDirectedGraphGenerationPerformance
    {
        [Params(10, 1000, 10000)]
        public int NumberOfNodes { get; set; }

        [Benchmark]
        public int ErdosRenyi()
        {
            var algorithm = new ErdosRenyiGenerator();
            var weightGenerator = new UniformWeightGenerator();
            var density = 5d / (NumberOfNodes + 1);
            var nodes = algorithm.Generate(NumberOfNodes, density, weightGenerator);
            return nodes.Length;
        }
    }
}