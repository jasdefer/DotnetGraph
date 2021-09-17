using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.ErdosRenyi;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.LineGraph;

namespace PerformanceTests.Algorithms.GraphGeneration
{
    public class WeightedUndirectedGraphGenerationPerformance
    {
        public const int SmallNumberOfNodes = 20;
        public const int BigNumberOfNodes = 7500;
        [Params(SmallNumberOfNodes, BigNumberOfNodes)]
        public int NumberOfNodes { get; set; }

        [Benchmark]
        public int ErdosRenyi()
        {
            var algorithm = new ErdosRenyiGenerator();
            var weightGenerator = new UniformNumberGenerator();
            var density = 5d / (NumberOfNodes + 1);
            var nodes = algorithm.Generate(NumberOfNodes, density, weightGenerator);
            return nodes.Length;
        }

        [Benchmark]
        public int LineGraph()
        {
            var algorithm = new LineGraphGenerator();
            var weightGenerator = new UniformNumberGenerator();
            var density = 5d / (NumberOfNodes + 1);
            var nodes = algorithm.Generate(NumberOfNodes, density, weightGenerator);
            return nodes.Length;
        }
    }
}