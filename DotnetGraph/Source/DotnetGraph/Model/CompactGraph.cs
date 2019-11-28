using DotnetGraph.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Model
{
    internal class CompactGraph<T>
    {
        private readonly Dictionary<T, int> nodeMap;
        private Arc<T>[] BaseArcs;
        private readonly int[] ArcMapping;
        public T[] Nodes { get; }
        public CompactArc[] Arcs { get; }
        public int[] Successors { get; set; }
        public int[] DestinationSortedArcs { get; set; }
        public int[] Predecessors { get; set; }
        public CompactGraph(IEnumerable<Arc<T>> arcs)
        {
            BaseArcs = arcs.ToArray();
            Nodes = BaseArcs.ExtractNodes().ToArray();
            nodeMap = Nodes.ToDictionary();
            throw new NotImplementedException();
        }

        public int GetIndex(T node)
        {
            return nodeMap[node];
        }

        public (int startIndex, int endIndex) GetLeavingArcs(int node)
        {
            var startIndex = Successors[node];
            var endIndex = Successors[node + 1];
            return (startIndex, endIndex);
        }

        public Arc<T> GetArc(int index)
        {
            var baseArcIndex = ArcMapping[index];
            return BaseArcs[baseArcIndex];
        }
    }
}