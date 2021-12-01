namespace DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph
{
    public class WeightedUndirectedGraphNode :
        IHasId,
        IHasEdges<WeightedUndirectedGraphEdge>
    {
        private readonly List<WeightedUndirectedGraphEdge> edges;
        public WeightedUndirectedGraphNode(int id, IReadOnlyCollection<WeightedUndirectedGraphEdge> edges = null)
        {
            Id = id;
            this.edges = edges is null ? new List<WeightedUndirectedGraphEdge>() : new List<WeightedUndirectedGraphEdge>(edges);
        }

        public int Id { get; }
        public IReadOnlyCollection<WeightedUndirectedGraphEdge> Edges => edges;

        public void Add(WeightedUndirectedGraphEdge edge)
        {
            edges.Add(edge);
        }
    }
}