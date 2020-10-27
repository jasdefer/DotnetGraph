using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.LineGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace PerformanceTests.Algorithms.GraphGeneration
{
    public class WeightedDirectedGraphGenerationPerformance
    {
        private WeightedUndirectedGraphNode[] weightedUndirectedGraphNodes;
        public const int SmallNumberOfNodes = 100;
        public const int BigNumberOfNodes = 5000;

        [Params(SmallNumberOfNodes, BigNumberOfNodes)]
        public int NumberOfNodes { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            var generator = new LineGraphGenerator();
            var density = 5d / (NumberOfNodes + 1);
            var weightGenerator = new UniformWeightGenerator();
            weightedUndirectedGraphNodes = generator.Generate(NumberOfNodes, density, weightGenerator);
        }

        [Benchmark]
        public int UndirectedToDirectedGraphConversion()
        {
            var nodes = UndirectedToDirectedGraphGenerator.Convert(weightedUndirectedGraphNodes);
            return nodes.Length;
        }
    }
}