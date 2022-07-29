global using BenchmarkDotNet.Attributes;
global using PerformanceTests.Algorithms.Components.ConnectedComponents;
global using PerformanceTests.Algorithms.Components.StronglyConnectedComponents;
global using PerformanceTests.Algorithms.GraphGeneration;
global using PerformanceTests.Algorithms.MinimumSpanningTree;
global using PerformanceTests.Algorithms.NetworkFlow.MaxFlow;
global using PerformanceTests.Algorithms.ShortestPath;
global using PerformanceTests.Algorithms.ShortestPathTree;
using PerformanceTests.Algorithms.KShortestPathRouting;

namespace PerformanceTests;

class Program
{
    static void Main(string[] args)
    {
        RunBenchmarkDotNet();
        //ProfileKruskal();
        //ProfileUndirectedToDirectedGraphConversion();
        //ProfileErdosRenyiGraphGenerator();
        //ProfileLineGraphGenerator();
        //ProfileSimpleConnectedComponentAlgorithm();
        //ProfileRawTarjanAlgorithm();
        //ProfileTarjanAlgorithmWithConversion();
        //ProfileDijkstra();
        //ProfileFifoRaw();
        //ProfileFordFulkerson();
        //ProfileYen();
    }

    private static void RunBenchmarkDotNet()
    {
        //var shortestPathSummary = BenchmarkRunner.Run<ShortestPathPerformance>();
        //var erdosRenyiGeneratorSummary = BenchmarkRunner.Run<ErdosRenyiGeneratorPerformance>();
        //var lineGraphGeneratorSumary = BenchmarkRunner.Run<LineGraphGeneratorPerformance>();
        //var componentsSummary = BenchmarkRunner.Run<ConnectedComponentsPerformance>();
        //var stronglyConnectedComponentsSummary = BenchmarkRunner.Run<StronglyConnectedComponentsPerformance>();
        //var minimumSpanningTreePeformance = BenchmarkRunner.Run<MinimumSpanningTreePerformance>();
        //var shortestPathTreePerformance = BenchmarkRunner.Run<ShortestPathTreePerformance>();
        //var forderFulkersonPerformance = BenchmarkRunner.Run<FordFulkersonPerformance>();
        //var kShortestPathRoutingPerformance = BenchmarkRunner.Run<KShortestPathRoutingPerformance>();
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

    private static double ProfileLineGraphGenerator()
    {
        var performanceTest = new LineGraphGeneratorPerformance
        {
            NumberOfNodes = LineGraphGeneratorPerformance.BigNumberOfNodes
        };
        var total = 0;
        for (int i = 0; i < 500; i++)
        {
            total += performanceTest.CreateGraph();
        }
        return total;
    }

    private static double ProfileErdosRenyiGraphGenerator()
    {
        var performanceTest = new ErdosRenyiGeneratorPerformance
        {
            NumberOfNodes = ErdosRenyiGeneratorPerformance.BigNumberOfNodes
        };
        var total = 0;
        for (int i = 0; i < 200; i++)
        {
            total += performanceTest.CreateGraph();
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

    private static double ProfileFifoRaw()
    {
        var performanceTest = new ShortestPathTreePerformance();
        performanceTest.Setup();
        var total = 0;
        for (int i = 0; i < 40; i++)
        {
            total += performanceTest.FifoRaw();
        }
        return total;
    }

    private static double ProfileFifoWithConversion()
    {
        var performanceTest = new ShortestPathTreePerformance();
        performanceTest.Setup();
        var total = 0;
        for (int i = 0; i < 20; i++)
        {
            total += performanceTest.FifoWithConversion();
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

    private static double ProfileFordFulkerson()
    {
        var performanceTest = new FordFulkersonPerformance();
        performanceTest.Setup();
        var total = 0d;
        for (int i = 0; i < 100000; i++)
        {
            //total += performanceTest.FordFulkersonWithConversion();
            total += performanceTest.FordFulkersonRaw();
        }
        return total;
    }

    private static double ProfileYen()
    {
        var performanceTest = new KShortestPathRoutingPerformance();
        performanceTest.Setup();
        var total = 0d;
        for (int i = 0; i < 250; i++)
        {
            //total += performanceTest.YenWithConversion();
            total += performanceTest.YenRaw();
        }
        return total;
    }
}
