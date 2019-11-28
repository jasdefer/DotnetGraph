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
        internal int?[] BestArrivingArc { get; }
        internal double[] BestDistances { get; }
        public List<int> Queue { get; }
        public CompactGraph<T> Graph { get; }

        internal DijkstraArguments(CompactGraph<T> compactGraph, T origin)
        {
            Graph = compactGraph;
            var nodeCount = compactGraph.Successors.Length - 1;
            BestArrivingArc = new int?[nodeCount];
            BestDistances = new double[nodeCount];
            Queue = new List<int>();
            Origin = compactGraph.GetIndex(origin);
            for (int i = 0; i < nodeCount; i++)
            {
                BestArrivingArc[i] = null;
                BestDistances[i] = double.PositiveInfinity;
                if(i == Origin)
                {
                    BestDistances[i] = 0;
                }
                Queue.Add(i);
            }
            
        }
    }
}