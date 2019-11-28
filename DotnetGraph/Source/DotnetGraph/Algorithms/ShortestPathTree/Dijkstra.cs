using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Helper;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public class Dijkstra : IShortestPathTreeAlgorithm
    {
        public Dictionary<T, Arc<T>[]> GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            var args = Initialization(arcs, origin);
            while (args.Queue.Count > 0)
            {
                var node = args.BestDistances.GetIndexOfMin();
                args.Queue.Remove(node);
                var leavingArcs = args.Graph.GetLeavingArcs(node);
                for (int i = 0; i < leavingArcs.Length; i++)
                {
                    var successor = leavingArcs[i].Destination;
                    var distanceToSuccessor = args.BestDistances[node] + leavingArcs[i].Weight;
                    var improvesDistance = distanceToSuccessor < args.BestDistances[successor];
                    if (improvesDistance)
                    {
                        args.BestDistances[successor] = distanceToSuccessor;
                        args.BestPredecessors[successor] = node;
                    }
                }
            }
            var shortestPathTree = ExtractShortestPathTree(args);
            return shortestPathTree;
        }

        internal static Dictionary<T, Arc<T>[]> ExtractShortestPathTree<T>(DijkstraArguments<T> args)
        {
            var bestArcs = new List<Arc<T>>();
            for (int i = 0; i < args.BestPredecessors.Length; i++)
            {
                if (args.BestPredecessors[i].HasValue)
                {
                    Arc<T> arc = args.Graph.GetShortestArc(args.BestPredecessors[i], i);
                    bestArcs.Add(arc);
                }
            }
            return bestArcs.GetShortestPathTree();
        }

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