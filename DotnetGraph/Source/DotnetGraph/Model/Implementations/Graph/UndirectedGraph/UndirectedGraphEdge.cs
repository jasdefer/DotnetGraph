using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.UndirectedGraph
{
    public record UndirectedGraphEdge(int Id, UndirectedGraphNode Node1, UndirectedGraphNode Node2) :
        IHasId,
        IConnectsNodes<UndirectedGraphNode>;
}