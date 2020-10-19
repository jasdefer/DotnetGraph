using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.UndirectedGraph
{
    public class UndirectedGraphNode : IdNodeWithEdge<UndirectedGraphEdge>,
        IHasId,
        IHasEdges<UndirectedGraphEdge>
    {
        public UndirectedGraphNode(int id) : base(id)
        {
        }
    }
}