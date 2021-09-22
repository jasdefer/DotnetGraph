using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration
{
    public interface IUndirectedGraphGenerator
    {
        UndirectedGraphNode[] Generate(int numberOfNodes, double density);
    }
}