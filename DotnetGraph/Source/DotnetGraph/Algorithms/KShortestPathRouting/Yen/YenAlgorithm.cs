using Ardalis.GuardClauses;
using DotnetGraph.Algorithms.ShortestPath;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;

namespace DotnetGraph.Algorithms.KShortestPathRouting.Yen;
public class YenAlgorithm : IKShortestPathRoutingAlgorithm
{
    public KShortestPathRoutingResult<TArc> GetKShortestPaths<TNode, TArc>(IReadOnlyList<TNode> nodes,
        int originNodeId,
        int destinationNodeId,
        int k)
        where TNode : IHasOutgoingArcs<TArc>, IHasId
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId
    {
        var dijkstraNodes = DijkstraAlgorithm.Convert<TNode, TArc>(nodes);
        var kShortestPaths = GetKShortestPaths(dijkstraNodes, originNodeId, destinationNodeId, k);
        var convertedResult = ConvertResult<TNode, TArc>(nodes, kShortestPaths);
        return convertedResult;
    }

    public static KShortestPathRoutingResult<TArc> ConvertResult<TNode, TArc>(IReadOnlyList<TNode> nodes,
        DijkstraArc[][] shortestPaths)
        where TNode : IHasOutgoingArcs<TArc>, IHasId
        where TArc : IHasDestination<TNode>, IHasWeight, IHasId
    {
        Guard.Against.Null(shortestPaths);
        Guard.Against.Null(nodes);
        var dict = nodes.SelectMany(x => x.OutgoingArcs).ToDictionary(x => x.Id, x => x);
        var convertedPaths = new List<ReadOnlyCollection<TArc>>();
        for (int i = 0; i < shortestPaths.Length; i++)
        {
            var convertedPath = new ReadOnlyCollection<TArc>(shortestPaths[i].Select(x => dict[x.Id]).ToArray());
            convertedPaths.Add(convertedPath);
        }
        var result = new KShortestPathRoutingResult<TArc>(convertedPaths);
        return result;
    }

    public static DijkstraArc[][] GetKShortestPaths(IReadOnlyList<DijkstraNode> nodes,
        int originNodeId,
        int destinationNodeId,
        int k)
    {
        Guard.Against.NegativeOrZero(k);
        Guard.Against.NullOrEmpty(nodes);

        DijkstraAlgorithm.ResetProgress(nodes);
        var shortestPaths = new List<ShortestPathResult<DijkstraArc>>(k);
        var initialShortestPath = DijkstraAlgorithm.GetShortestPath(nodes, originNodeId, destinationNodeId);
        shortestPaths.Add(initialShortestPath);
        var candidates = new List<ShortestPathResult<DijkstraArc>>();
        for (int i = 1; i < k; i++)
        {
            var jUpperBound = shortestPaths[i - 1].Arcs.Count;
            for (int j = 0; j < jUpperBound; j++)
            {
                var spurNode = shortestPaths[i - 1].Arcs[j].Origin;
                var rootPath = shortestPaths[i - 1].Arcs.Take(j);
                var removedArcs = new List<DijkstraArc>();
                foreach (var shortestPath in shortestPaths)
                {
                    var rootOfShortestPath = shortestPath.Arcs.Take(j);
                    if (Enumerable.SequenceEqual(rootPath, rootOfShortestPath))
                    {
                        removedArcs.Add(shortestPath.Arcs[j]);
                        spurNode.RemoveArc(shortestPath.Arcs[j]);
                    }
                }
                var canidateArcs = new List<DijkstraArc>();
                var candidateWeight = 0d;
                foreach (var arc in rootPath)
                {
                    removedArcs.Add(arc);
                    arc.Origin.RemoveArc(arc);
                    canidateArcs.Add(arc);
                    candidateWeight += arc.Weight;
                }
                ShortestPathResult<DijkstraArc>? spurPath = null;
                DijkstraAlgorithm.ResetProgress(nodes);
                try
                {
                    spurPath = DijkstraAlgorithm.GetShortestPath(nodes, spurNode.Id, destinationNodeId);
                }
                catch (InvalidDestinationException) { }
                catch (IdsNotUniqueException) { }
                foreach (var arc in removedArcs)
                {
                    arc.Origin.AddArc(arc);
                }
                if (spurPath != null)
                {
                    foreach (var arc in spurPath.Arcs)
                    {
                        canidateArcs.Add(arc);
                        candidateWeight += arc.Weight;
                    }
                    var candidate = new ShortestPathResult<DijkstraArc>(new ReadOnlyCollection<DijkstraArc>(canidateArcs), candidateWeight);
                    if (!candidates.Any(x => Enumerable.SequenceEqual(x.Arcs, candidate.Arcs)))
                    {
                        candidates.Add(candidate);
                    }
                }
            }
            if (candidates.Count == 0)
            {
                break;
            }
            candidates = candidates.OrderBy(x => x.TotalWeight).ToList();
            shortestPaths.Add(candidates[0]);
            candidates.RemoveAt(0);
        }
        return shortestPaths.Select(x => x.Arcs.ToArray()).ToArray();
    }
}
