using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts
{
    public interface IShortestPathTreeAlgorithm
    {
        Arc<T>[] GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin);
    }
}