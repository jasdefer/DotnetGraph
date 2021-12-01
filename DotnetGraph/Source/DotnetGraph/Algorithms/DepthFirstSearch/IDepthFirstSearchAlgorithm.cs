namespace DotnetGraph.Algorithms.DepthFirstSearch;

public interface IDepthFirstSearchAlgorithm
{
    void Run<TNode, TArc>(IReadOnlyList<TNode> nodes)
        where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
        where TArc : IHasId, IHasDestination<TNode>;
}
