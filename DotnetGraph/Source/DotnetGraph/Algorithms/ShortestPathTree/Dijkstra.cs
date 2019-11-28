using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public class Dijkstra : IShortestPathTreeAlgorithm
    {
        /// <summary>
        /// Create the shortest path tree from an origin to all other reachable nodes in a graph.
        /// Cannot handle arcs with negative edges.
        /// </summary>
        /// <typeparam name="T">The type of the nodes in the graph.</typeparam>
        /// <param name="arcs">The collection of arcs in the graph.</param>
        /// <param name="origin">The origin node of the shortest path tree.</param>
        /// <returns>Returns a dictionary with a list of arcs as the shortest path (value) for a given destination node (key).</returns>
        public Dictionary<T, Arc<T>[]> GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            var args = Initialization(arcs, origin);
            var iteration = 0;
            while (args.Queue.Count > 0)
            {
                iteration++;
                var node = GetIndexOfMin(args.BestDistances, args.Queue);
                args.Queue.Remove(node);
                (var startIndex, var endIndex) = args.Graph.GetLeavingArcs(node);
                for (int i = startIndex; i < endIndex; i++)
                {
                    var successor = args.Graph.Arcs[i].Destination;
                    var distanceToSuccessor = args.BestDistances[node] + args.Graph.Arcs[i].Weight;
                    var improvesDistance = distanceToSuccessor < args.BestDistances[successor];
                    if (improvesDistance)
                    {
                        args.BestDistances[successor] = distanceToSuccessor;
                        args.BestArrivingArc[successor] = i;
                    }
                }
            }
            var shortestPathTree = ExtractShortestPathTree(args);
            return shortestPathTree;
        }

        /// <summary>
        /// Get the node in the queue with the lowest distance.
        /// </summary>
        private static int GetIndexOfMin(double[] distances, List<int> queue)
        {
            double min = double.PositiveInfinity;
            var index = -1;
            foreach (var node in queue)
            {
                if (distances[node] < min)
                {
                    min = distances[node];
                    index = node;
                }
            }
            return index;
        }

        /// <summary>
        /// Convert the result of the Dijkstra algorithm to a shortest path tree.
        /// </summary>
        /// <returns>Returns a dictionary with a list of arcs as the shortest path (value) for a given destination node (key).</returns>
        private static Dictionary<T, Arc<T>[]> ExtractShortestPathTree<T>(DijkstraArguments<T> args)
        {
            var dict = new Dictionary<T, Arc<T>[]>();
            for (int i = 0; i < args.BestArrivingArc.Length; i++)
            {
                if (i != args.Origin &&args.BestArrivingArc[i].HasValue)
                {
                    var arcs = new List<Arc<T>>();
                    int previous = i;
                    while (previous != args.Origin)
                    {
                        var arcIndex = args.BestArrivingArc[previous].Value;
                        previous = args.Graph.Arcs[arcIndex].Origin;
                        var arc = args.Graph.GetArc(arcIndex);
                        arcs.Add(arc);
                    }
                    arcs.Reverse();
                    dict.Add(args.Graph.GetNode(i), arcs.ToArray());
                }
            }
            return dict;
        }

        /// <summary>
        /// Initialize the Dijkstra algorithm. Validate the input and create the compact graph with the Dijkstra initialization.
        /// </summary>
        private DijkstraArguments<T> Initialization<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            if (arcs == null) throw new ArgumentNullException(nameof(arcs));
            if (origin == null) throw new ArgumentNullException(nameof(origin));
            if (arcs.Any(x => x.Weight < 0)) throw new Exception("Dijkstra's algorithm cannot handle negative weights.");
            var compactGraph = new CompactGraph<T>(arcs);
            var args = new DijkstraArguments<T>(compactGraph, origin);
            return args;
        }
    }
}