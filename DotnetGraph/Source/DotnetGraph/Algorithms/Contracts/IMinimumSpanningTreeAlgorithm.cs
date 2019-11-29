using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts
{
    public interface IMinimumSpanningTreeAlgorithm
    {
        Edge<T>[] GetMinimumSpanningTree<T>(IEnumerable<Edge<T>> arcs);
    }
}