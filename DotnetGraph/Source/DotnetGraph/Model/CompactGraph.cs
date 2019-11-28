using DotnetGraph.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Model
{
    internal class CompactGraph<T>
    {
        private readonly Dictionary<T, int> nodeMap;
        public T[] Nodes { get; }
        public Arc<T>[] BaseArcs { get; }
        public CompactArc[] Arcs { get; }
        public int[] Successors { get; set; }
        public int[] C { get; set; }
        public int[] Predecessors { get; set; }
        public CompactGraph(IEnumerable<Arc<T>> arcs)
        {
            BaseArcs = arcs.ToArray();
            Nodes = BaseArcs.ExtractNodes().ToArray();
            throw new NotImplementedException();
        }

        public int GetIndex(T node)
        {
            return nodeMap[node];
        }

        public CompactArc[] GetLeavingArcs(int node)
        {
            throw new NotImplementedException();
        }

        internal Arc<T> GetShortestArc(int? v, int i)
        {
            throw new NotImplementedException();
        }
    }
}