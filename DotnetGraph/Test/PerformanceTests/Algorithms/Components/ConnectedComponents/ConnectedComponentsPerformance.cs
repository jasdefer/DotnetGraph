using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;
using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;

namespace PerformanceTests.Algorithms.Components.ConnectedComponents;

public class ConnectedComponentsPerformance
{
    private UndirectedGraphNode[] nodes;

    [GlobalSetup]
    public void Setup()
    {
        var algorithm = new ErdosRenyiGenerator()
        {
            ConnectComponents = false
        };
        nodes = algorithm.Generate(5000, 0.001);
    }

    [Benchmark]
    public int SimpleConnectedComponentAlgorithm()
    {
        var algorithm = new SimpleConnectedComponentAlgorithm();
        var result = algorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
        return result.NumberOfComponents;
    }
}
