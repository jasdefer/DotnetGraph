using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public class Dijkstra : IShortestPathTreeAlgorithm
    {
        public Arc<T>[] GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            throw new NotImplementedException();
        }
    }
}