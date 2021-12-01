namespace DotnetGraph.Algorithms.NetworkFlow.MaxFlow
{
    public interface IMaxFlowAlgorithm
    {
        void SetFlow<TNode, TArc>(IReadOnlyList<TNode> nodes, int originNodeId, int destinationNodeId)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>, IHasCapacity, IHasFlow;
    }
}