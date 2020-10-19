using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations
{
    public class NodeWithEdges<TEdge> : IHasEdges<TEdge>
        where TEdge : IConnectsNodes<NodeWithEdges<TEdge>>
    {
        private readonly List<TEdge> edges = new List<TEdge>();
        public NodeWithEdges()
        {
        }

        public IReadOnlyCollection<TEdge> Edges => edges;

        public void AddEdge(TEdge edge)
        {
            edges.Add(edge);
        }

        public void RemoveEdge(TEdge edge)
        {
            edges.Remove(edge);
        }
    }
}