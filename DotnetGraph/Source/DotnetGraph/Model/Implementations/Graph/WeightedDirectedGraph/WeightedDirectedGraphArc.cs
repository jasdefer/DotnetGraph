using DotnetGraph.Model.Properties;
using System.Diagnostics;

namespace DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph
{
    [DebuggerDisplay("Arc {Id}: to {Destination.Id}")]
    public record WeightedDirectedGraphArc(int Id, double Weight, WeightedDirectedGraphNode Destination) :
        IHasWeight,
        IHasDestination<WeightedDirectedGraphNode>,
        IHasId;
}