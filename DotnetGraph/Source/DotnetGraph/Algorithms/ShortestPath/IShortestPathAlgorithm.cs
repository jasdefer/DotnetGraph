namespace DotnetGraph.Algorithms.ShortestPath;

public interface IShortestPathAlgorithm
{
    ShortestPathResult<TArc> GetShortestPath<TNode, TArc>(IReadOnlyList<TNode> nodes,
        int originNodeId,
        int destinationNodeId)
        where TNode : IHasOutgoingArcs<TArc>, IHasId
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId;
}
