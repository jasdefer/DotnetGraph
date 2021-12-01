using System.Diagnostics;

namespace DotnetGraph.Model.Implementations.Graph.UndirectedGraph;

[DebuggerDisplay("Edge {Id}: {Node1.Id} - {Node2.Id}")]
public record UndirectedGraphEdge(int Id, UndirectedGraphNode Node1, UndirectedGraphNode Node2) :
    IHasId,
    IConnectsNodes<UndirectedGraphNode>;
