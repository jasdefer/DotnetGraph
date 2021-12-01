namespace DotnetGraph.Algorithms.MinimumSpanningTree
{
    public interface IMinimumSpanningTreeAlgorithm
    {
        MinimumSpanningTreeResult<TEdge> GetMinimumSpanningTree<TNode, TEdge>(IEnumerable<TNode> nodes)
            where TNode : IHasId, IHasEdges<TEdge>
            where TEdge : IConnectsNodes<TNode>, IHasId, IHasWeight;
    }
}