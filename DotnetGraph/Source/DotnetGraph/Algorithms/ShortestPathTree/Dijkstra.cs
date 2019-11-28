﻿using DotnetGraph.Algorithms.Contracts;
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

        internal static Dictionary<T, Arc<T>[]> ExtractShortestPathTree<T>(DijkstraArguments<T> args)
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
                    dict.Add(args.Graph.Nodes[i], arcs.ToArray());
                }
            }
            return dict;
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