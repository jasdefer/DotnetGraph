using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph
{
    public record WeightedDirectedGraphArc(int Id, double Weight, WeightedDirectedGraphNode Destination) :
        IHasWeight,
        IHasDestination<WeightedDirectedGraphNode>,
        IHasId;
}