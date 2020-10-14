using BenchmarkDotNet.Running;
using PerformanceTests.Algorithms.GraphGeneration;
using PerformanceTests.Algorithms.ShortestPath;

namespace PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var shortestPathSummary = BenchmarkRunner.Run<ShortestPathPerformance>();
            var graphGenerationSummary = BenchmarkRunner.Run<WeightedDirectedGraphGenerationPerformance>();
        }
    }
}