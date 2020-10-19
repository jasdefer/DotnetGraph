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

        [Params(100, 5000)]
        public int NumberOfNodes { get; set; }


        [GlobalSetup]
        public void GlobalSetup()
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