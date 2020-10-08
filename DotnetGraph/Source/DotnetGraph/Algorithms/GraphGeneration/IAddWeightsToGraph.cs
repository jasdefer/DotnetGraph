using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.GraphGeneration
{
    public interface IAddWeightsToGraph<T> where T : IArc
    {
        List<INode<IWeightedArc>> Add(IEnumerable<INode<T>> nodes);
    }
}