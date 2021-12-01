using System.Globalization;
using System.IO;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace DotnetGraph.Helper;

public static class GraphConverter
{
    public static TNode[] GetNodes<TNode, TEdge>(IEnumerable<TEdge> edges)
        where TNode : IHasId
        where TEdge : IConnectsNodes<TNode>
    {
        if (edges is null)
        {
            throw new ArgumentNullException(nameof(edges));
        }
        var nodes = new Dictionary<int, TNode>();
        foreach (var edge in edges)
        {
            if (!nodes.ContainsKey(edge.Node1.Id))
            {
                nodes.Add(edge.Node1.Id, edge.Node1);
            }
            if (!nodes.ContainsKey(edge.Node2.Id))
            {
                nodes.Add(edge.Node2.Id, edge.Node2);
            }
        }
        return nodes.Values.ToArray();
    }

    public static WeightedDirectedGraphNode[] GetNodes(IList<ArcData> arcDatas)
    {
        if (arcDatas is null)
        {
            throw new ArgumentNullException(nameof(arcDatas));
        }

        var nodes = new Dictionary<int, WeightedDirectedGraphNode>();
        for (int i = 0; i < arcDatas.Count; i++)
        {
            if (!nodes.ContainsKey(arcDatas[i].OriginNodeId))
            {
                var node = new WeightedDirectedGraphNode(arcDatas[i].OriginNodeId, new List<WeightedDirectedGraphArc>());
                nodes.Add(node.Id, node);
            }
            if (!nodes.ContainsKey(arcDatas[i].DestinationNodeId))
            {
                var node = new WeightedDirectedGraphNode(arcDatas[i].DestinationNodeId, new List<WeightedDirectedGraphArc>());
                nodes.Add(node.Id, node);
            }
            var destination = nodes[arcDatas[i].DestinationNodeId];
            var origin = nodes[arcDatas[i].OriginNodeId];
            var arc = new WeightedDirectedGraphArc(i + 1, arcDatas[i].Weight, destination);
            origin.Add(arc);
        }
        return nodes.Values.ToArray();
    }

    /// <summary>
    /// Convert a well known string to a graph.
    /// </summary>
    /// <param name="wkn">The string contains a line for each arc. Each lines has the structure: {OriginId}{<paramref name="separator"/>}{DestinationId}{<paramref name="separator"/>}{Weight}</param>
    public static WeightedDirectedGraphNode[] GetNodes(string wkn, string separator = "\t")
    {
        if (wkn is null)
        {
            throw new ArgumentNullException(nameof(wkn));
        }
        var dict = new Dictionary<int, WeightedDirectedGraphNode>(wkn.Length / 10);
        var arcId = 0;
        using (var reader = new StringReader(wkn))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var args = line.Split(separator);
                if (args.Length < 3)
                {
                    throw new ArgumentException($"Invalid input string.");
                }
                var originId = int.Parse(args[0], CultureInfo.InvariantCulture);
                var destinationId = int.Parse(args[1], CultureInfo.InvariantCulture);
                var weight = double.Parse(args[2], CultureInfo.InvariantCulture);
                if (!dict.ContainsKey(originId))
                {
                    dict.Add(originId, new WeightedDirectedGraphNode(originId));
                }

                if (!dict.ContainsKey(destinationId))
                {
                    dict.Add(destinationId, new WeightedDirectedGraphNode(destinationId));
                }
                var arc = new WeightedDirectedGraphArc(++arcId, weight, dict[destinationId]);
                dict[originId].Add(arc);
            }
        }
        return dict.Values.ToArray();
    }
}
