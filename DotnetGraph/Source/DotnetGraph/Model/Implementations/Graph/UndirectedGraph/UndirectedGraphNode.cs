using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations.Graph.UndirectedGraph
{
    public class UndirectedGraphNode :
        IHasId,
        IHasEdges<UndirectedGraphEdge>
    {
        private readonly List<UndirectedGraphEdge> edges;

        public UndirectedGraphNode(int id, IReadOnlyCollection<UndirectedGraphEdge> edges = null)
        {
            Id = id;
            this.edges = edges is null ? new List<UndirectedGraphEdge>() : new List<UndirectedGraphEdge>(edges);
        }

        public int Id { get; }
        public IReadOnlyCollection<UndirectedGraphEdge> Edges => edges;
        public void Add(UndirectedGraphEdge edge)
        {
            edges.Add(edge);
        }
    }
}