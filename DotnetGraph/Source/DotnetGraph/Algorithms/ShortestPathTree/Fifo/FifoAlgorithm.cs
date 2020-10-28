using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotnetGraph.Algorithms.ShortestPathTree.Fifo
{
    public class FifoAlgorithm : IShortestPathTreeAlgorithm
    {
        public ShortestPathTreeResult<TNode, TArc> GetShortestPathTree<TNode, TArc>(IEnumerable<TNode> nodes, int origin)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasWeight, IHasDestination<TNode>
        {
            FifoNode[] fifoNodes = Convert<TNode, TArc>(nodes);
            ComputeShortestPaths(fifoNodes, origin);
            var fifoPathTree = BuildShortestPathResult(fifoNodes, origin);
            var shortestPathTreeResult = Convert<TNode, TArc>(nodes, fifoPathTree);
            return shortestPathTreeResult;
        }

        public static ShortestPathTreeResult<FifoNode, FifoArc> GetShortestPathTree(FifoNode[] nodes, int originNodeId)
        {
            ComputeShortestPaths(nodes, originNodeId);
            var shortestPathTreeResult = BuildShortestPathResult(nodes, originNodeId);
            return shortestPathTreeResult;
        }

        private static ShortestPathTreeResult<TNode, TArc> Convert<TNode, TArc>(IEnumerable<TNode> nodes,
            ShortestPathTreeResult<FifoNode, FifoArc> shortestPathTreeResult)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId
        {
            var arcDict = nodes.SelectMany(x => x.OutgoingArcs).ToDictionary(x => x.Id, x => x);
            var origin = nodes.Single(x => x.Id == shortestPathTreeResult.Origin.Id);
            var tree = new Dictionary<int, ReadOnlyCollection<TArc>>();
            foreach (var path in shortestPathTreeResult.ShortestPaths)
            {
                var arcs = new List<TArc>();
                foreach (var arc in path.Value)
                {
                    arcs.Add(arcDict[arc.Id]);
                }
                tree.Add(path.Key, arcs.AsReadOnly());
            }
            var readonlyTree = new ReadOnlyDictionary<int, ReadOnlyCollection<TArc>>(tree);
            var result = new ShortestPathTreeResult<TNode, TArc>(origin, readonlyTree);
            return result;
        }

        public static FifoNode[] Convert<TNode, TArc>(IEnumerable<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasWeight, IHasDestination<TNode>
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var fifoNodes = nodes.ToDictionary(x => x.Id, x => new FifoNode(x.Id));

            foreach (var node in nodes)
            {
                var originId = node.Id;
                var origin = fifoNodes[node.Id];
                foreach (var arc in node.OutgoingArcs)
                {
                    var fifoArc = new FifoArc(arc.Id, origin, fifoNodes[arc.Destination.Id], arc.Weight);
                    origin.AddArc(fifoArc);
                }
            }
            return fifoNodes.Values.ToArray();
        }

        public static void ComputeShortestPaths(FifoNode[] nodes, int originId)
        {
            var origin = PrepareInput(nodes, originId);
            var queue = new Queue<FifoNode>();
            queue.Enqueue(origin);
            var idsInQueue = new HashSet<int>() { originId };
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                idsInQueue.Remove(node.Id);
                foreach (var arc in node.OutgoingArcs)
                {
                    var newDistance = node.DistanceFromOrigin.Value + arc.Weight;
                    var oldDistance = arc.Destination.DistanceFromOrigin;
                    if (!oldDistance.HasValue ||
                        newDistance < oldDistance.Value)
                    {
                        arc.Destination.DistanceFromOrigin = newDistance;
                        arc.Destination.BestPredecessor = arc;
                        if (!idsInQueue.Contains(arc.Destination.Id))
                        {
                            idsInQueue.Add(arc.Destination.Id);
                            queue.Enqueue(arc.Destination);
                        }
                    }
                }
            }
        }

        internal static ShortestPathTreeResult<FifoNode, FifoArc> BuildShortestPathResult(FifoNode[] nodes, int originNodeId)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var tree = new Dictionary<int, ReadOnlyCollection<FifoArc>>();
            FifoNode origin = null;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].Id == originNodeId)
                {
                    origin = nodes[i];
                }
                else
                {
                    var node = nodes[i];
                    var arcs = new List<FifoArc>();
                    while (node.Id != originNodeId &&
                        node.BestPredecessor != null)
                    {
                        arcs.Add(node.BestPredecessor);
                        node = node.BestPredecessor.Origin;
                    }
                    arcs.Reverse();
                    tree.Add(nodes[i].Id, arcs.AsReadOnly());
                }
            }
            var readonlyTree = new ReadOnlyDictionary<int, ReadOnlyCollection<FifoArc>>(tree);
            var result = new ShortestPathTreeResult<FifoNode, FifoArc>(origin, readonlyTree);
            return result;
        }

        public static FifoNode PrepareInput(FifoNode[] nodes, int originNodeId)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var originIndex = -1;
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].DistanceFromOrigin = null;
                nodes[i].BestPredecessor = null;
                if (nodes[i].Id == originNodeId)
                {
                    nodes[i].DistanceFromOrigin = 0;
                    originIndex = i;
                }
            }

            if (originIndex == -1)
            {
                throw new Exception($"Cannot find the origin node id {originNodeId}");
            }

            return nodes[originIndex];
        }
    }
}