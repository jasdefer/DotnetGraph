using DotnetGraph.Model.Implementations.Graph.DiscoverableDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.DiscoverableDirectedGraphGeneration
{
    public interface IDiscoverableDirectedGraphGenerator
    {
        DiscoverableDirectedGraphNode[] Generate(int numberOfNodes, double density);
    }
}