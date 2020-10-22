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
            RunBenchmarkDotNet();
        }

        private static void ProfileDijkstra()
        {
            var performanceTest = new ShortestPathPerformance();
            performanceTest.Setup();
            var total = 0d;
            for (int i = 0; i < 5000; i++)
            {
                total += performanceTest.DijkstraRaw();
            }
        }

        private static void RunBenchmarkDotNet()
        {
            var shortestPathSummary = BenchmarkRunner.Run<ShortestPathPerformance>();
            //var graphGenerationSummary = BenchmarkRunner.Run<WeightedUndirectedGraphGenerationPerformance>();
            //var componentsSummary = BenchmarkRunner.Run<ConnectedComponentsPerformance>();
            //var stronglyConnectedComponentsSummary = BenchmarkRunner.Run<StronglyConnectedComponentsPerformance>();
            //var minimumSpanningTreePeformance = BenchmarkRunner.Run<MinimumSpanningTreePerformance>();
        }
    }
}