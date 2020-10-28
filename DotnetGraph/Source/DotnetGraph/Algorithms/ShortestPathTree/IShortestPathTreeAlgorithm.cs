using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public interface IShortestPathTreeAlgorithm
    {
        ShortestPathTreeResult<TNode, TArc> GetShortestPathTree<TNode, TArc>(IEnumerable<TNode> nodes, int origin)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasWeight, IHasDestination<TNode>;
    }
}