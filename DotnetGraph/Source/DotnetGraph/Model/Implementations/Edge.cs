using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Model.Implementations
{
    public class Edge<TNode> : IConnectsNodes<TNode>
    {
        public Edge(TNode node1, TNode node2)
        {
            Node1 = node1 ?? throw new ArgumentNullException(nameof(node1));
            Node2 = node2 ?? throw new ArgumentNullException(nameof(node2));
        }

        public TNode Node1 { get; }
        public TNode Node2 { get; }
    }
}