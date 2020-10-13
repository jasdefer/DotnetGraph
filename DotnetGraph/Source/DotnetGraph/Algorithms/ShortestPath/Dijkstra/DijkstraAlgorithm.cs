using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraAlgorithm : IShortestPathAlgorithm
    {
        public ShortestPathResult<TArc> GetShortestPath<TArc, TNode>(IList<TNode> nodes, int originNodeId, int destinationNodeId)
            where TArc : IHasDestination<TNode>, IHasWeight, IHasId
            where TNode : IHasOutgoingArcs<TArc>, IHasId
        {
            var dijkstraNodes = Convert<TNode, TArc>(nodes);
            var dijkstraResult = GetShortestPath(dijkstraNodes, originNodeId, destinationNodeId);
            var shortestPathResult = ConvertResult<TNode, TArc>(nodes, dijkstraResult);
            return shortestPathResult;
        }


        public static List<DijkstraNode> Convert<TNode, TArc>(IList<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasWeight, IHasId
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var dict = nodes.ToDictionary(x => x.Id, x => new DijkstraNode(x.Id));

            for (int i = 0; i < nodes.Count; i++)
            {
                var originId = nodes[i].Id;
                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    var dijkstraArc = new DijkstraArc(arc.Id, dict[originId], dict[arc.Destination.Id], arc.Weight);
                    dict[nodes[i].Id].AddArc(dijkstraArc);
                }
            }
            return dict.Values.ToList();
        }

        public static ShortestPathResult<TArc> ConvertResult<TNode, TArc>(IList<TNode> nodes, ShortestPathResult<DijkstraArc> dijkstraResult)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasWeight, IHasId
        {
            if (dijkstraResult is null)
            {
                throw new ArgumentNullException(nameof(dijkstraResult));
            }

            var arcs = new List<TArc>(dijkstraResult.Arcs.Count);
            var dict = nodes.SelectMany(x => x.OutgoingArcs).ToDictionary(x => x.Id, x => x);
            for (int i = 0; i < dijkstraResult.Arcs.Count; i++)
            {
                var arcId = dijkstraResult.Arcs[i].Id;
                arcs.Add(dict[arcId]);
            }
            var result = new ShortestPathResult<TArc>(arcs.AsReadOnly(), dijkstraResult.TotalWeight);
            return result;
        }

        public static ShortestPathResult<DijkstraArc> GetShortestPath(List<DijkstraNode> nodes, int originNodeId, int destinationNodeId)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            nodes.Single(x => x.Id == originNodeId).DistanceFromOrigin = 0;
            while (nodes.Count > 0)
            {
                var node = GetClosestNode(nodes);
                if (destinationNodeId == node.Id)
                {
                    var result = BuildResult(node);
                    return result;
                }
                foreach (var arc in node.OutgoingArcs)
                {
                    if (arc.Weight < 0)
                    {
                        throw new Exception("Dijkstra cannot handle arcs with negative weights.");
                    }

                    var newDistance = node.DistanceFromOrigin.Value + arc.Weight;
                    var currentDistance = arc.Destination.DistanceFromOrigin;
                    var isImprovement = !currentDistance.HasValue || newDistance < arc.Destination.DistanceFromOrigin;
                    if (isImprovement)
                    {
                        arc.Destination.DistanceFromOrigin = newDistance;
                        arc.Destination.BestPredecessor = arc;
                    }
                }
                nodes.Remove(node);
            }
            throw new Exception($"The destination ({destinationNodeId}) is not included in the given nodes.");
        }

        public static DijkstraNode GetClosestNode(IList<DijkstraNode> nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var min = double.MaxValue;
            var index = (int?)null;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].DistanceFromOrigin < min)
                {
                    min = nodes[i].DistanceFromOrigin.Value;
                    index = i;
                }
            }
            if (index.HasValue)
            {
                return nodes[index.Value];
            }

            throw new Exception("The destination is not connected to the origin.");
        }

        public static ShortestPathResult<DijkstraArc> BuildResult(DijkstraNode destination)
        {
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var arcs = new List<DijkstraArc>();
            var node = destination;
            var totalWeight = 0d;
            while (node.BestPredecessor != null)
            {
                arcs.Insert(0, node.BestPredecessor);
                totalWeight += node.BestPredecessor.Weight;
                node = node.BestPredecessor.Origin;
            }

            var result = new ShortestPathResult<DijkstraArc>(arcs.AsReadOnly(), totalWeight);
            return result;
        }
    }
}