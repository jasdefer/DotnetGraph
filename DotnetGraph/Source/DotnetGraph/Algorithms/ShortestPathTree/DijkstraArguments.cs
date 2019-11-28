using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    /// <summary>
    /// All information of the graph are stored here in a way suitable for Dijkstra's algorithm.
    /// </summary>
    internal class DijkstraArguments<T>
    {
        internal int Origin { get; }
        internal int?[] BestPredecessors { get; }
        internal double[] BestDistances { get; }
        public List<int> Queue { get; }
        public CompactGraph<T> Graph { get; }

        internal DijkstraArguments(CompactGraph<T> compactGraph, T origin)
        {
            var nodeCount = compactGraph.Successors.Length - 1;
            BestPredecessors = new int?[nodeCount];
            BestDistances = new double[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                BestPredecessors[i] = null;
                BestDistances[i] = double.PositiveInfinity;
                Queue.Add(i);
            }
            Origin = compactGraph.GetIndex(origin);
        }
    }
}