using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts
{
    public interface IShortestPathAlgorithm
    {
        Arc<T>[] GetShortestPath<T>(IEnumerable<Arc<T>> arcs, T origin, T destination);
    }
}