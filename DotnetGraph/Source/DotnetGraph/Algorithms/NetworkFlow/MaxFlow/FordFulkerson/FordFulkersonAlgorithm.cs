using DotnetGraph.Helper;
using DotnetGraph.Helper.Exceptions;
using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.NetworkFlow.MaxFlow.FordFulkerson
{
    public class FordFulkersonAlgorithm : IMaxFlowAlgorithm
    {
        public void SetFlow<TNode, TArc>(IReadOnlyList<TNode> nodes, int originNodeId, int destinationNodeId)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>, IHasCapacity, IHasFlow
        {
            var fordFulkersonNodes = Convert<TNode, TArc>(nodes);
            // Remove antiparallel arcs
            SetFlow(fordFulkersonNodes, originNodeId, destinationNodeId);

            // Copy the resulting flows to the arcs of the given input nodes
            var dict = fordFulkersonNodes.SelectMany(x => x.OutgoingArcs).ToDictionary(x => x.Id, x => x.Flow);
            for (int i = 0; i < nodes.Count; i++)
            {
                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    arc.Flow = dict[arc.Id];
                }
            }
        }

        #region SetFlow

        public static void SetFlow(IReadOnlyList<FordFulkersonNode> nodes, int originNodeId, int destinationNodeId)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }
            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    arc.Flow = 0;
                }
            }

            var augmentingPath = GetAugmentingPath(nodes, originNodeId, destinationNodeId);
            if (augmentingPath == null)
            {
                throw new InvalidDestinationException($"There is no flow possible between the origin {originNodeId} to {destinationNodeId}.");
            }
            while (augmentingPath != null)
            {
                var minResidualCapacity = GetMinimalResidualCapacity(augmentingPath, originNodeId);
                UpdateFlow(augmentingPath, originNodeId, minResidualCapacity);
                augmentingPath = GetAugmentingPath(nodes, originNodeId, destinationNodeId);
            }
        }

        private static void UpdateFlow(List<FordFulkersonArc> path, int originNodeId, double minResidualCapacity)
        {
            var id = originNodeId;
            foreach (var arc in path)
            {
                if (arc.Origin.Id == id)
                {
                    arc.Flow += minResidualCapacity;
                    id = arc.Destination.Id;
                }
                else if (arc.Destination.Id == id)
                {
                    arc.Flow -= minResidualCapacity;
                    id = arc.Origin.Id;
                }
            }
        }

        private static double GetMinimalResidualCapacity(List<FordFulkersonArc> path, int originNodeId)
        {
            var id = originNodeId;
            var min = (double?)null;
            foreach (var arc in path)
            {
                //Calculate the residual capacity which is based on the direction of the arc
                double residualCapacity;
                if (arc.Origin.Id == id)
                {
                    residualCapacity = arc.Capacity - arc.Flow;
                    id = arc.Destination.Id;
                }
                else if (arc.Destination.Id == id)
                {
                    residualCapacity = arc.Flow;
                    id = arc.Origin.Id;
                }
                else
                {
                    throw new ArgumentException("Invalid path", nameof(path));
                }

                //Update the minimum residual capacity
                if (!min.HasValue || residualCapacity < min.Value)
                {
                    min = residualCapacity;
                }
            }
            return min.Value;
        }
        #endregion

        #region Augmenting Path
        private static List<FordFulkersonArc> GetAugmentingPath(IReadOnlyList<FordFulkersonNode> nodes, int originNodeId, int destinationNodeId)
        {
            //Breadth-First-Search to find the shortest path from the origin to the destination
            //The shortest path is the path with the fewest arcs
            //Each arc can be traversed in both directions

            //Initialse the breadth-first-search
            var queue = InitializeAugmentingPathSearch(nodes, originNodeId);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var arc in node.OutgoingArcs)
                {
                    //Only arcs in the forward direction can be taken, if the capacity is not exhausted
                    if (arc.Capacity - arc.Flow > 0)
                    {
                        if (arc.Destination.SearchStatus == SearchState.Undiscovered)
                        {
                            arc.Destination.SearchStatus = SearchState.Discovered;
                            arc.Destination.Predecessor = arc;
                            queue.Enqueue(arc.Destination);
                        }

                    }
                }
                foreach (var arc in node.IncomingArcs)
                {
                    //Only arc in the backward direction can be taken, if the flow is not zero.
                    if (arc.Flow > 0)
                    {
                        if (arc.Origin.SearchStatus == SearchState.Undiscovered)
                        {
                            arc.Origin.SearchStatus = SearchState.Discovered;
                            arc.Origin.Predecessor = arc;
                            queue.Enqueue(arc.Origin);
                        }
                    }
                }
                node.SearchStatus = SearchState.Visited;

                //Stop the search, if the destination is reached
                if (node.Id == destinationNodeId)
                {
                    var path = GetPath(node, originNodeId);
                    return path;
                }
            }
            return null;
        }

        private static Queue<FordFulkersonNode> InitializeAugmentingPathSearch(IReadOnlyList<FordFulkersonNode> nodes, int originNodeId)
        {
            var queue = new Queue<FordFulkersonNode>();
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].SearchStatus = SearchState.Undiscovered;
                nodes[i].Predecessor = null;
                if (nodes[i].Id == originNodeId)
                {
                    nodes[i].SearchStatus = SearchState.Discovered;
                    queue.Enqueue(nodes[i]);
                }
            }
            if (queue.Count != 1)
            {
                throw new KeyNotFoundException($"Origin node {originNodeId} not found.");
            }
            return queue;
        }

        private static List<FordFulkersonArc> GetPath(FordFulkersonNode node, int originNodeId)
        {
            var path = new List<FordFulkersonArc>();
            while (node.Id != originNodeId)
            {
                path.Add(node.Predecessor);
                node = node.Predecessor.Origin;
            }
            path.Reverse();
            return path;
        }
        #endregion

        #region Conversion And Validation
        public static IReadOnlyList<FordFulkersonNode> Convert<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>, IHasCapacity, IHasFlow
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var dict = nodes.ToDictionary(x => x.Id, x => new FordFulkersonNode(x.Id));
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = dict[nodes[i].Id];
                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    var fordFulkersonArc = new FordFulkersonArc(arc.Id, arc.Capacity, node, dict[arc.Destination.Id], arc.Flow);
                    node.AddOutgoingArc(fordFulkersonArc);
                    if (arc.Destination.OutgoingArcs.Any(x => x.Destination.Id == nodes[i].Id))
                    {
                        throw new HasAntiparallelArcException("This algorithm does not support graphs with antiparallel arcs.");
                    }
                }
            }
            return dict.Values.ToList();
        }

        public static void ValidateInput<TNode, TArc>(IList<FordFulkersonNode> nodes, int originNodeId, int destinationNodeId)
            where TNode : IHasId
        {
            GraphValidation.IdExists(nodes, originNodeId, destinationNodeId);
            GraphValidation.ValidateUniqueIds(nodes);
            GraphValidation.ValidateUniqueArcIds(nodes);
            GraphValidation.ValidateOnlyPositiveCapacities<FordFulkersonNode, FordFulkersonArc>(nodes);
            GraphValidation.ValidateNoAntiparallelArcs<FordFulkersonNode, FordFulkersonArc>(nodes);
        }
        #endregion
    }
}