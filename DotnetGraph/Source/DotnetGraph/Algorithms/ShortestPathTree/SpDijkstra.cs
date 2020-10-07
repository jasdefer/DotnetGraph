using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public class SpDijkstra : IShortestPathTreeAlgorithm
    {

        private struct SpDijkstraInputs<T>
        {            
            internal Dictionary<T, SpDijkstraNode<T>> Nodes;
            internal Dictionary<T, Arc<T>[]> Arcs;
            internal NodeQueue Queue;
        }
        

        public Dictionary<T, Arc<T>[]> GetShortestPathTree<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            SpDijkstraInputs<T> i = GetInput(arcs, origin);

            while (!i.Queue.IsEmpty)
            {
                var u = (SpDijkstraNode<T>)i.Queue.ExtractMinimum();
                if (!u.IsConnectedToOrigin)
                    break;
                if (i.Arcs.TryGetValue(u.NodeItem, out Arc<T>[] localOut))
                {
                    for (int j = 0; j < localOut.Length; j++)
                    {
                        SpDijkstraNode<T> destination = i.Nodes[localOut[j].Destination];
                        if (!destination.IsProcessed)
                        {
                            Relax(u, destination, localOut[j], i.Queue);
                        }

                    }
                }
                u.IsProcessed = true;
            }
            return ConvertResults(i.Nodes);
        }

        private Dictionary<T, Arc<T>[]> ConvertResults<T>(Dictionary<T, SpDijkstraNode<T>> nodes)
        {
            Dictionary<T, Arc<T>[]> result = new Dictionary<T, Arc<T>[]>();
            foreach (var item in nodes)
            {
                List<Arc<T>> path = new List<Arc<T>>();
                Arc<T> pre = item.Value.OptimalPredecessorArc;
                while (pre != null)
                {
                    path.Add(pre);
                    pre = nodes[pre.Origin].OptimalPredecessorArc;
                }
                if (path.Any())
                {
                    path.Reverse();
                    result[item.Key] = path.ToArray();
                }
            }
            return result;
        }


        private void Relax<T>(SpDijkstraNode<T> o, SpDijkstraNode<T> d, Arc<T> arc, NodeQueue q)
        {
            if (double.IsInfinity(arc.Weight))
                return;

            double stretch = o.DistanceFromOrigin + arc.Weight;
            if (d.DistanceFromOrigin > stretch)
            {
                q.UpdateNodePosition(d, stretch);
                d.OptimalPredecessorArc = arc;
            }
        }

        private SpDijkstraInputs<T> GetInput<T>(IEnumerable<Arc<T>> arcs, T origin)
        {
            //check data
            if (arcs == null || !arcs.Any())
                throw new ArgumentNullException(nameof(arcs));
            if (origin == null)
                throw new ArgumentNullException(nameof(origin));
            //convert nodes
            SpDijkstraNode<T> o = new SpDijkstraNode<T>(origin);
            HashSet<T> nodes = new HashSet<T>();
            foreach (var a in arcs)
            {
                nodes.Add(a.Origin);
                nodes.Add(a.Destination);
            }
            Dictionary<T, SpDijkstraNode<T>> dNodes = nodes.Select(n => new SpDijkstraNode<T>(n)).ToDictionary(n => n.NodeItem);
            //setup min-priority queue
            NodeQueue q = new NodeQueue(dNodes.Values);
            q.UpdateNodePosition(dNodes[origin], 0);
            //prepare arcs for quick access
            Dictionary<T, Arc<T>[]> dArcs = new Dictionary<T, Arc<T>[]>();
            foreach (var arcGroup in arcs.GroupBy(a => a.Origin))
            {
                dArcs[arcGroup.Key] = arcGroup.ToArray();
            }
            //return inputs
            return new SpDijkstraInputs<T>()
            {
                Nodes = dNodes,
                Arcs = dArcs,
                Queue = q
            };
        }
    }

    internal class SpDijkstraNode
    {
        public SpDijkstraNode()
        {
            PositionInQueue = 0;
            DistanceFromOrigin = double.PositiveInfinity;
            IsProcessed = false;
        }

        internal bool IsProcessed;
        internal int PositionInQueue;
        internal double DistanceFromOrigin;
        internal bool IsConnectedToOrigin => DistanceFromOrigin < double.PositiveInfinity;
    }

    internal class SpDijkstraNode<T> : SpDijkstraNode
    {
        internal T NodeItem;
        internal Arc<T> OptimalPredecessorArc;
        public SpDijkstraNode(T nodeItem) : base()
        {
            NodeItem = nodeItem ?? throw new ArgumentNullException(nameof(nodeItem));
            OptimalPredecessorArc = null;
        }
    }

    internal sealed class NodeQueue
    {
        private readonly List<SpDijkstraNode> _baseHeap;

        internal NodeQueue(IEnumerable<SpDijkstraNode> initialNodes)
        {
            _baseHeap = new List<SpDijkstraNode>();
            foreach (SpDijkstraNode n in initialNodes)
            {
                Enqeue(n);
            }
        }

        internal NodeQueue() : this(Enumerable.Empty<SpDijkstraNode>()) { }

        internal bool IsEmpty
        {
            get { return _baseHeap.Count == 0; }
        }

        private int LastItem
        {
            get
            {
                int count = _baseHeap.Count;
                return (count > 0) ? count - 1 : 0;
            }
        }

        private int FirstItem
        {
            get { return 0; }
        }

        private SpDijkstraNode HeapMinimum
        {
            get
            {
                return _baseHeap[0];
            }
        }

        internal SpDijkstraNode ExtractMinimum()
        {
            if (_baseHeap.Count < 1) throw new Exception("heap underflow");

            var min = HeapMinimum;
            //rebuild heap
            ExchangeElements(FirstItem, LastItem);
            _baseHeap.RemoveAt(LastItem);
            Heapify(FirstItem);
            return min;
        }

        internal void Enqeue(SpDijkstraNode node)
        {
            _baseHeap.Add(node);
            SlideUpwards(LastItem);
        }

        internal void UpdateNodePosition(SpDijkstraNode node, double updatedWeight)
        {
            if (!IsLess(updatedWeight, node.DistanceFromOrigin))
                throw new Exception("New Distance must be smaller than distance flag when updating nodes!");

            node.DistanceFromOrigin = updatedWeight;
            int pos = node.PositionInQueue;
            //restore heap structure (lower priority nodes slide upwards)
            SlideUpwards(pos);
        }

        internal void Clear()
        {
            _baseHeap.Clear();
        }

        private bool IsLess(double w1, double w2)
        {
            return (w1 < w2);
        }

        private void ExchangeElements(int pos1, int pos2)
        {
            //switch
            var v = _baseHeap[pos1];
            _baseHeap[pos1] = _baseHeap[pos2];
            _baseHeap[pos2] = v;

            _baseHeap[pos1].PositionInQueue = pos1;
            _baseHeap[pos2].PositionInQueue = pos2;
        }

        private void SlideUpwards(int pos)
        {
            while (pos > 0 && !ParentChildProperty(pos))
            {
                ExchangeElements(pos, GetParent(pos));
                pos = GetParent(pos);
            }
        }

        private bool ParentChildProperty(int pos)
        {
            return IsLess(_baseHeap[GetParent(pos)].DistanceFromOrigin, _baseHeap[pos].DistanceFromOrigin);
        }

        private void Heapify(int pos)
        {
            //check if left or right child of Vertex are smaller, 
            //the smallest of both changes position with parent 
            //which now needs to be reexamined with its new children

            if (pos > LastItem || pos < FirstItem) return;
            int smallest;
            int left = GetLeftChild(pos);
            int right = GetRightChild(pos);

            if (left <= LastItem && IsLess(
                _baseHeap[left].DistanceFromOrigin,
                _baseHeap[pos].DistanceFromOrigin))
            {
                smallest = left;
            }
            else
            {
                smallest = pos;
            }

            if (right <= LastItem && IsLess(
                _baseHeap[right].DistanceFromOrigin,
                _baseHeap[smallest].DistanceFromOrigin))
            {
                smallest = right;
            }

            if (smallest != pos)
            {
                ExchangeElements(pos, smallest);
                Heapify(smallest);
            }

        }

        private int GetParent(int pos)
        {
            if (pos == FirstItem) return 0;
            return (int)(Convert.ToDouble(pos - 1) / 2); //cast simply drops decimals
        }

        private int GetLeftChild(int pos)
        {
            return (2 * pos + 1);
        }

        private int GetRightChild(int pos)
        {
            return (2 * pos + 2);
        }

    }

}
