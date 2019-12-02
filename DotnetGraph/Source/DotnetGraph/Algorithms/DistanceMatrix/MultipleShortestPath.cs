using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Helper;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.DistanceMatrix
{
    public class MultipleShortestPath : IDistanceMatrixAlgorithm
    {
        private readonly IShortestPathTreeAlgorithm shortestPathTreeAlgorithm;

        public MultipleShortestPath(IShortestPathTreeAlgorithm shortestPathTreeAlgorithm)
        {
            this.shortestPathTreeAlgorithm = shortestPathTreeAlgorithm ?? throw new ArgumentNullException(nameof(shortestPathTreeAlgorithm));
        }
        public DistanceMatrix<T> GetDistanceMatrix<T>(IEnumerable<Arc<T>> arcs)
        {
            var distances = new Dictionary<T, Dictionary<T, double>>();
            var nodes = arcs.ExtractNodes();
            foreach (var node in nodes)
            {
                var tree = shortestPathTreeAlgorithm.GetShortestPathTree(arcs, node);
                distances.Add(node, new Dictionary<T, double>());
                foreach (var destination in tree.Keys)
                {
                    distances[node].Add(destination, tree[destination].TotalWeight());
                }
            }
            var matrix = new DistanceMatrix<T>(distances);
            return matrix;
        }
    }
}