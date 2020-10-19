using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.UndirectedGraph
{
    public class UndirectedGraphEdge : Edge<UndirectedGraphNode>,
        IHasId,
        IConnectsNodes<UndirectedGraphNode>
    {
        public UndirectedGraphEdge(int id,
            UndirectedGraphNode node1,
            UndirectedGraphNode node2) : base(node1, node2)
        {
            Id = id;
        }

        public int Id { get; }
    }
}