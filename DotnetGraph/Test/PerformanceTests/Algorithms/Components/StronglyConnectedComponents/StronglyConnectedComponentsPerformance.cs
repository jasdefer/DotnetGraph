using BenchmarkDotNet.Attributes;
using DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.ErdosRenyi;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace PerformanceTests.Algorithms.Components.StronglyConnectedComponents
{
    public class StronglyConnectedComponentsPerformance
    {
        private WeightedDirectedGraphNode[] nodes;
        private TarjanNode[] tarjanNodes;

        [GlobalSetup]
        public void Setup()
        {
            var algorithm = new UndirectedToDirectedGraphGenerator()
            {
                WeightedUndirectedGraphGenerator = new ErdosRenyiGenerator()
            };
            var weightGenerator = new UniformNumberGenerator();
            nodes = algorithm.Generate(10000, 0.0004, weightGenerator);
            tarjanNodes = TarjanAlgorithm.Convert<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
        }

        [Benchmark]
        public int TarjanAlgorithmWithConversion()
        {
            var algorithm = new TarjanAlgorithm();
            var result = algorithm.GetCompontents<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
            return result.NumberOfComponents;
        }

        [Benchmark]
        public int RawTarjanAlgorithm()
        {
            var algorithm = new TarjanAlgorithm();
            var result = algorithm.GetComponents(tarjanNodes);
            return result.NumberOfComponents;
        }
    }
}