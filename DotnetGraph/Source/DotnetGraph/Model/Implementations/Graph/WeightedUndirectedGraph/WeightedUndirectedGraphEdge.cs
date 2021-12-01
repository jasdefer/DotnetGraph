namespace DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

public record WeightedUndirectedGraphEdge(
    int Id,
    double Weight,
    WeightedUndirectedGraphNode Node1,
    WeightedUndirectedGraphNode Node2)
        :
    IHasId,
    IHasWeight,
    IConnectsNodes<WeightedUndirectedGraphNode>;
