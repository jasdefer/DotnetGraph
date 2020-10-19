using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath
{
    public interface IShortestPathAlgorithm
    {
        ShortestPathResult<TArc> GetShortestPath<TNode, TArc>(IList<TNode> nodes,
            int originNodeId,
            int destinationNodeId)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasWeight, IHasId;
    }
}