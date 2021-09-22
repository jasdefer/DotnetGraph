using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.DepthFirstSearch
{
    public interface IDepthFirstSearchAlgorithm
    {
        DepthFirstSearchResult Run<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId;
    }
}