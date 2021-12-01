using DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan;
using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;

namespace PerformanceTests.Algorithms.Components.StronglyConnectedComponents
{
    public class StronglyConnectedComponentsPerformance
    {
        private DirectedGraphNode[] nodes;
        private TarjanNode[] tarjanNodes;

        [GlobalSetup]
        public void Setup()
        {
            var algorithm = new UndirectedToDirectedGraphGenerator();
            nodes = algorithm.Generate(10000, 0.0004);
            tarjanNodes = TarjanAlgorithm.Convert<DirectedGraphNode, DirectedGraphArc>(nodes);
        }

        [Benchmark]
        public int TarjanAlgorithmWithConversion()
        {
            var algorithm = new TarjanAlgorithm();
            var result = algorithm.GetCompontents<DirectedGraphNode, DirectedGraphArc>(nodes);
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