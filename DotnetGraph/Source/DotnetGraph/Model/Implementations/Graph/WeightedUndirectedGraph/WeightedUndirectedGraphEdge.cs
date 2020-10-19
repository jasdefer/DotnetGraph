using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph
{
    public class WeightedUndirectedGraphEdge :
        Edge<WeightedUndirectedGraphNode>,
        IHasId,
        IHasWeight,
        IConnectsNodes<WeightedUndirectedGraphNode>
    {
        public WeightedUndirectedGraphEdge(int id,
            double weight,
            WeightedUndirectedGraphNode node1,
            WeightedUndirectedGraphNode node2) : base(node1, node2)
        {
            Id = id;
            Weight = weight;
        }

        public int Id { get; }
        public double Weight { get; }
    }
}