using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath
{
    public interface IShortestPathAlgorithm
    {
        ShortestPathResult GetShortestPath(IEnumerable<INode<IWeightedArc>> nodes,
            INode<IWeightedArc> origin,
            INode<IWeightedArc> destination);
    }
}