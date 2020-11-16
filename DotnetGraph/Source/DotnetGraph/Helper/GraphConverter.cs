using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Helper
{
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
                var arc = new WeightedDirectedGraphArc(i + 1,  arcDatas[i].Weight, destination);
                origin.Add(arc);
            }
            return nodes.Values.ToArray();
        }
    }
}