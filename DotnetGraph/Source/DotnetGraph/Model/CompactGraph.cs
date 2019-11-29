using DotnetGraph.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Model
{
    internal class CompactGraph<T>
    {
        private readonly Dictionary<T, int> nodeMap;
        private readonly T[] nodes;
        private readonly Arc<T>[] baseArcs;
        internal int[] Successors { get; }
        internal CompactArc[] Arcs { get; }
        internal int[] DestinationSortedArcs { get; }
        internal int[] Predecessors { get; }
        internal int NodeCount => nodes.Length;
        internal CompactGraph(IEnumerable<Arc<T>> arcs)
        {
            var arcArray = arcs.ToArray();
            nodeMap = arcArray.ExtractNodes().ToDictionary();
            nodes = nodeMap.Keys.ToArray();
            baseArcs = new Arc<T>[arcArray.Length];
            Arcs = new CompactArc[baseArcs.Length];
            Successors = new int[nodes.Length+ 1];
            Predecessors = new int[nodes.Length + 1];
            DestinationSortedArcs = new int[Arcs.Length];
            SetupBaseArcs(arcArray);
            SetupCompactArcs();
            SetupSuccessors();
            SetupDestinationSortedArcs();
            SetupPredecessors();
        }

        private void SetupPredecessors()
        {
            var arcIndex = 0;
            var arc = Arcs[DestinationSortedArcs[arcIndex]];
            for (int i = 0; i < nodes.Length; i++)
            {
                Predecessors[i] = arcIndex;
                while (arc.Destination == i && arcIndex < Arcs.Length)
                {
                    if (arcIndex < Arcs.Length - 1)
                    {
                        arc = Arcs[DestinationSortedArcs[++arcIndex]];
                    }
                    else
                    {
                        arcIndex++;
                    }

                }
            }
            Predecessors[nodes.Length] = arcIndex;
        }

        private void SetupDestinationSortedArcs()
        {
            var indices = Arcs.Select((x, i) => new KeyValuePair<CompactArc, int>(x,i))
                .OrderBy(x => x.Key.Destination)
                .Select(x => x.Value).ToList();
            for (int i = 0; i < DestinationSortedArcs.Length; i++)
            {
                DestinationSortedArcs[i] = indices[i];
            }
        }

        private void SetupBaseArcs(Arc<T>[] arcs)
        {
            var index = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];
                for (int j = 0; j < arcs.Length; j++)
                {
                    if(arcs[j].Origin.Equals(node))
                    {
                        baseArcs[index++] = arcs[j];
                    }
                }
            }
        }

        private void SetupSuccessors()
        {
            var arcIndex = 0;
            var arc = Arcs[arcIndex];
            for (int i = 0; i < nodes.Length; i++)
            {
                Successors[i] = arcIndex;
                while(arc.Origin == i && arcIndex<Arcs.Length)
                {
                    if (arcIndex < Arcs.Length - 1)
                    {
                        arc = Arcs[++arcIndex];
                    }
                    else
                    {
                        arcIndex++;
                    }
                    
                }
            }
            Successors[nodes.Length] = arcIndex;
        }

        private void SetupCompactArcs()
        {
            for (int i = 0; i < baseArcs.Length; i++)
            {
                var origin = baseArcs[i].Origin;
                var destination = baseArcs[i].Destination;
                var weight = baseArcs[i].Weight;
                Arcs[i] = new CompactArc(nodeMap[origin], nodeMap[destination], weight);
            }
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

        public (int startIndex, int endIndex) GetArrivingArcs(int node)
        {
            var startIndex = Predecessors[node];
            var endIndex = Predecessors[node + 1];
            return (startIndex, endIndex);
        }

        public int CountSuccessors(int node)
        {
            (int startIndex, int endIndex) = GetLeavingArcs(node);
            return endIndex - startIndex;
        }

        public int CountPredecessors(int node)
        {
            (int startIndex, int endIndex) = GetArrivingArcs(node);
            return endIndex - startIndex;
        }

        public Arc<T> GetArc(int index)
        {
            return baseArcs[index];
        }

        public T GetNode(int index)
        {
            return nodes[index];
        }

        public int GetNodeIndex(T node)
        {
            return nodeMap[node];
        }
    }
}