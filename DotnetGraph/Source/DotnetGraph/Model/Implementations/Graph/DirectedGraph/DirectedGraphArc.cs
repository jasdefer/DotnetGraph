using System.Diagnostics;

namespace DotnetGraph.Model.Implementations.Graph.DirectedGraph;

[DebuggerDisplay("Arc {Id}: to {Destination.Id}")]
public record DirectedGraphArc(int Id, DirectedGraphNode Destination) :
    IHasDestination<DirectedGraphNode>,
    IHasId;
