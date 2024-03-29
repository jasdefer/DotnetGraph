﻿namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra;

public class DijkstraAlgorithm : IShortestPathAlgorithm
{
    public ShortestPathResult<TArc> GetShortestPath<TNode, TArc>(IReadOnlyList<TNode> nodes, int originNodeId, int destinationNodeId)
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId
        where TNode : IHasOutgoingArcs<TArc>, IHasId
    {
        var dijkstraNodes = Convert<TNode, TArc>(nodes);
        var dijkstraResult = GetShortestPath(dijkstraNodes, originNodeId, destinationNodeId);
        var shortestPathResult = ConvertResult<TNode, TArc>(nodes, dijkstraResult);
        return shortestPathResult;
    }

    public static IReadOnlyList<DijkstraNode> Convert<TNode, TArc>(IReadOnlyList<TNode> nodes)
        where TNode : IHasOutgoingArcs<TArc>, IHasId
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId
    {
        if (nodes is null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        var dict = nodes.ToDictionary(x => x.Id, x => new DijkstraNode(x.Id, new List<DijkstraArc>()));

        for (int i = 0; i < nodes.Count; i++)
        {
            var origin = dict[nodes[i].Id];
            foreach (var arc in nodes[i].OutgoingArcs)
            {
                var dijkstraArc = new DijkstraArc(arc.Id, arc.Weight, origin, dict[arc.Destination.Id]);
                origin.AddArc(dijkstraArc);
            }
        }
        return dict.Values.ToList();
    }

    public static void ResetProgress(IReadOnlyList<DijkstraNode> nodes)
    {
        if (nodes is null)
        {
            return;
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].DistanceFromOrigin = null;
            nodes[i].BestPredecessor = null;
        }
    }

    public static ShortestPathResult<TArc> ConvertResult<TNode, TArc>(IReadOnlyList<TNode> nodes, ShortestPathResult<DijkstraArc> dijkstraResult)
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

    public static ShortestPathResult<DijkstraArc> GetShortestPath(IReadOnlyList<DijkstraNode> inputNodes,
        int originNodeId,
        int destinationNodeId)
    {
        var originNode = GetOrigin(inputNodes, originNodeId);
        var heap = new DijkstraHeap(originNode);
        while (heap.Count > 0)
        {
            var node = heap.ExtractMinimum();
            if (destinationNodeId == node.Id)
            {
                var result = BuildResult(node);
                return result;
            }
            foreach (var arc in node.OutgoingArcs)
            {
                if (arc.Weight < 0)
                {
                    throw new NegativeWeightException("Dijkstra cannot handle arcs with negative weights.");
                }
#pragma warning disable CS8629 // Nullable value type may be null.
                var newDistance = node.DistanceFromOrigin.Value + arc.Weight;
#pragma warning restore CS8629 // Nullable value type may be null.
                heap.UpdateArc(arc, newDistance);
            }
        }
        ValidateInput<DijkstraNode, DijkstraArc>(inputNodes, originNodeId, destinationNodeId);
        throw new InvalidDestinationException("Could not reach the destination.");
    }

    public static void ValidateInput<TNode, TArc>(IReadOnlyList<TNode> nodes, int originNodeId, int destinationNodeId)
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId
        where TNode : IHasOutgoingArcs<TArc>, IHasId
    {
        GraphValidation.ValidateUniqueIds(nodes);
        GraphValidation.ValidateUniqueArcIds<TNode, TArc>(nodes);
        GraphValidation.IdExists(nodes, originNodeId, destinationNodeId);
        GraphValidation.ValidateOnlyPositiveWeights<TNode, TArc>(nodes);
    }

    public static DijkstraNode GetOrigin(IReadOnlyList<DijkstraNode> nodes, int originNodeId)
    {
        if (nodes is null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        var originIndex = -1;
        for (int i = 0; i < nodes.Count; i++)
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
            throw new KeyNotFoundException($"Cannot find the origin node id {originNodeId}");
        }

        return nodes[originIndex];
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
