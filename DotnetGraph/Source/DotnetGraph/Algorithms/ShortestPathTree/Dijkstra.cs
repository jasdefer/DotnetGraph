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
        public Arc<T>[] GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            var args = CreateDijkstraArgs(arcs, origin);
            throw new NotImplementedException();
        }

        private DijkstraArguments CreateDijkstraArgs<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            if (arcs == null) throw new ArgumentNullException(nameof(arcs));
            if (origin == null) throw new ArgumentNullException(nameof(origin));
            var nodes = arcs.ExtractNodes().ToDictionary();
            var args = Initialization(arcs, nodes, origin);
            return args;
        }

        internal static Successor[] GetSuccessors<T>(IEnumerable<Arc<T>> arcs, Dictionary<T, int> nodes, int node)
        {
            var successors = new Dictionary<int, double>();
            foreach (var arc in arcs)
            {
                if (nodes[arc.Origin] == node)
                {
                    var successor = nodes[arc.Destination];
                    if (!successors.ContainsKey(successor))
                    {
                        successors.Add(successor, arc.Weight);
                    }
                    else if (arc.Weight < successors[successor])
                    {
                        successors[successor] = arc.Weight;
                    }
                }
            }
            return successors.Select(x => new Successor(x.Key, x.Value)).ToArray();
        }

        private DijkstraArguments Initialization<T>(IEnumerable<Arc<T>> arcs, Dictionary<T, int> nodes, T origin)
        {
            var args = new DijkstraArguments(nodes.Count, nodes[origin]);
            for (int i = 0; i < nodes.Count; i++)
            {
                args.Successors[i] = GetSuccessors(arcs, nodes, i);
            }
            return args;
        }
    }
}