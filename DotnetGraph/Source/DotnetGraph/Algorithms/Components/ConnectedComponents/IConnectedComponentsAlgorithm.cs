namespace DotnetGraph.Algorithms.Components.ConnectedComponents;

public interface IConnectedComponentsAlgorithm
{
    ConnectedComponentResult<TNode> GetComponents<TNode, TEdge>(IEnumerable<TNode> nodes)
        where TNode : IHasId, IHasEdges<TEdge>
        where TEdge : IConnectsNodes<TNode>, IHasId;
}
