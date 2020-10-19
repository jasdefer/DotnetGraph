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
            var shortestPathSummary = BenchmarkRunner.Run<ShortestPathPerformance>();
            var graphGenerationSummary = BenchmarkRunner.Run<WeightedUndirectedGraphGenerationPerformance>();
            var componentsSummary = BenchmarkRunner.Run<ConnectedComponentsPerformance>();
            var stronglyConnectedComponentsSummary = BenchmarkRunner.Run<StronglyConnectedComponentsPerformance>();
            var minimumSpanningTreePeformance = BenchmarkRunner.Run<MinimumSpanningTreePerformance>();
        }
    }
}