using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;

namespace PerformanceTests.Algorithms.GraphGeneration
{
    public class LineGraphGeneratorPerformance
    {
        public const int SmallNumberOfNodes = 100;
        public const int BigNumberOfNodes = 5000;

        [Params(SmallNumberOfNodes, BigNumberOfNodes)]
        public int NumberOfNodes { get; set; }

        [Benchmark]
        public int CreateGraph()
        {
            var generator = new LineGraphGenerator();
            var density = 5d / (NumberOfNodes + 1);
            var undirectedGraphNodes = generator.Generate(NumberOfNodes, density);
            return undirectedGraphNodes.Length;
        }
    }
}