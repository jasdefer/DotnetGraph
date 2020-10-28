using System;
using System.Collections.ObjectModel;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public class ShortestPathTreeResult<TNode, TArc>
    {
        public ShortestPathTreeResult(TNode origin, ReadOnlyDictionary<int, ReadOnlyCollection<TArc>> shortestPaths)
        {
            Origin = origin;
            ShortestPaths = shortestPaths ?? throw new ArgumentNullException(nameof(shortestPaths));
        }

        /// <summary>
        /// The origin for all shortest paths
        /// </summary>
        public TNode Origin { get; }

        /// <summary>
        /// Key: Id of the destination node, Value: Collection of arcs from the origin to the destination
        /// </summary>
        public ReadOnlyDictionary<int, ReadOnlyCollection<TArc>> ShortestPaths { get; }

    }
}