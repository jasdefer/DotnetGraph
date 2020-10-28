using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Helper
{
    public static class GraphConverter
    {
        public static TNode[] GetNodes<TNode, TEdge>(IEnumerable<TEdge> edges)
            where TNode: IHasId
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
    }
}