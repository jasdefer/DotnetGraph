using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.DepthFirstSearch
{
    public interface IDepthFirstSearchAlgorithm
    {
        DepthFirstSearchResult Run<TNode, TArc>(IList<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId;
    }
}