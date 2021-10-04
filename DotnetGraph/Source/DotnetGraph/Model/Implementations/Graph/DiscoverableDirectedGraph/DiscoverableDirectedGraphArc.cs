using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.DiscoverableDirectedGraph
{
    public record DiscoverableDirectedGraphArc(int Id, DiscoverableDirectedGraphNode Destination) :
        IHasId,
        IHasDestination<DiscoverableDirectedGraphNode>;
}