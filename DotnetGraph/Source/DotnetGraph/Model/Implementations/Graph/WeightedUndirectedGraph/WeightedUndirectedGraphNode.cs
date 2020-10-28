using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph
{
    public class WeightedUndirectedGraphNode :
        IdNodeWithEdge<WeightedUndirectedGraphEdge>,
        IHasId,
        IHasEdges<WeightedUndirectedGraphEdge>
    {
        public WeightedUndirectedGraphNode(int id) : base(id)
        {
        }
    }
}