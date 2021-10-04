using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents
{
    public interface IGetStronglyConnectedComponents
    {
        StronglyConnectedComponentsResult<TNode> GetCompontents<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId;
    }
}