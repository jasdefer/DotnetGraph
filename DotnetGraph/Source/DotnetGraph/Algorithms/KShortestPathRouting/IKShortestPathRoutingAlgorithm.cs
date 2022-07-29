namespace DotnetGraph.Algorithms.KShortestPathRouting;
public interface IKShortestPathRoutingAlgorithm
{
    KShortestPathRoutingResult<TArc> GetKShortestPaths<TNode, TArc>(IReadOnlyList<TNode> nodes,
        int originNodeId,
        int destinationNodeId,
        int k)
        where TNode : IHasOutgoingArcs<TArc>, IHasId
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId;
}
