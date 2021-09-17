using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.DirectedGraph
{
    public record DirectedGraphArc(int Id, DirectedGraphNode Destination) :
        IHasDestination<DirectedGraphNode>,
        IHasId;
}