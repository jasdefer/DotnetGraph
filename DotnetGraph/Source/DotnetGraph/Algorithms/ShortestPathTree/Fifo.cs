using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public class Fifo : IShortestPathTreeAlgorithm
    {
        public Dictionary<T, Arc<T>[]> GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            var args = new FifoArguments<T>(arcs, origin);
            while (args.Queue.Count > 0)
            {
                var node = args.Queue.Dequeue();
                (var startIndex, var endIndex) = args.Graph.GetLeavingArcs(node);
                for (int i = startIndex; i < endIndex; i++)
                {
                    var arc = args.Graph.Arcs[i];
                    var newDistance = args.BestDistances[node] + arc.Weight;
                    var isShorter = newDistance < args.BestDistances[arc.Destination];
                    if (isShorter)
                    {
                        args.BestDistances[arc.Destination] = newDistance;
                        args.BestArrivingArc[arc.Destination] = i;
                        if (!args.Queue.Contains(arc.Destination))
                        {
                            args.Queue.Enqueue(arc.Destination);
                        }
                    }
                }
            }
            var shortestPathTree = ExtractShortestPathTree(args);
            return shortestPathTree;
        }

        /// <summary>
        /// Convert the result of the Dijkstra algorithm to a shortest path tree.
        /// </summary>
        /// <returns>Returns a dictionary with a list of arcs as the shortest path (value) for a given destination node (key).</returns>
        private static Dictionary<T, Arc<T>[]> ExtractShortestPathTree<T>(FifoArguments<T> args)
        {
            var dict = new Dictionary<T, Arc<T>[]>();
            for (int i = 0; i < args.BestArrivingArc.Length; i++)
            {
                if (i != args.Origin && args.BestArrivingArc[i].HasValue)
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
    }
}