using BenchmarkDotNet.Running;
using PerformanceTests.Algorithms.Components.ConnectedComponents;
using PerformanceTests.Algorithms.Components.StronglyConnectedComponents;
using PerformanceTests.Algorithms.GraphGeneration;
using PerformanceTests.Algorithms.MinimumSpanningTree;
using PerformanceTests.Algorithms.ShortestPath;

namespace PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunBenchmarkDotNet();
            //ProfileKruskal();
            //ProfileUndirectedToDirectedGraphConversion();
            //ProfileErdosRenyiGraphGenerator();
            //ProfileLineGraphGenerator();
            //ProfileSimpleConnectedComponentAlgorithm();
            ProfileRawTarjanAlgorithm();
            //ProfileTarjanAlgorithmWithConversion();
            //ProfileDijkstra();
        }

        private static void RunBenchmarkDotNet()
        {
            //var shortestPathSummary = BenchmarkRunner.Run<ShortestPathPerformance>();
            //var graphGenerationSummary = BenchmarkRunner.Run<WeightedUndirectedGraphGenerationPerformance>();
            //var componentsSummary = BenchmarkRunner.Run<ConnectedComponentsPerformance>();
            //var stronglyConnectedComponentsSummary = BenchmarkRunner.Run<StronglyConnectedComponentsPerformance>();
            var minimumSpanningTreePeformance = BenchmarkRunner.Run<MinimumSpanningTreePerformance>();
        }

        private static double ProfileKruskal()
        {
            var performanceTest = new MinimumSpanningTreePerformance();
            performanceTest.Setup();
            var total = 0d;
            for (int i = 0; i < 500; i++)
            {
                total += performanceTest.Kruskal();
            }
            return total;
        }

        private static double ProfileUndirectedToDirectedGraphConversion()
        {
            var performanceTest = new WeightedDirectedGraphGenerationPerformance();
            performanceTest.NumberOfNodes = WeightedDirectedGraphGenerationPerformance.BigNumberOfNodes;
            performanceTest.Setup();
            var total = 0;
            for (int i = 0; i < 5000; i++)
            {
                total += performanceTest.UndirectedToDirectedGraphConversion();
            }
            return total;
        }
        private static double ProfileLineGraphGenerator()
        {
            var performanceTest = new WeightedUndirectedGraphGenerationPerformance();
            performanceTest.NumberOfNodes = WeightedUndirectedGraphGenerationPerformance.BigNumberOfNodes;
            var total = 0;
            for (int i = 0; i < 500; i++)
            {
                total += performanceTest.LineGraph();
            }
            return total;
        }

        private static double ProfileErdosRenyiGraphGenerator()
        {
            var performanceTest = new WeightedUndirectedGraphGenerationPerformance();
            performanceTest.NumberOfNodes = WeightedUndirectedGraphGenerationPerformance.BigNumberOfNodes;
            var total = 0;
            for (int i = 0; i < 200; i++)
            {
                total += performanceTest.ErdosRenyi();
            }
            return total;
        }

        private static double ProfileDijkstra()
        {
            var performanceTest = new ShortestPathPerformance();
            performanceTest.Setup();
            var total = 0d;
            for (int i = 0; i < 5000; i++)
            {
                total += performanceTest.DijkstraRaw();
            }
            return total;
        }

        private static int ProfileSimpleConnectedComponentAlgorithm()
        {
            var performanceTest = new ConnectedComponentsPerformance();
            performanceTest.Setup();
            var total = 0;
            for (int i = 0; i < 10000; i++)
            {
                total += performanceTest.SimpleConnectedComponentAlgorithm();
            }
            return total;
        }

        private static int ProfileTarjanAlgorithmWithConversion()
        {
            var performanceTest = new StronglyConnectedComponentsPerformance();
            performanceTest.Setup();
            var total = 0;
            for (int i = 0; i < 5000; i++)
            {
                total += performanceTest.TarjanAlgorithmWithConversion();
            }
            return total;
        }

        private static int ProfileRawTarjanAlgorithm()
        {
            var performanceTest = new StronglyConnectedComponentsPerformance();
            performanceTest.Setup();
            var total = 0;
            for (int i = 0; i < 1000000; i++)
            {
                total += performanceTest.RawTarjanAlgorithm();
            }
            return total;
        }
    }
}