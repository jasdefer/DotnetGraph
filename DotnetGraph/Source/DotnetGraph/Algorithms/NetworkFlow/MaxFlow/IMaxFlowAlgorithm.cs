using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.NetworkFlow.MaxFlow
{
    public interface IMaxFlowAlgorithm
    {
        void SetFlow<TNode, TArc>(IList<TNode> nodes, int originNodeId, int destinationNodeId)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>, IHasCapacity, IHasFlow;
    }
}